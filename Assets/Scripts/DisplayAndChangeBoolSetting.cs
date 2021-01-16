using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DisplayAndChangeBoolSetting : MonoBehaviour
{
    [SerializeField] private BoolSetting setting;
    [SerializeField] private TMP_Text settingsDisplay;
    private void Start()
    {
        setting.onValueChanged.AddListener(UpdateSettingDisplay);
        setting.Load();
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
