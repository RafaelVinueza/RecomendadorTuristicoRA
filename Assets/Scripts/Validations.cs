using System;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Validations : MonoBehaviour
{
    public InputField startDate;
    public InputField startTour;
    public InputField endTour;
    public InputField startLunch;
    public InputField endLunch;
    public Text textError;
    Regex reDate = new Regex("^\\d{4}([\\-/.])(0?[1-9]|1[1-2])\\1(3[01]|[12][0-9]|0?[1-9])$");
    Regex reHour = new Regex("^([01]?[0 - 9]|2[0-3]):[0-5][0-9]$");
    string todayDate;


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
    
    public bool validate()
    {

        //if (validateDate() && validateHour(startTour) && validateHour(endLunch) && validateHour(startLunch) && validateHour(endLunch))
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
                            if(DateTime.Compare(Convert.ToDateTime(startDate.text), Convert.ToDateTime(todayDate)) >= 0)
                            {
                                return true;
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
}
