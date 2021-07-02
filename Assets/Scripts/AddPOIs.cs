using Mapbox.Unity.Map;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;

public class AddPOIs : MonoBehaviour
{
    private GameObject _mapa;
    private GameObject _controller;
    private AbstractMap _abstractMap;
    private Variables _variables;
    private PointOfInterest[] ubicaciones;

    public GameObject prefab;

    // Start is called before the first frame update
    void Start()
    {


        //_mapa = GameObject.Find("Map");
        //_controller = GameObject.Find("Controller");

        //if(_mapa != null && _controller != null)
        //{
        //    _abstractMap = _mapa.GetComponent<AbstractMap>();
        //    _variables = _controller.GetComponent<Variables>();

        //    ubicaciones = _variables.Ubicaciones;
            
        //    PrefabItemOptions capaDefault = new PrefabItemOptions();
        //    capaDefault.prefabItemName = "Default";
        //    capaDefault.spawnPrefabOptions = new SpawnPrefabOptions();
        //    capaDefault.spawnPrefabOptions.prefab = prefab;
        //    capaDefault.isActive = true;
        //    capaDefault.snapToTerrain = true;
        //    capaDefault.findByType = LocationPrefabFindBy.AddressOrLatLon;
        //    List<string> list_default = new List<string>();
        //    capaDefault.coordinates = new string[1];

        //    //for(int i = 0; i < ubicaciones.Length; i++)
        //    //{
        //    //    list_default.Add(String.Format(CultureInfo.InvariantCulture, "{0},{1}", ubicaciones[i].Latitud, ubicaciones[i].Longitud));
        //    //}
            
        //    capaDefault.coordinates = list_default.ToArray();
        //    _abstractMap.VectorData.AddPointsOfInterestSubLayer(capaDefault);

            
        //}
    }

}
