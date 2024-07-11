using FMOD.Studio;
using FMODUnity;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [Header("Volume")]

    [Range(0f, 1f)]
    public float masterVolume = 1f;
    [Range(0f, 1f)]
    public float musicVolume = 1f;
    [Range(0f, 1f)]
    public float sFXVolume = 1f;

    private Bus masterBus;
    private Bus musicBus;
    private Bus sFXBus;

    private List<EventInstance> eventInstances;
    public static AudioManager Instance { get; private set; }

    private EventInstance backgroundMusicInstance;

    private void Awake()
    {
        Instance ??= this;

        eventInstances = new List<EventInstance>();

        masterBus = RuntimeManager.GetBus("bus:/");
        musicBus = RuntimeManager.GetBus("bus:/Music");
        sFXBus = RuntimeManager.GetBus("bus:/SFX");
    }

    private void Start()
    {
        InitializeBackgroundMusic(FMODEvents.Instance.BackgroundMusic);
    }

    private void Update()
    {
        masterBus.setVolume(masterVolume);
        musicBus.setVolume(musicVolume);
        sFXBus.setVolume(sFXVolume);
    }

    public void PlayOneShot(EventReference sound, Vector3 worldPos)
    {
        RuntimeManager.PlayOneShot(sound, worldPos);
    }

    public EventInstance CreateEventInstance(EventReference eventReference)
    {
        EventInstance eventInstance = RuntimeManager.CreateInstance(eventReference);
        eventInstances.Add(eventInstance);
        return eventInstance;
    }

    private void InitializeBackgroundMusic(EventReference backgroundMusic)
    {
        backgroundMusicInstance = CreateEventInstance(backgroundMusic);
        backgroundMusicInstance.start();
    }

    private void CleanUp()
    {
        foreach (EventInstance eventInstance in eventInstances) {
            eventInstance.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
            eventInstance.release();
        }
    }

    private void OnDestroy()
    {
        CleanUp();
    }

}
