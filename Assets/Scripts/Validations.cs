using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Validations : MonoBehaviour
{
    public InputField inputTotalDays;
    public InputField startTour;
    public InputField endTour;
    public InputField startLunch;
    public InputField endLunch;
    Regex reDate = new Regex("^\\d{4}([\\-/.])(0?[1-9]|1[1-2])\\1(3[01]|[12][0-9]|0?[1-9])$");
    Regex reHour = new Regex("^([01]?[0 - 9]|2[0-3]):[0-5] [0-9]$");

    
    public bool validateDate()
    {
        if (reDate.IsMatch(inputTotalDays.text))
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
            return true;
        else
            return false;
    }
}
