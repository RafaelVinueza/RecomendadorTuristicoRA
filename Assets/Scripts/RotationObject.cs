using UnityEngine;

public class RotationObject : MonoBehaviour
{

    private GameObject locationObject;
    private Transform target;
    
    void Start()
    {
        locationObject = GameObject.Find("LocationObject");
        if(locationObject != null)
        {
            target = locationObject.transform;
        }
    }
    
    void Update()
    {
        transform.LookAt(new Vector3(target.position.x, target.position.y, target.position.z));
    }
}
