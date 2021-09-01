using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DisplayAndChangeBoolSetting : MonoBehaviour
{
    public static float musicVolume { get; private set; }
    public static float SFXVolume { get; private set; }

    [SerializeField] private BoolSetting setting;
    [SerializeField] private TMP_Text settingsDisplay;

    private void Start()
    {
        setting.onValueChanged.AddListener(UpdateSettingDisplay);
        setting.Load();
    }

    public void OnMusicSliderValueChange()
    {
        if (setting.key == "Music" && !setting.Value) { musicVolume = 1; }
        else { musicVolume = 0.0001f; }

        FindObjectOfType<Camera>().GetComponent<AudioStarter>().audioManager.UpdateMixerVolume();
        ChangeSetting();
    }

    public void OnSFXSliderValueChange()
    {
        if (setting.key == "Sound" && !setting.Value) { SFXVolume = 1; }
        else { SFXVolume = 0.0001f; }
        
        FindObjectOfType<Camera>().GetComponent<AudioStarter>().audioManager.UpdateMixerVolume();
        ChangeSetting();
    }

    public void ChangeSetting()
    {
        setting.Value = !setting.Value;
    }

    private void UpdateSettingDisplay()
    {
        var valueText = setting.Value ? "On" : "Off";
        settingsDisplay.text = setting.key +":" + valueText;
    }

    private void OnDestroy()
    {
        setting.onValueChanged.RemoveListener(UpdateSettingDisplay);
    }
}
