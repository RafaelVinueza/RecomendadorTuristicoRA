using UnityEngine;

public class ListPois : MonoBehaviour
{
    public string Name { get; set; }
    public float Distance { get; set; }

    public ListPois(string name, float distance)
    {
        Name = name;
        Distance = distance;
    }
}
