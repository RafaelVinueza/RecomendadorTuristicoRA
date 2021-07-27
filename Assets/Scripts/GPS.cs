using System.Collections;
using UnityEngine;
using UnityEngine.Android;

public class GPS : MonoBehaviour
{

    public static GPS Instance { set; get; }
    public float latitud;
    public float longitud;

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

    void Start()
    {
        //latitud = -0.12941428659107065f;
        //longitud = -78.48624782279018f;
        latitud = -0.22092957856294237f;
        longitud = -78.5139782650852f;
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
            //latitud = Input.location.lastData.latitude;
            //longitud = Input.location.lastData.longitude;
            
        }
        else
        {
            //GPSStatus.text = "Stop";
        }
    }

   
}
