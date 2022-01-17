using UnityEngine;

public class POIData : MonoBehaviour
{
    public PointOfInterest data;

    public POIData(PointOfInterest data)
    {
        this.data = data;
    }

    public PointOfInterest getData()
    {
        return data;
    }

    public void setData(PointOfInterest data)
    {
        this.data = data;
    }

}
