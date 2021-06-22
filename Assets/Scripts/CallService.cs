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
        WWWForm form = new WWWForm();
        form.AddField("Text", JsonUtility.ToJson(datos));
        UnityWebRequest web = UnityWebRequest.Post("localhost:8080", form);
        yield return web.SendWebRequest();
        if (!web.isNetworkError && !web.isHttpError)
        {
            var datosRegreso = web;
        }
    }


}