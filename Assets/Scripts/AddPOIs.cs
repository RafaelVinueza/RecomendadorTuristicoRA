using Mapbox.Unity.Map;
using Mapbox.Unity.MeshGeneration.Factories;
using System;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;

public class AddPOIs : MonoBehaviour
{
    private GameObject mapa;
    private GameObject controller;
    private GameObject player;
    private AbstractMap abstractMap;
    private Variables variables;
    private PrefabItemOptions capaDefault;
    private List<string> list_default;
    private List<GameObject> poiObjects;
    private bool primeraVez;
    private Transform elementoActual;

    public GameObject prefab;
    public GameObject prefabDirection;

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

            var poi = abstractMap.VectorData.GetPointsOfInterestSubLayerAtIndex(0);
            var stop = 0;
        }
    }


    private void Update()
    {

        if (primeraVez)
        {
            mapa = GameObject.Find("Map");
            player = GameObject.Find("Player");
            poiObjects = new List<GameObject>();

            for (int i = 1; i <= mapa.transform.childCount - 1; i++)
            {
                GameObject tile = mapa.transform.GetChild(i).gameObject;

                for (int j = 0; j < tile.transform.childCount; j++)
                {
                    poiObjects.Add(tile.transform.GetChild(j).gameObject);
                }
            }

            elementoActual = player.transform;
            while (poiObjects.Count > 0)
            {
                poiObjects = generarPares(poiObjects);
            }

            primeraVez = false;
            var stop = 0;

        }

    }


    private List<GameObject> generarPares(List<GameObject> poiObjects)
    {
        GameObject poiMasCercano = poiObjects[0];
        float distancia = Vector3.Distance(elementoActual.position, poiObjects[0].transform.position);

        for (int i = 1; i <= poiObjects.Count; i++)
        {
            if (i < poiObjects.Count && distancia > Vector3.Distance(elementoActual.position, poiObjects[i].transform.position))
            {
                distancia = Vector3.Distance(elementoActual.position, poiObjects[i].transform.position);
                poiMasCercano = poiObjects[i];
            }
        }

        Debug.Log("Posicion Elemento Actual: " + elementoActual.position + " Posicion Mas Cercano: " + poiMasCercano.transform.position);
        //aqui generar el distance entre el elementoActual y el poiMasCercano
        GameObject direction = Instantiate(prefabDirection);
        DirectionsFactory directionsFactory = direction.GetComponent<DirectionsFactory>();
        Transform[] waypoints = {elementoActual, poiMasCercano.transform};
        directionsFactory.addWaypoints(waypoints);

        
        elementoActual = poiMasCercano.transform;
        poiObjects.Remove(poiMasCercano);

        return poiObjects;
    }

}
