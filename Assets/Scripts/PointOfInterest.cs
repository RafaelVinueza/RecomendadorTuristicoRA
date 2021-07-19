using UnityEngine;

public class PointOfInterest
{

    public string type;
    public string business_status;
    public string formatted_address;
    public string international_phone_number;
    public string name;
    public System.Object opening_hours;
    public OpenHour open_hours;
    public float rating;
    public string website;
    public Location location = new Location();
    
    public string typePOI;
    public string distance;
    public GameObject cubo;

    public PointOfInterest(string tipo, string distancia)
    {
        this.cubo = null;
        this.typePOI = tipo;
        this.distance = distancia;
    }

    public PointOfInterest(){
        this.cubo = null;
        this.typePOI = "Type not defined";
        this.distance = "";
    }
    
}
