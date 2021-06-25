using System;
using System.Collections.Generic;

[Serializable]
public class Tour
{
    private List<PointInterest>[] days;

    public List<PointInterest>[] Days { get => days; set => days = value; }

    public Tour()
    {
    }
}
