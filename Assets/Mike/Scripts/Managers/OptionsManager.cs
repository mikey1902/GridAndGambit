using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionsManager : MonoBehaviour
{
    private AudioManager audioManager;

    public bool muteAudio = false;


    void Start()
    {
        audioManager = GameManager.Instance.AudioManager;
    }

    void Update()
    {
        
    }
}
