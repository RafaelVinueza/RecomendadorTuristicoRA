using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using UnityEngine.UI;

public class UserLocation : MonoBehaviour
{
    
    public List<GameObject> prefabs = new List<GameObject>();
    public float miLat;
    public float miLong;
    public Dropdown dropDownDays;
    public Button openMap;

    private double[,] ubicacionesVirtuales;
    private Variables variables;
    private List<PointOfInterest>[] days;
    private List<string> opcionesDropDown = new List<string>();
    private int daySelected;
    private List<PointOfInterest> ubicacionesVisibles = new List<PointOfInterest>();
    private List<PointOfInterest> ubicacionesOcultas = new List<PointOfInterest>();
    private int range = 70; //metros
    private float prefabAltitude = 10;

    void Awake()
    {
        variables = GameObject.Find("Controller").GetComponent<Variables>();
        openMap.onClick.AddListener(GameObject.Find("Controller").GetComponent<ChangeScene>().LoadScene);
        days = variables.days;
        dropDownDays.ClearOptions();
        daySelected = 0;
        variables.daySelected = 0;

        for (int i = 0; i < days.Length; i++)
        {
            opcionesDropDown.Add("Day " + (i + 1));
        }

        dropDownDays.AddOptions(opcionesDropDown);
    }

    void Start()
    {
        miLat = GPS.Instance.latitud;
        miLong = GPS.Instance.longitud;
        //miLat = -0.2184156877599296f;
        //miLong = -78.51204901265913f;
        initializeDay();
    }
    
    void Update()
    {
        //miLat = GPS.Instance.latitud;
        //miLong = GPS.Instance.longitud;

        for (int i = 0; i < ubicacionesVisibles.Count; i++)
        {
            float distance = FormulaHaversine((float)miLat, (float)miLong, float.Parse(ubicacionesVisibles[i].location.lat, CultureInfo.InvariantCulture.NumberFormat), float.Parse(ubicacionesVisibles[i].location.lng, CultureInfo.InvariantCulture.NumberFormat));
            ubicacionesVisibles[i].distance = distance.ToString("0.0");
            if (distance > range)
            {
                ubicacionesOcultas.Add(ubicacionesVisibles[i]);
                ubicacionesVisibles.RemoveAt(i);
            }
        }

        for (int i = 0; i < ubicacionesOcultas.Count; i++)
        {
            float distance = FormulaHaversine((float)miLat, (float)miLong, float.Parse(ubicacionesOcultas[i].location.lat, CultureInfo.InvariantCulture.NumberFormat), float.Parse(ubicacionesOcultas[i].location.lng, CultureInfo.InvariantCulture.NumberFormat));
            ubicacionesOcultas[i].distance = distance.ToString("0.0");
            if (distance < range)
            {
                ubicacionesVisibles.Add(ubicacionesOcultas[i]);
                ubicacionesOcultas.RemoveAt(i);
            }
            else
            {
                Destroy(ubicacionesOcultas[i].cubo);
            }
        }

        ubicacionesVirtuales = EncontrarCuadrante((float)miLat, (float)miLong, ubicacionesVisibles);

        for (int i = 0; i < ubicacionesVisibles.Count; i++)
        {
            if (ubicacionesVisibles[i].cubo == null)
            {
                ubicacionesVisibles[i].cubo = Instantiate(buscarIcono(ubicacionesVisibles[i].typePOI),
                new Vector3((float)ubicacionesVirtuales[i, 0], 0, (float)ubicacionesVirtuales[i, 1]),
                Quaternion.identity);
                ubicacionesVisibles[i].cubo.transform.localScale = calcularEscala((float)TransformarMetrosUnidades(FormulaHaversine((float)miLat, (float)miLong, float.Parse(days[daySelected][i].location.lat, CultureInfo.InvariantCulture.NumberFormat), float.Parse(days[daySelected][i].location.lng, CultureInfo.InvariantCulture.NumberFormat))));

                if (ubicacionesVisibles[i].cubo.GetComponent<POIData>() != null)
                    ubicacionesVisibles[i].cubo.GetComponent<POIData>().setData(ubicacionesVisibles[i]);

            }
            else
            {
                ubicacionesVisibles[i].cubo.transform.position = new Vector3((float)ubicacionesVirtuales[i, 0], prefabAltitude, (float)ubicacionesVirtuales[i, 1]);
                ubicacionesVisibles[i].cubo.transform.localScale = calcularEscala(FormulaHaversine((float)miLat, (float)miLong, float.Parse(days[daySelected][i].location.lat, CultureInfo.InvariantCulture.NumberFormat), float.Parse(days[daySelected][i].location.lng, CultureInfo.InvariantCulture.NumberFormat)));
            }

        }

    }

    public void daysChange(int day)
    {
        daySelected = day;
        variables.daySelected = day;

        foreach (PointOfInterest poi in ubicacionesVisibles)
            poi.cubo.Destroy();

        ubicacionesVisibles.Clear();
        ubicacionesOcultas.Clear();
        //ubicacionesVirtuales = ;
        initializeDay();
    }

    public void initializeDay()
    {
        for (int i = 0; i < days[daySelected].Count; i++)
        {
            float distance = FormulaHaversine((float)miLat, (float)miLong, float.Parse(days[daySelected][i].location.lat, CultureInfo.InvariantCulture.NumberFormat), float.Parse(days[daySelected][i].location.lng, CultureInfo.InvariantCulture.NumberFormat));
            days[daySelected][i].distance = distance.ToString();

            if (distance < range)
            {
                ubicacionesVisibles.Add(days[daySelected][i]);
            }
            else
            {
                ubicacionesOcultas.Add(days[daySelected][i]);
            }
        }

        ubicacionesVirtuales = EncontrarCuadrante((float)miLat, (float)miLong, ubicacionesVisibles);

        for (int i = 0; i < ubicacionesVisibles.Count; i++)
        {
            ubicacionesVisibles[i].cubo = Instantiate(buscarIcono(ubicacionesVisibles[i].typePOI),
                new Vector3((float)ubicacionesVirtuales[i, 0], 0, (float)ubicacionesVirtuales[i, 1]),
                Quaternion.identity);
            ubicacionesVisibles[i].cubo.transform.localScale = calcularEscala((float)TransformarMetrosUnidades(FormulaHaversine((float)miLat, (float)miLong, float.Parse(days[daySelected][i].location.lat, CultureInfo.InvariantCulture.NumberFormat), float.Parse(days[daySelected][i].location.lng, CultureInfo.InvariantCulture.NumberFormat))));
            ubicacionesVisibles[i].distance = "";

            if (ubicacionesVisibles[i].cubo.GetComponent<POIData>() != null)
                ubicacionesVisibles[i].cubo.GetComponent<POIData>().setData(ubicacionesVisibles[i]);

        }
    }

    public double[,] EncontrarCuadrante(float lat1, float long1, List<PointOfInterest> places)
    {
        float lat3 = lat1;
        double[,] ubicacionesVirtuales = new double[places.Count, 2];

        for (int i = 0; i < places.Count; i++)
        {
            if (lat1 <= float.Parse(places[i].location.lat, CultureInfo.InvariantCulture.NumberFormat) && long1 <= float.Parse(places[i].location.lng, CultureInfo.InvariantCulture.NumberFormat))
            {
                ubicacionesVirtuales[i, 0] = TransformarMetrosUnidades(FormulaHaversine((float)miLat, (float)miLong, lat3, float.Parse(places[i].location.lng, CultureInfo.InvariantCulture.NumberFormat)));
                ubicacionesVirtuales[i, 1] = TransformarMetrosUnidades(FormulaHaversine(float.Parse(places[i].location.lat, CultureInfo.InvariantCulture.NumberFormat), float.Parse(places[i].location.lng, CultureInfo.InvariantCulture.NumberFormat), lat3, float.Parse(places[i].location.lng, CultureInfo.InvariantCulture.NumberFormat)));
            }
            else if (lat1 > float.Parse(places[i].location.lat, CultureInfo.InvariantCulture.NumberFormat) && long1 <= float.Parse(places[i].location.lng, CultureInfo.InvariantCulture.NumberFormat))
            {
                ubicacionesVirtuales[i, 0] = TransformarMetrosUnidades(FormulaHaversine((float)miLat, (float)miLong, lat3, float.Parse(places[i].location.lng, CultureInfo.InvariantCulture.NumberFormat)));
                ubicacionesVirtuales[i, 1] = TransformarMetrosUnidades(FormulaHaversine(float.Parse(places[i].location.lat, CultureInfo.InvariantCulture.NumberFormat), float.Parse(places[i].location.lng, CultureInfo.InvariantCulture.NumberFormat), lat3, float.Parse(places[i].location.lng, CultureInfo.InvariantCulture.NumberFormat))) * -1;
            }
            else if (lat1 > float.Parse(places[i].location.lat, CultureInfo.InvariantCulture.NumberFormat) && long1 > float.Parse(places[i].location.lng, CultureInfo.InvariantCulture.NumberFormat))
            {
                ubicacionesVirtuales[i, 0] = TransformarMetrosUnidades(FormulaHaversine((float)miLat, (float)miLong, lat3, float.Parse(places[i].location.lng, CultureInfo.InvariantCulture.NumberFormat))) * -1;
                ubicacionesVirtuales[i, 1] = TransformarMetrosUnidades(FormulaHaversine(float.Parse(places[i].location.lat, CultureInfo.InvariantCulture.NumberFormat), float.Parse(places[i].location.lng, CultureInfo.InvariantCulture.NumberFormat), lat3, float.Parse(places[i].location.lng, CultureInfo.InvariantCulture.NumberFormat))) * -1;
            }
            else if (lat1 <= float.Parse(places[i].location.lat, CultureInfo.InvariantCulture.NumberFormat) && long1 > float.Parse(places[i].location.lng, CultureInfo.InvariantCulture.NumberFormat))
            {
                ubicacionesVirtuales[i, 0] = TransformarMetrosUnidades(FormulaHaversine((float)miLat, (float)miLong, lat3, float.Parse(places[i].location.lng, CultureInfo.InvariantCulture.NumberFormat))) * -1;
                ubicacionesVirtuales[i, 1] = TransformarMetrosUnidades(FormulaHaversine(float.Parse(places[i].location.lat, CultureInfo.InvariantCulture.NumberFormat), float.Parse(places[i].location.lng, CultureInfo.InvariantCulture.NumberFormat), lat3, float.Parse(places[i].location.lng, CultureInfo.InvariantCulture.NumberFormat)));
            }
        }
        return ubicacionesVirtuales;
    }

    public double TransformarMetrosUnidades(float metros)
    {
        //1 metro = 2 unidades de Unity
        return metros * 2;
    }

    public float FormulaHaversine(float lat1, float long1, float lat2, float long2)
    {
        float earthRad = 6371000;
        float lRad1 = lat1 * Mathf.Deg2Rad;
        float lRad2 = lat2 * Mathf.Deg2Rad;
        float dLat = (lat2 - lat1) * Mathf.Deg2Rad;
        float dLong = (long2 - long1) * Mathf.Deg2Rad;
        float a = Mathf.Sin(dLat / 2.0f) * Mathf.Sin(dLat / 2.0f) +
            Mathf.Cos(lRad1) * Mathf.Cos(lRad2) *
            Mathf.Sin(dLong / 2.0f) * Mathf.Sin(dLong / 2.0f);
        float c = 2 * Mathf.Atan2(Mathf.Sqrt(a), Mathf.Sqrt(1 - a));
        return earthRad * c;
    }

    public Vector3 calcularEscala(float distancia)
    {

        //float valor = (distancia) / 60;
        float valor = 10;

        if (distancia < 60 && distancia > 40)
            valor = 4;
        else if (distancia < 40)
            valor = 2;

        return new Vector3(valor, valor, valor);
    }

    public GameObject buscarIcono(string tipo)
    {
        switch (tipo)
        {
            case "Restaurant":
                return prefabs[1];
            case "Church":
                return prefabs[2];
            case "Park":
                return prefabs[3];
            case "Hospital":
                return prefabs[4];
            case "FullStation":
                return prefabs[5];
            case "Museum":
                return prefabs[6];
            case "Hotel":
                return prefabs[7];
            case "MovieTheater":
                return prefabs[8];
            case "Bar":
                return prefabs[9];
            case "Sport":
                return prefabs[10];
            case "Shopping":
                return prefabs[11];
            case "School":
                return prefabs[12];
            case "Police":
                return prefabs[13];
            case "Supermarket":
                return prefabs[14];
            case "Amusement":
                return prefabs[15];
            case "Aquarium":
                return prefabs[16];
            case "Cementery":
                return prefabs[17];
            case "Temple":
                return prefabs[18];
            case "Mosque":
                return prefabs[19];
            case "Stadium":
                return prefabs[19];
            case "Zoo":
                return prefabs[19];
            default:
                return prefabs[0];
        }
    }

    public void nearPoisChange(bool value)
    {
        if (value)
            range = 300;
        else
            range = 70;
    }
}
