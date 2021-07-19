using UnityEngine;

public class GyroManager : MonoBehaviour
{
    #region Instance
    private static GyroManager instance;
    public static GyroManager Instance
    {
        get
        {
            if(instance == null)
            {
                instance = FindObjectOfType<GyroManager>();
                if(instance == null)
                {
                    instance = new GameObject("Spawned GyroManager", typeof(GyroManager)).GetComponent<GyroManager>();
                }
            }

            return instance;
        }

        set
        {
            instance = value;
        }
    }
    #endregion
    private Gyroscope gyro;
    private Quaternion rotation;
    private bool gyroActivate;

   
    private void Update()
    {
        if (gyroActivate)
        {
            rotation = gyro.attitude;
        }
    }

    public Quaternion getGyroRotation()
    {
        return rotation;
    }

    public void enableGyro()
    {
        if (gyroActivate)
            return;

        if (SystemInfo.supportsGyroscope)
        {
            gyro = Input.gyro;
            gyro.enabled = true;
            gyroActivate = gyro.enabled;
        }
    }
}
