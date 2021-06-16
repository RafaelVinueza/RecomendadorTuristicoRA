using System;
using TMPro;
using UnityEngine;

public class TouchEvent : MonoBehaviour
{
    private POIData poiData;

    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount > 0 && Input.touches[0].phase == TouchPhase.Began)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.touches[0].position);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider != null)
                {
                    //Color newColor = new Color(Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f), 1.0f);
                    //hit.collider.GetComponent<MeshRenderer>().material.color = newColor;

                    poiData = hit.collider.GetComponent<POIData>();
                    if (poiData != null)
                    {
                        try
                        {
                            GameObject plano = hit.collider.transform.GetChild(0).gameObject;

                            if (!plano.activeInHierarchy)
                            {
                                plano.SetActive(true);
                                TextMeshPro nombre = plano.transform.GetChild(0).gameObject.GetComponent<TextMeshPro>();
                                TextMeshPro horario = plano.transform.GetChild(1).gameObject.GetComponent<TextMeshPro>();
                                TextMeshPro tipo = plano.transform.GetChild(2).gameObject.GetComponent<TextMeshPro>();
                                TextMeshPro distancia = plano.transform.GetChild(3).gameObject.GetComponent<TextMeshPro>();

                                nombre.text = poiData.getData().Nombre;
                                horario.text = poiData.getData().HorarioAtencion;
                                tipo.text = poiData.getData().Tipo;
                                distancia.text = poiData.getData().Distancia;
                            }
                            else
                            {
                                plano.SetActive(false);
                            }

                            
                        }
                        catch (Exception e)
                        {
                            Debug.Log("Error: " + e.Message);
                        }
                    }

                }
            }
        }

#if UNITY_EDITOR

        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider != null)
                {
                    //Color newColor = new Color(Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f), 1.0f);
                    //hit.collider.GetComponent<MeshRenderer>().material.color = newColor;

                    poiData = hit.collider.GetComponent<POIData>();
                    if(poiData != null)
                    {
                        try
                        {
                            GameObject plano = hit.collider.transform.GetChild(0).gameObject;

                            if (!plano.activeInHierarchy)
                                plano.SetActive(true);
                            else
                                plano.SetActive(false);

                            TextMeshPro nombre = plano.transform.GetChild(0).gameObject.GetComponent<TextMeshPro>();
                            TextMeshPro horario = plano.transform.GetChild(1).gameObject.GetComponent<TextMeshPro>();
                            TextMeshPro tipo = plano.transform.GetChild(2).gameObject.GetComponent<TextMeshPro>();

                            nombre.text = poiData.getData().Nombre;
                            horario.text = poiData.getData().HorarioAtencion;
                            tipo.text = poiData.getData().Tipo;
                        }
                        catch(Exception e)
                        {
                            Debug.Log("Error: " + e.Message);
                        }
                    }

                }
            }
        }
#endif
    }
}
