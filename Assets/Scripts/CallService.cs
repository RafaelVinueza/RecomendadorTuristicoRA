using System.Collections;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class CallService : MonoBehaviour
{
    [System.Serializable]
    public struct localizacion
    {
        public float lat;
        public float lng;
    }

    [System.Serializable]
    public struct estructuraDatos
    {
        public int totalDays;
        public localizacion location;
        public string startDate;
        public string[] categories;
    }

    public estructuraDatos datos;

    void Start()
    {
        GameObject.Find("ButtonSubmit").GetComponent<Button>().onClick.AddListener(PostData);
        
    }

    void PostData() => StartCoroutine(PostData_Coroutine()); 
    IEnumerator PostData_Coroutine()
    {

        var request = new UnityWebRequest("http://localhost:8080/ttdp", "POST");
        string json = JsonUtility.ToJson(datos);

        byte[] bodyRaw = Encoding.UTF8.GetBytes(json);
        request.uploadHandler = (UploadHandler)new UploadHandlerRaw(bodyRaw);
        request.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");
        yield return request.SendWebRequest();

        if (request.error != null)
        {
            Debug.Log("Error: " + request.error);
        }
        else
        {
            Debug.Log("All OK");
            Debug.Log("Status Code: " + request.responseCode);

            var respuesta = request;
        }
        
    }


}