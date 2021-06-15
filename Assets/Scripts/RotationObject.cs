using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationObject : MonoBehaviour
{

    GameObject locationObject;
    Transform target;

    // Start is called before the first frame update
    void Start()
    {
        locationObject = GameObject.Find("LocationObject");

        if(locationObject != null)
        {
            target = locationObject.transform;
        }
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(target.position);
    }
}
