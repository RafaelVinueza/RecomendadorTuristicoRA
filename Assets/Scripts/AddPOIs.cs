using Mapbox.Unity.Map;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;

public class AddPOIs : MonoBehaviour
{
    private GameObject mapa;
    private GameObject controller;
    private AbstractMap abstractMap;
    private Variables variables;
    private PrefabItemOptions capaDefault;
    private List<string> list_default;

    public GameObject prefab;

    void Start()
    {
        mapa = GameObject.Find("Map");
        controller = GameObject.Find("Controller");

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


        }
    }

}
