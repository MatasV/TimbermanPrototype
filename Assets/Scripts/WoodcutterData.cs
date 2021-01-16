using System;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/Woodcutter Data")]
public class WoodcutterData : ScriptableObject
{
    public string woodcutterName;
    public Sprite idleSprite;
    public Sprite choppingSprite;
    public Vector2 leftSidePosition = new Vector2(-2f, -2.5f);
    public Vector2 rightSidePosition = new Vector2(2.5f, -2.5f);
}