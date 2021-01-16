using System;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "Settings/Bool Setting")]
public class BoolSetting : ScriptableObject
{
    public string key;
    [SerializeField] private bool _value;

    public void Load()
    {
        if (PlayerPrefs.HasKey(key))
        {
            Value = Convert.ToBoolean(PlayerPrefs.GetInt(key));
        }
        else
        {
            Value = false;
        }
    }
    public bool Value
    {
        get => _value;
        set
        {
            _value = value; 
            PlayerPrefs.SetInt(key, Convert.ToInt32(value));
            PlayerPrefs.Save();
            onValueChanged.Invoke();
        }
    }

    public UnityEvent onValueChanged;
}