using System;
using System.Collections.Generic;
using System.Globalization;
using TMPro;
using UnityEngine;

public class TouchEvent : MonoBehaviour
{
    public List<Material> materials = new List<Material>();

    private POIData poiData;
    private Renderer planeRenderer;

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
                    poiData = hit.collider.GetComponent<POIData>();
                    if (poiData != null)
                    {
                        try
                        {
                            GameObject plano = hit.collider.transform.GetChild(0).gameObject;
                            planeRenderer = plano.GetComponent<Renderer>();

                            if (!plano.activeInHierarchy)
                            {
                                plano.SetActive(true);
                                TextMeshPro name = plano.transform.GetChild(0).gameObject.GetComponent<TextMeshPro>();
                                TextMeshPro businessStatus = plano.transform.GetChild(1).gameObject.GetComponent<TextMeshPro>();
                                TextMeshPro address = plano.transform.GetChild(2).gameObject.GetComponent<TextMeshPro>();
                                TextMeshPro phoneNumber = plano.transform.GetChild(3).gameObject.GetComponent<TextMeshPro>();
                                TextMeshPro openNow = plano.transform.GetChild(4).gameObject.GetComponent<TextMeshPro>();
                                TextMeshPro rating = plano.transform.GetChild(5).gameObject.GetComponent<TextMeshPro>();
                                TextMeshPro type = plano.transform.GetChild(6).gameObject.GetComponent<TextMeshPro>();
                                TextMeshPro distance = plano.transform.GetChild(7).gameObject.GetComponent<TextMeshPro>();
                                TextMeshPro scale = plano.transform.GetChild(8).gameObject.GetComponent<TextMeshPro>();


                                if (poiData.getData().name != null)
                                    name.text = poiData.getData().name;

                                if (poiData.getData().business_status != null)
                                    businessStatus.text = (CultureInfo.InvariantCulture.TextInfo.ToTitleCase(poiData.getData().business_status.ToLower()));

                                if (poiData.getData().formatted_address != null)
                                    address.text = poiData.getData().formatted_address;

                                if (poiData.getData().international_phone_number != null)
                                    phoneNumber.text = poiData.getData().international_phone_number;

                                if (poiData.getData().open_hours != null && poiData.getData().open_hours.open_now)
                                {
                                    openNow.text = "Currently Open";
                                }
                                else
                                {
                                    try
                                    {
                                        if (poiData.getData().open_hours.periods != null && poiData.getData().open_hours.periods.Length > 0)
                                        {
                                            int day = DayToNumber(DateTime.Now.ToString("dddd", CultureInfo.CreateSpecificCulture("en-US")));
                                            if (day != -1 && poiData.getData().open_hours.periods[day].open != "" && poiData.getData().open_hours.periods[day].close != "")
                                            {
                                                openNow.fontSize = 5;
                                                openNow.text = "Currently closed, today schedule -> " + poiData.getData().open_hours.periods[day].open + " to " + poiData.getData().open_hours.periods[day].close;
                                            }
                                            else
                                            {
                                                openNow.text = "Currently Closed";
                                            }
                                        }
                                    }
                                    catch (Exception)
                                    {
                                        openNow.text = "Unknown if open now";
                                    }
                                }

                                rating.text = "Rating(/5): " + poiData.getData().rating;

                                if (poiData.getData().typePOI != null)
                                    type.text = poiData.getData().typePOI;

                                if (poiData.getData().distance != null)
                                    distance.text = poiData.getData().distance + " meters";

                                scale.text = "Escala: (" + hit.collider.transform.localScale.x + "," + hit.collider.transform.localScale.y + "," + hit.collider.transform.localScale.z + ")";

                                planeRenderer.material = FindMaterial(type.text);
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
                    poiData = hit.collider.GetComponent<POIData>();
                    if (poiData != null)
                    {
                        try
                        {
                            GameObject plano = hit.collider.transform.GetChild(0).gameObject;
                            planeRenderer = plano.GetComponent<Renderer>();

                            if (!plano.activeInHierarchy)
                            {
                                plano.SetActive(true);
                                TextMeshPro name = plano.transform.GetChild(0).gameObject.GetComponent<TextMeshPro>();
                                TextMeshPro businessStatus = plano.transform.GetChild(1).gameObject.GetComponent<TextMeshPro>();
                                TextMeshPro address = plano.transform.GetChild(2).gameObject.GetComponent<TextMeshPro>();
                                TextMeshPro phoneNumber = plano.transform.GetChild(3).gameObject.GetComponent<TextMeshPro>();
                                TextMeshPro openNow = plano.transform.GetChild(4).gameObject.GetComponent<TextMeshPro>();
                                TextMeshPro rating = plano.transform.GetChild(5).gameObject.GetComponent<TextMeshPro>();
                                TextMeshPro type = plano.transform.GetChild(6).gameObject.GetComponent<TextMeshPro>();
                                TextMeshPro distance = plano.transform.GetChild(7).gameObject.GetComponent<TextMeshPro>();
                                TextMeshPro scale = plano.transform.GetChild(8).gameObject.GetComponent<TextMeshPro>();


                                if (poiData.getData().name != null)
                                    name.text = poiData.getData().name;

                                if(poiData.getData().business_status != null)
                                    businessStatus.text = (CultureInfo.InvariantCulture.TextInfo.ToTitleCase(poiData.getData().business_status.ToLower()));

                                if(poiData.getData().formatted_address != null)
                                    address.text = poiData.getData().formatted_address;

                                if(poiData.getData().international_phone_number != null)
                                    phoneNumber.text = poiData.getData().international_phone_number;

                                if (poiData.getData().open_hours != null && poiData.getData().open_hours.open_now)
                                {
                                    openNow.text = "Currently Open";
                                }
                                else 
                                {
                                    try
                                    {
                                        if (poiData.getData().open_hours.periods != null && poiData.getData().open_hours.periods.Length > 0)
                                        {
                                            int day = DayToNumber(DateTime.Now.ToString("dddd", CultureInfo.CreateSpecificCulture("en-US")));
                                            if (day != -1 && poiData.getData().open_hours.periods[day].open != "" && poiData.getData().open_hours.periods[day].close != "")
                                            {
                                                openNow.fontSize = 5;
                                                openNow.text = "Currently closed, today schedule -> " + poiData.getData().open_hours.periods[day].open + " to " + poiData.getData().open_hours.periods[day].close;
                                            }
                                            else
                                            {
                                                openNow.text = "Currently Closed";
                                            }
                                        }
                                    }
                                    catch (Exception)
                                    {
                                        openNow.text = "Unknown if open now";
                                    }
                                }
                                
                                rating.text = "Rating(/5): " + poiData.getData().rating;
                                
                                if(poiData.getData().typePOI != null) 
                                    type.text = poiData.getData().typePOI;

                                if(poiData.getData().distance != null)
                                    distance.text = poiData.getData().distance + " meters";

                                scale.text = "Escala: (" + hit.collider.transform.localScale.x + "," + hit.collider.transform.localScale.y + "," + hit.collider.transform.localScale.z + ")";

                                planeRenderer.material = FindMaterial(type.text);
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
#endif
    }

    public int DayToNumber(string day)
    {
        switch (day)
        {
            case "Monday":
                return 0;
            case "Tuesday":
                return 1;
            case "Wednesday":
                return 2;
            case "Thursday":
                return 3;
            case "Friday":
                return 4;
            case "Saturday":
                return 5;
            case "Sunday":
                return 6;
            default:
                return -1;
        }
    }

    public Material FindMaterial(string type)
    {
        switch (type)
        {
            case "Restaurant":
                return materials[1];
            case "Church":
                return materials[2];
            case "Park":
                return materials[3];
            case "Hospital":
                return materials[4];
            case "FullStation":
                return materials[5];
            case "Museum":
                return materials[6];
            case "Hotel":
                return materials[7];
            case "MovieTheater":
                return materials[8];
            case "Bar":
                return materials[9];
            case "Sport":
                return materials[10];
            case "Shopping":
                return materials[11];
            case "School":
                return materials[12];
            case "Police":
                return materials[13];
            case "Supermarket":
                return materials[14];
            case "Amusement":
                return materials[15];
            case "Aquarium":
                return materials[16];
            case "Cementery":
                return materials[17];
            case "Temple":
                return materials[18];
            case "Mosque":
                return materials[19];
            case "Stadium":
                return materials[19];
            case "Zoo":
                return materials[19];
            default:
                return materials[0];
        }
    }

}
