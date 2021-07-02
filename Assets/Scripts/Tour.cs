using System;
using System.Collections.Generic;

[Serializable]
public class Tour
{
    private List<PointOfInterest>[] days;

    public List<PointOfInterest>[] Days { get => days; set => days = value; }

    public Tour()
    {
    }
}
