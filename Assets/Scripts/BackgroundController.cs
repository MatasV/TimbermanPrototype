using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundController : MonoBehaviour
{
    [SerializeField] private WoodcutterDatabase database;

    [SerializeField] private SpriteRenderer backGround;
    [SerializeField] private SpriteRenderer ground;
    
    private BackgroundData backgroundData;
    private void Start()
    {
        backgroundData = database.ReturnCurrentAppearance().backgroundData;

        backGround.sprite = backgroundData.backGroundSprite;
        ground.sprite = backgroundData.groundSprite;
    }
}
