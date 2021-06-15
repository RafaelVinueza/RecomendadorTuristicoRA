using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoDestruir : MonoBehaviour
{

    private void Awake()
    {
        var noDestruirEntreEscenas = GameObject.Find("Controller");
        DontDestroyOnLoad(noDestruirEntreEscenas);
    }
}
