using UnityEngine;

public class gridInteg : MonoBehaviour
{
    public Vector2 gcord;
    public Vector2 Gcord
    {
        get
        {
            return gcord;
        }
        set
        {
            gcord = value;  
        }
    }
    //Placeholder
    public bool isOurs { get; set; }

    void Update()
    {
    }

}
