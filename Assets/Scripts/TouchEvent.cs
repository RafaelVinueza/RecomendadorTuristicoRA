using System;
using System.Globalization;
using TMPro;
using UnityEngine;

public class TouchEvent : MonoBehaviour
{
    private POIData poiData;

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

                                name.text = poiData.getData().name;
                                businessStatus.text = (CultureInfo.InvariantCulture.TextInfo.ToTitleCase(poiData.getData().business_status.ToLower()));
                                address.text = poiData.getData().formatted_address;
                                phoneNumber.text = poiData.getData().international_phone_number;

                                if (poiData.getData().open_hours.open_now)
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
                                            if (day != -1)
                                                openNow.text = "Currently Closed, today schedule -> " + poiData.getData().open_hours.periods[day].open + " to " + poiData.getData().open_hours.periods[day].close;
                                            else
                                                openNow.text = "Currently Closed";
                                        }
                                    }
                                    catch (Exception)
                                    {
                                        openNow.text = "Unknown if open now";
                                    }

                                }

                                rating.text = "Rating(/5): " + poiData.getData().rating;
                                type.text = poiData.getData().typePOI;
                                distance.text = poiData.getData().distance + " meters";
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

                                name.text = poiData.getData().name;
                                businessStatus.text = (CultureInfo.InvariantCulture.TextInfo.ToTitleCase(poiData.getData().business_status.ToLower()));
                                address.text = poiData.getData().formatted_address;
                                phoneNumber.text = poiData.getData().international_phone_number;

                                if (poiData.getData().open_hours.open_now)
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
                                type.text = poiData.getData().typePOI;
                                distance.text = poiData.getData().distance + " meters";
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

}
