using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class WoodcutterDatabase : ScriptableObject
{
    private const string PrefAppearanceKey = "SelectedCharacterIndex";
    public AppearanceData[] appearanceData;
    public int selectedAppearaceIndex;

    public AppearanceData ReturnCurrentAppearance()
    {
        if (PlayerPrefs.HasKey(PrefAppearanceKey))
        {
            selectedAppearaceIndex = PlayerPrefs.GetInt(PrefAppearanceKey);
        }
        return appearanceData[selectedAppearaceIndex];
    }

    public void SetSelectedIndex(int index)
    {
        selectedAppearaceIndex = index;
        PlayerPrefs.SetInt(PrefAppearanceKey, index);
        PlayerPrefs.Save();
    }

    public AppearanceData this[int index]
    {
        get => appearanceData[index];
        set => appearanceData[index] = value;
    }
}