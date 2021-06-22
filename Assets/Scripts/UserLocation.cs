using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UserLocation : MonoBehaviour
{
    
    public List<GameObject> prefabs = new List<GameObject>();

    double[,] ubicacionesVirtuales;
    public float miLat;
    public float miLong;
    private Variables variables;
    private PointOfInterest[] ubicaciones;


    List<PointOfInterest> ubicacionesVisibles = new List<PointOfInterest>();
    List<PointOfInterest> ubicacionesOcultas = new List<PointOfInterest>();

    // Start is called before the first frame update
    void Start()
    {
        miLat = GPS.Instance.latitud;
        miLong = GPS.Instance.longitud;

        //miLat = -0.12914759411639676f;
        //miLong = -78.48616997182278f;

        variables = GameObject.Find("Controller").GetComponent<Variables>();

        ubicaciones = variables.Ubicaciones;

        for (int i = 0; i < ubicaciones.Length; i++)
        {
            float distance = FormulaHaversine((float)miLat, (float)miLong, (float)ubicaciones[i].Latitud, (float)ubicaciones[i].Longitud);
            if (distance < 300)
            {
                ubicaciones[i].Distancia = distance.ToString();
                ubicacionesVisibles.Add(ubicaciones[i]);
            }
            else
            {
                ubicacionesOcultas.Add(ubicaciones[i]);
            }
        }

        ubicacionesVirtuales = EncontrarCuadrante((float)miLat, (float)miLong, ubicacionesVisibles);

        for (int i = 0; i < ubicacionesVisibles.Count; i++)
        {
            
            ubicacionesVisibles[i].Cubo = Instantiate(buscarIcono(ubicacionesVisibles[i].Tipo),
                new Vector3((float)ubicacionesVirtuales[i, 0], 0, (float)ubicacionesVirtuales[i, 1]),
                Quaternion.identity);
            ubicacionesVisibles[i].Cubo.transform.localScale = calcularEscala((float)TransformarMetrosUnidades(FormulaHaversine((float)miLat, (float)miLong, (float)ubicaciones[i].Latitud, (float)ubicaciones[i].Longitud)));
            ubicacionesVisibles[i].Distancia = "";
            
            if (ubicacionesVisibles[i].Cubo.GetComponent<POIData>() != null)
                ubicacionesVisibles[i].Cubo.GetComponent<POIData>().setData(ubicacionesVisibles[i]);

        }
    }

    public double[,] EncontrarCuadrante(float lat1, float long1, List<PointOfInterest> ubicaciones)
    {
        float lat3 = lat1;
        double[,] ubicacionesVirtuales = new double[ubicaciones.Count, 2];
        
        for (int i = 0; i < ubicaciones.Count; i++)
        {
            if (lat1 <= ubicaciones[i].Latitud && long1 <= ubicaciones[i].Longitud)
            {
                ubicacionesVirtuales[i, 0] = TransformarMetrosUnidades(FormulaHaversine((float)miLat, (float)miLong, lat3, (float)ubicaciones[i].Longitud));
                ubicacionesVirtuales[i, 1] = TransformarMetrosUnidades(FormulaHaversine((float)ubicaciones[i].Latitud, (float)ubicaciones[i].Longitud, lat3, (float)ubicaciones[i].Longitud));
            }else if (lat1 > ubicaciones[i].Latitud && long1 <= ubicaciones[i].Longitud)
            {
                ubicacionesVirtuales[i, 0] = TransformarMetrosUnidades(FormulaHaversine((float)miLat, (float)miLong, lat3, (float)ubicaciones[i].Longitud));
                ubicacionesVirtuales[i, 1] = TransformarMetrosUnidades(FormulaHaversine((float)ubicaciones[i].Latitud, (float)ubicaciones[i].Longitud, lat3, (float)ubicaciones[i].Longitud)) * -1;
            }else if (lat1 > ubicaciones[i].Latitud && long1 > ubicaciones[i].Longitud)
            {
                ubicacionesVirtuales[i, 0] = TransformarMetrosUnidades(FormulaHaversine((float)miLat, (float)miLong, lat3, (float)ubicaciones[i].Longitud)) * -1;
                ubicacionesVirtuales[i, 1] = TransformarMetrosUnidades(FormulaHaversine((float)ubicaciones[i].Latitud, (float)ubicaciones[i].Longitud, lat3, (float)ubicaciones[i].Longitud)) * -1;
            }else if (lat1 <= ubicaciones[i].Latitud && long1 > ubicaciones[i].Longitud)
            {
                ubicacionesVirtuales[i, 0] = TransformarMetrosUnidades(FormulaHaversine((float)miLat, (float)miLong, lat3, (float)ubicaciones[i].Longitud)) * -1;
                ubicacionesVirtuales[i, 1] = TransformarMetrosUnidades(FormulaHaversine((float)ubicaciones[i].Latitud, (float)ubicaciones[i].Longitud, lat3, (float)ubicaciones[i].Longitud));
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
        //float valor = (2f * distancia) / 30f;
        //return new Vector3(valor, valor, valor / 5);

        float valor = distancia / 120f;
        return new Vector3(valor, valor, valor);
    }

    public GameObject buscarIcono(string tipo)
    {
        switch (tipo)
        {
            case "Restaurante":
                return prefabs[1];
            case "Iglesia":
                return prefabs[2];
            case "Parque":
                return prefabs[3];
            case "Hospital":
                return prefabs[4];
            case "Gasolinera":
                return prefabs[5];
            case "Museo":
                return prefabs[6];
            case "Hotel":
                return prefabs[7];
            case "Cine":
                return prefabs[8];
            case "BarDisc":
                return prefabs[9];
            case "Deporte":
                return prefabs[10];
            case "Shopping":
                return prefabs[11];
            case "UniEducativa":
                return prefabs[12];
            case "PoliBomberos":
                return prefabs[13];
            case "Supermercado":
                return prefabs[14];
            default:
                return prefabs[0];
        }
    }


    // Update is called once per frame
    void Update()
    {
        //miLat = GPS.Instance.latitud;
        //miLong = GPS.Instance.longitud;

        miLat = -0.12914759411639676f;
        miLong = -78.48616997182278f;

        ubicacionesVisibles.Clear();
        ubicacionesOcultas.Clear();

        for (int i = 0; i < ubicaciones.Length; i++)
        {
            float distance = FormulaHaversine((float)miLat, (float)miLong, (float)ubicaciones[i].Latitud, (float)ubicaciones[i].Longitud);
            if (distance < 300)
            {
                ubicaciones[i].Distancia = distance.ToString();
                ubicacionesVisibles.Add(ubicaciones[i]);
            }
            else
            {
                ubicacionesOcultas.Add(ubicaciones[i]);
            }
        }

        for (int i = 0; i < ubicacionesOcultas.Count; i++)
        {
            Destroy(ubicacionesOcultas[i].Cubo);
        }

        ubicacionesVirtuales = EncontrarCuadrante((float)miLat, (float)miLong, ubicacionesVisibles);

        for (int i = 0; i < ubicacionesVisibles.Count; i++)
        {
            if(ubicacionesVisibles[i].Cubo == null)
            {
                ubicacionesVisibles[i].Cubo = Instantiate(buscarIcono(ubicacionesVisibles[i].Tipo),
                new Vector3((float)ubicacionesVirtuales[i, 0], 0, (float)ubicacionesVirtuales[i, 1]),
                Quaternion.identity);
                ubicacionesVisibles[i].Cubo.transform.localScale = calcularEscala((float)TransformarMetrosUnidades(FormulaHaversine((float)miLat, (float)miLong, (float)ubicaciones[i].Latitud, (float)ubicaciones[i].Longitud)));

                if(ubicacionesVisibles[i].Cubo.GetComponent<POIData>() != null)
                    ubicacionesVisibles[i].Cubo.GetComponent<POIData>().setData(ubicacionesVisibles[i]);
                
            }
            else
            {
                ubicacionesVisibles[i].Cubo.transform.position = new Vector3((float)ubicacionesVirtuales[i, 0], 0, (float)ubicacionesVirtuales[i, 1]);
                ubicacionesVisibles[i].Cubo.transform.localScale = calcularEscala((float)TransformarMetrosUnidades(FormulaHaversine((float)miLat, (float)miLong, (float)ubicaciones[i].Latitud, (float)ubicaciones[i].Longitud)));
            }

        }

    }
}
