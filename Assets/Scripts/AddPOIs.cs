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

    public GameObject prefab;

    void Start()
    {
        _mapa = GameObject.Find("Map");
        _controller = GameObject.Find("Controller");

        if (_mapa != null && _controller != null)
        {
            _abstractMap = _mapa.GetComponent<AbstractMap>();
            _variables = _controller.GetComponent<Variables>();

            PrefabItemOptions capaDefault = new PrefabItemOptions();
            capaDefault.prefabItemName = "Default";
            capaDefault.spawnPrefabOptions = new SpawnPrefabOptions();
            capaDefault.spawnPrefabOptions.prefab = prefab;
            capaDefault.isActive = true;
            capaDefault.snapToTerrain = true;
            capaDefault.findByType = LocationPrefabFindBy.AddressOrLatLon;
            List<string> list_default = new List<string>();
            capaDefault.coordinates = new string[1];

            for (int i = 0; i < _variables.days[_variables.daySelected].Count; i++)
            {
                list_default.Add(String.Format(CultureInfo.InvariantCulture, "{0},{1}", _variables.days[_variables.daySelected][i].location.lat, _variables.days[_variables.daySelected][i].location.lng));
            }

            capaDefault.coordinates = list_default.ToArray();
            _abstractMap.VectorData.AddPointsOfInterestSubLayer(capaDefault);


        }
    }

}
