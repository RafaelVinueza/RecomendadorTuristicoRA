using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.UI;

public class Validations : MonoBehaviour
{
    public InputField inputTotalDays;
    Regex re = new Regex("^(0?[1-9]|1[0-9]|2|2[0-9]|3[0-1])/(0?[1-9]|1[0-2])/(d{2}|d{4})$");

    public bool validateDate()
    {
        if (re.IsMatch(inputTotalDays.text))
            return true;
        else
            return false;
    }
}
