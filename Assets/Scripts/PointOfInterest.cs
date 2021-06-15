
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PointOfInterest
{
    double latitud;
    double longitud;
    string nombre;
    string horarioAtencion;
    string tipo;
    GameObject cubo;


    public double Latitud { get => latitud; set => latitud = value; }
    public double Longitud { get => longitud; set => longitud = value; }
    public string Nombre { get => nombre; set => nombre = value; }
    public string HorarioAtencion { get => horarioAtencion; set => horarioAtencion = value; }
    public GameObject Cubo { get => cubo; set => cubo = value; }
    public string Tipo { get => tipo; set => tipo = value; }

    public PointOfInterest(double latitud, double longitud, string nombre, string horarioAtencion, string tipo)
    {
        this.latitud = latitud;
        this.longitud = longitud;
        this.nombre = nombre;
        this.horarioAtencion = horarioAtencion;
        this.Cubo = null;
        this.tipo = tipo;
    }

    public PointOfInterest(double latitud, double longitud) {
        this.latitud = latitud;
        this.longitud = longitud;
        this.nombre = "Sin definir";
        this.horarioAtencion = "Sin definir";
        this.Cubo = null;
        this.tipo = "Sin definir";
    }

    public PointOfInterest() {
        this.latitud = 0;
        this.longitud = 0;
        this.nombre = "Sin definir";
        this.horarioAtencion = "Sin definir";
        this.Cubo = null;
        this.tipo = "Sin definir";
    }
}
