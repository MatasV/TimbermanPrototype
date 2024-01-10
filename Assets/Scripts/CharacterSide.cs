using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu]
public class CharacterSide : ScriptableObject
{
    public enum Side
    {
        Left,
        Right
    }
    private Side _currentSide;
    public Side CurrentSide
    {
        get => _currentSide;
        set
        {
            _currentSide = value;
            valueChangeEvent.Invoke();
        }
    }
    public UnityEvent valueChangeEvent = new UnityEvent();
}
