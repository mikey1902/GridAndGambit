using FMODUnity;
using UnityEngine;

public class FMODEvents : MonoBehaviour
{
    [field: Header("Card Hover SFX")]

    [field: SerializeField] public EventReference cardFlickSound { get; private set; }

    public static FMODEvents Instance { get; private set; }

    private void Awake()
    {
        Instance ??= this;
    }
}
