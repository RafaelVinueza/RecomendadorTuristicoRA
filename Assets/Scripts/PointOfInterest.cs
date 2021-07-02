using System.Collections;
using UnityEngine;
using UnityEngine.UI;

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
    
    public string tipo;
    public string distancia;
    public GameObject cubo;

    public PointOfInterest(string tipo, string distancia)
    {
        this.cubo = null;
        this.tipo = tipo;
        this.distancia = distancia;
    }

    public PointOfInterest(){
        this.cubo = null;
        this.tipo = "Sin definir";
    }
    
}
