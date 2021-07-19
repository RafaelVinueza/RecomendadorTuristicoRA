using System;

[Serializable]
public class OpenHour
{
    public bool open_now;
    public Period[] periods;

    public OpenHour()
    {
        open_now = false;
        periods = new Period[7];
    }
}
