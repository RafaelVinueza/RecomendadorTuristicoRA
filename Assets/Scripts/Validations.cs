using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.UI;

public class Validations : MonoBehaviour
{
    public InputField totalDays;
    public InputField startDate;
    public InputField startTour;
    public InputField endTour;
    public InputField startLunch;
    public InputField endLunch;
    public List<string> categories;
    public List<Toggle> togglePlaces;
    public Text textError;
    public int placesCount = 0;

    private Regex reDate = new Regex("^\\d{4}([\\-/.])(0?[1-9]|1[0-2])\\1(3[01]|[12][0-9]|0?[1-9])$");
    private Regex reHour = new Regex("^(0[0-9]|1[0-9]|2[0-3]):[0-5][0-9]$");
    private string todayDate;
    private Variables variables;

    private void Awake()
    {
        variables = GameObject.Find("Controller").GetComponent<Variables>();
        categories = new List<string> { "Aquariums", "Premise", "Churches", "Cementeries", "Art Galleries", "Zoo", "Mosques", "City Halls", "Parks", "Museums", "Stadiums", "Amusement Park", "Indu Temples" };
    }


    public bool validateDate()
    {
        if (reDate.IsMatch(startDate.text))
            return true;
        else
            return false;
    }

    public bool validateHour(InputField Hour)
    {
        if (reHour.IsMatch(Hour.text))
            return true;
        else
            return false;
    }

    public void validatePlaces(bool tag)
    {
        if (tag)
            placesCount++;
        else
            placesCount--;
    }

    public bool validateSize(string name, int maxSize, int minSize, InputField field)
    {
        if (field.text.Length > maxSize || field.text.Length < minSize)
        {
            textError.text = name + " field must have a maximum of " + maxSize + " characters and a minimum of " + minSize + " characters";
            return false;
        }
        else
        {
            return true;
        }
    }

    public bool validate()
    {
        if(validateSize("Total Days", 1, 1, totalDays) && validateSize("Start Date", 10, 10, startDate) && validateSize("Start of Tours", 5, 5, startTour ) && validateSize("End of Tours", 5, 5, endTour) && validateSize("Start of Lunch", 5, 5, startLunch) && validateSize("End of Lunch", 5, 5, endLunch))
        {
            if (validateDate())
            {
                if (validateHour(startTour))
                {
                    if (validateHour(endTour))
                    {
                        if (validateHour(startLunch))
                        {
                            if (validateHour(startLunch))
                            {
                                todayDate = DateTime.UtcNow.Date.ToString("yyyy/MM/dd");
                                if (DateTime.Compare(Convert.ToDateTime(startDate.text), Convert.ToDateTime(todayDate)) >= 0)
                                {
                                    if (DateTime.Compare(new DateTime(2021, 02, 02, int.Parse(startTour.text.Split(':')[0]), int.Parse(startTour.text.Split(':')[1]), 0), new DateTime(2021, 02, 02, int.Parse(endTour.text.Split(':')[0]), int.Parse(endTour.text.Split(':')[1]), 0)) < 0)
                                    {
                                        if (DateTime.Compare(new DateTime(2021, 02, 02, int.Parse(startLunch.text.Split(':')[0]), int.Parse(startLunch.text.Split(':')[1]), 0), new DateTime(2021, 02, 02, int.Parse(endLunch.text.Split(':')[0]), int.Parse(endLunch.text.Split(':')[1]), 0)) < 0)
                                        {
                                            if (placesCount > 0)
                                            {
                                                variables.serviceLoadData.totalDays = int.Parse(totalDays.text, CultureInfo.InvariantCulture.NumberFormat);
                                                variables.serviceLoadData.startDate = startDate.text;
                                                variables.serviceLoadData.travelSchedule.start = startTour.text.Replace(":", "");
                                                variables.serviceLoadData.travelSchedule.end = endTour.text.Replace(":", "");
                                                variables.serviceLoadData.luchTime.start = startLunch.text.Replace(":", "");
                                                variables.serviceLoadData.luchTime.end = endLunch.text.Replace(":", "");
                                                variables.serviceLoadData.location.lat = GPS.Instance.latitud;
                                                variables.serviceLoadData.location.lng = GPS.Instance.longitud;

                                                variables.serviceLoadData.categories = new string[placesCount];
                                                int auxi = 0;

                                                for (int i = 0; i < togglePlaces.Count; i++)
                                                {
                                                    if (togglePlaces[i].isOn)
                                                    {
                                                        variables.serviceLoadData.categories[auxi] = categories[i];
                                                        auxi++;
                                                    }
                                                }

                                                return true;
                                            }
                                            else
                                            {
                                                textError.text = "Choice at least one place";
                                                return false;
                                            }
                                        }
                                        else
                                        {
                                            textError.text = "End hour for lunch cannot be earlier than start hour";
                                            return false;
                                        }
                                    }
                                    else
                                    {
                                        textError.text = "End hour for tours cannot be earlier than start hour";
                                        return false;
                                    }
                                }
                                else
                                {
                                    textError.text = "Start Date entered is less than today's date";
                                    return false;
                                }
                            }
                            else
                            {
                                textError.text = "End hour for lunch is not in the correct format (HH:mm)";
                                return false;
                            }
                        }
                        else
                        {
                            textError.text = "Start hour for lunch is not in the correct format (HH:mm)";
                            return false;
                        }
                    }
                    else
                    {
                        textError.text = "End hour for tour is not in the correct format (HH:mm)";
                        return false;
                    }
                }
                else
                {
                    textError.text = "Start hour for tour is not in the correct format (HH:mm)";
                    return false;
                }
            }
            else
            {
                textError.text = "Start date is not in the correct format (yyyy/mm/dd)";
                return false;
            }
        }
        else
        {
            return false;
        }

        
    }
}
