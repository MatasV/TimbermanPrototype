using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu]
public class CharacterSide : ScriptableObject
{
    protected bool Equals(CharacterSide other)
    {
        return base.Equals(other) && _currentSide == other._currentSide && Equals(valueChangeEvent, other.valueChangeEvent);
    }

    public override bool Equals(object obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != this.GetType()) return false;
        return Equals((CharacterSide) obj);
    }

    public override int GetHashCode()
    {
        unchecked
        {
            int hashCode = base.GetHashCode();
            hashCode = (hashCode * 397) ^ (int) _currentSide;
            hashCode = (hashCode * 397) ^ (valueChangeEvent != null ? valueChangeEvent.GetHashCode() : 0);
            return hashCode;
        }
    }

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
    public static bool operator ==([NotNull] CharacterSide left, Side right) { return left.CurrentSide == right; }
    public static bool operator !=([NotNull] CharacterSide left, Side right) { return left.CurrentSide != right; }
}
