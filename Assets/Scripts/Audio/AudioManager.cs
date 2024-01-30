using System;
using System.Collections;
using System.Collections.Generic;
using FMOD.Studio;
using UnityEngine;
using FMODUnity;
using STOP_MODE = FMOD.Studio.STOP_MODE;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance { get; private set; }

    private EventInstance musicEventInstance;

    public List<EventInstance> eventInstances;

    public bool playingMusic;
    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("More than one AudioManager in the scene.");
        }

        instance = this;
        eventInstances = new List<EventInstance>();
    }

    private void Start()
    {
        InitializeMusic(FMODEvents.instance.titleTheme);
    }

    public void PlayLevelOneShot(EventReference sound, Vector3 worldPos)
    {
        if (GameObject.FindWithTag("GameSystem").GetComponent<GameSystem>().ZoomStatus == GameSystem.Zoom.Level && worldPos.x>=-25)
        {
            RuntimeManager.PlayOneShot(sound, worldPos);
        }
    }
    


    public void InitializeMusic(EventReference musicEventReference)
    {
        musicEventInstance = CreateInstance(musicEventReference);
        musicEventInstance.start();
        playingMusic = true;
    }
    
    public void StopMusic(STOP_MODE stopMode)
    {
        musicEventInstance.stop(stopMode);
        playingMusic = false;
    }
    
    public void MusicSetPaused(bool pause)
    {
        musicEventInstance.setPaused(pause);
    }


    public EventInstance CreateInstance(EventReference eventReference)
    {
        EventInstance eventInstance = RuntimeManager.CreateInstance(eventReference);
        eventInstances.Add(eventInstance);
        return eventInstance;
    }

    public void SetMainThemeDampnessParameter(string parameterName, float parameterValue)
    {
        musicEventInstance.setParameterByName(parameterName, parameterValue);
    }
}
