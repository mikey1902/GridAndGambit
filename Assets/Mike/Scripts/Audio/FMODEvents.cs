using FMODUnity;
using UnityEngine;

public class FMODEvents : MonoBehaviour
{
    [field: Header("Card Hover SFX")]

    [field: SerializeField] public EventReference CardFlickSound { get; private set; }

    [field: Header("Background Music")]

    [field: SerializeField] public EventReference BackgroundMusic { get; private set; }

    public static FMODEvents Instance { get; private set; }

    private void Awake()
    {
        Instance ??= this;
    }
}
