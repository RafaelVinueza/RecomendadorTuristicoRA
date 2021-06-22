using System.Collections;
using UnityEngine;
using UnityEngine.Android;
using UnityEngine.UI;

public class GPS : MonoBehaviour
{

    public static GPS Instance { set; get; }

    public float latitud;
    public float longitud;

    public Text latitudeValue;
    public Text longitudeValue;


    void Start()
    {
        latitudeValue.text = latitud.ToString();
        longitudeValue.text = longitud.ToString();
        latitud = -0.12914759411639676f;
        longitud = -78.48616997182278f;
    }

    private void Update()
    {
        latitudeValue.text = latitud.ToString();
        longitudeValue.text = longitud.ToString();

        //latitud = -0.12914759411639676f;
        //longitud = -78.48616997182278f;

        //latitud = latitud + 0.00001f;
        //longitud = longitud + 0.00001f;
    }


    IEnumerator GPSLocation()
    {
        if (!Input.location.isEnabledByUser)
            yield break;

        Input.location.Start();

        int maxWait = 20;
        while (Input.location.status == LocationServiceStatus.Initializing && maxWait > 0)
        {
            yield return new WaitForSeconds(1);
            maxWait--;
        }

        if (maxWait < 1)
        {
            //GPSStatus.text = "Time out";
            yield break;
        }

        if (Input.location.status == LocationServiceStatus.Failed)
        {
            //GPSStatus.text = "Unable to determine device location";
            yield break;
        }
        else
        {
            //GPSStatus.text = "Running";
            InvokeRepeating("UpdateGPSData", 0.5f, 1f);
        }
    }

    private void UpdateGPSData()
    {
        if (Input.location.status == LocationServiceStatus.Running)
        {
            latitud = Input.location.lastData.latitude;
            longitud = Input.location.lastData.longitude;
            latitudeValue.text = latitud.ToString();
            longitudeValue.text = longitud.ToString();
        }
        else
        {
            //GPSStatus.text = "Stop";
        }
    }

    private void Awake()
    {
        if (!Permission.HasUserAuthorizedPermission(Permission.FineLocation))
        {
            Permission.RequestUserPermission(Permission.FineLocation);
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
        StartCoroutine(GPSLocation());
    }
}
