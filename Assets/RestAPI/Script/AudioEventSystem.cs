using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class AudioEventSystem
{
    public static event Action<string> OnPlayAudio;
    public static event Action<string> OnStopAudio;


    public static void PlayAudio(string audioName)
    {
        if (OnPlayAudio != null)
        {
            OnPlayAudio(audioName);
        }
    }

    public static void StopAudio(string audioName)
    {
        if (OnStopAudio != null)
        {
            OnStopAudio(audioName);
        }
    }
}
