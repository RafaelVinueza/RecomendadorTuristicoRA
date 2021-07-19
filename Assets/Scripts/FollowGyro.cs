using UnityEngine;

public class FollowGyro : MonoBehaviour
{

    private Quaternion baseRotation = new Quaternion(0, 0, 1, 0);
    
    void Start()
    {
        GyroManager.Instance.enableGyro();
    }
    
    void Update()
    {
        transform.localRotation = GyroManager.Instance.getGyroRotation() * baseRotation;
    }

    
}
