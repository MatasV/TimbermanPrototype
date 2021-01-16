using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioStarter : MonoBehaviour
{
    [SerializeField] private AudioManager audioManager;
    [SerializeField] private string AudioName;
    [SerializeField] private bool IsMusic;
    [SerializeField] private bool onStart;
    private void Start()
    {
        if (!onStart) return;
        if(IsMusic) audioManager.PlayMusic(AudioName);
        else audioManager.PlaySound(AudioName);
    }

    public void PlaySound()
    {
        if(IsMusic) audioManager.PlayMusic(AudioName);
        else audioManager.PlaySound(AudioName);
    }
}
