using Mapbox.Unity.Map;
using Mapbox.Unity.MeshGeneration.Factories;
using System;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using UnityEngine.UI;

public class AddPOIs : MonoBehaviour
{
    private GameObject mapa;
    private GameObject controller;
    private GameObject player;
    private AbstractMap abstractMap;
    private Variables variables;
    private PrefabItemOptions capaDefault;
    private List<string> list_default;
    private List<ListPois> lista_pois;
    private List<GameObject> poiObjects;
    private Transform elementoActual;
    private bool primeraVez;
    private List<string> opcionesDropDown = new List<string>();
    private int placeSelected;

    public GameObject prefab;
    public GameObject prefabDirection;
    public Slider slider;
    public Dropdown dropDownPlaces;


    void Start()
    {
        mapa = GameObject.Find("Map");
        controller = GameObject.Find("Controller");
        primeraVez = true;

        if (mapa != null && controller != null)
        {
            abstractMap = mapa.GetComponent<AbstractMap>();
            variables = controller.GetComponent<Variables>();

            capaDefault = new PrefabItemOptions();
            capaDefault.prefabItemName = "Default";
            capaDefault.spawnPrefabOptions = new SpawnPrefabOptions();
            capaDefault.spawnPrefabOptions.prefab = prefab;
            capaDefault.isActive = true;
            capaDefault.snapToTerrain = true;
            capaDefault.findByType = LocationPrefabFindBy.AddressOrLatLon;
            list_default = new List<string>();
            capaDefault.coordinates = new string[1];

            for (int i = 0; i < variables.days[variables.daySelected].Count; i++)
            {
                list_default.Add(String.Format(CultureInfo.InvariantCulture, "{0},{1}", variables.days[variables.daySelected][i].location.lat, variables.days[variables.daySelected][i].location.lng));
            }

            capaDefault.coordinates = list_default.ToArray();
            abstractMap.VectorData.AddPointsOfInterestSubLayer(capaDefault);

            dropDownPlaces.ClearOptions();

            for (int i = 0; i < variables.days[variables.daySelected].Count; i++)
            {
                opcionesDropDown.Add("Place " + (i + 1));
            }
            
            dropDownPlaces.AddOptions(opcionesDropDown);

        }
    }
    
    private void LateUpdate()
    {

        if (primeraVez)
        {
            mapa = GameObject.Find("Map");
            player = GameObject.Find("Player");
            poiObjects = new List<GameObject>();

            for (int i = 0; i < mapa.transform.childCount; i++)
            {
                GameObject tile = mapa.transform.GetChild(i).gameObject;

                for (int j = 0; j < tile.transform.childCount; j++)
                {
                    poiObjects.Add(tile.transform.GetChild(j).gameObject);
                }
            }
            
            distanciasDos();

            poiObjects.Sort((x, y) => Vector3.Distance(player.transform.position, x.transform.position).CompareTo(Vector3.Distance(player.transform.position, y.transform.position)));
            lista_pois.Sort((x, y) => x.Distance.CompareTo(y.Distance));

            //Debug.Log(lista_pois);

            for (int i = 0; i < poiObjects.Count; i++)
            {
                poiObjects[i].AddComponent<POIMapboxName>();
                poiObjects[i].GetComponent<POIMapboxName>().name = lista_pois[i].Name;
            }

            for (int i = 0; i < variables.days[variables.daySelected].Count; i++)
            {
                for (int j = 0; j < poiObjects.Count; j++)
                {
                    if (poiObjects[j].GetComponent<POIMapboxName>().name == variables.days[variables.daySelected][i].name)
                        poiObjects[j].GetComponent<POIMapboxName>().position = i;
                }
            }

            poiObjects.Sort((x, y) => x.GetComponent<POIMapboxName>().position.CompareTo(y.GetComponent<POIMapboxName>().position));

            //createDirections(new Transform[] { player.transform, poiObjects[0].transform });

            //para generar todos los caminos a los pois
            //for (int i = 0; i < poiObjects.Count - 1; i++)
            //{
            //    createDirections(new Transform[] { poiObjects[i].transform, poiObjects[i + 1].transform });
            //}

            slider.value = 15;
            primeraVez = false;
        }
        
        foreach (var way in GameObject.FindGameObjectsWithTag("DirectionWaypoint"))
        {

            if (way.transform.parent == null)
                way.Destroy();

            if (slider.value < 12)
                way.SetActive(false);
            else
                way.SetActive(true);
        }

        if(GameObject.FindGameObjectsWithTag("Direction").Length < 1 && poiObjects.Count < 2)
        {
            poiObjects = new List<GameObject>();

            for (int i = 0; i < mapa.transform.childCount; i++)
            {
                GameObject tile = mapa.transform.GetChild(i).gameObject;

                for (int j = 0; j < tile.transform.childCount; j++)
                {
                    poiObjects.Add(tile.transform.GetChild(j).gameObject);
                }
            }

            createDirections(new Transform[] { player.transform, poiObjects[0].transform });
        }
    }

    public void placeChange(int place)
    {
        placeSelected = place;

        abstractMap.VectorData.RemovePointsOfInterestSubLayer(capaDefault);
        list_default.Clear();
        list_default.Add(String.Format(CultureInfo.InvariantCulture, "{0},{1}", variables.days[variables.daySelected][place].location.lat, variables.days[variables.daySelected][place].location.lng));
        capaDefault.coordinates = list_default.ToArray();
        abstractMap.VectorData.AddPointsOfInterestSubLayer(capaDefault);
        poiObjects.Clear();

        foreach (var direction in GameObject.FindGameObjectsWithTag("Direction"))
        {
            direction.Destroy();
        }

        //if(placeSelected == 0)
        //    createDirections(new Transform[] { player.transform, poiObjects[0].transform });
        //else
        //    createDirections(new Transform[] { poiObjects[placeSelected - 1].transform, poiObjects[placeSelected].transform });
        
    }

    public void createDirections(Transform[] transforms)
    {
        GameObject direction = Instantiate(prefabDirection);
        DirectionsFactory directionsFactory = direction.GetComponent<DirectionsFactory>();
        Transform[] waypoints = transforms;
        directionsFactory.addWaypoints(waypoints);
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

    public void distanciasDos()
    {
            lista_pois = new List<ListPois>();

            for (int i = 0; i < variables.days[variables.daySelected].Count; i++)
            {
                lista_pois.Add(new ListPois(variables.days[variables.daySelected][i].name,
                FormulaHaversine(GPS.Instance.latitud, GPS.Instance.longitud,
                float.Parse(variables.days[0][i].location.lat, CultureInfo.InvariantCulture.NumberFormat),
                float.Parse(variables.days[0][i].location.lng, CultureInfo.InvariantCulture.NumberFormat))));
            }        
    }

}
