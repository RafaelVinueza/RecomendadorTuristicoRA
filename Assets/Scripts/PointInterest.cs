using System;

[Serializable]
public class PointInterest
{
    public string type;
    public string business_status;
    public string formatted_address;
    public Location location = new Location();
}
