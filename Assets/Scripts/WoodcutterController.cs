using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WoodcutterController : MonoBehaviour
{
    [SerializeField] private SharedBool isDead;
    public SpriteRenderer spriteRenderer;
    [SerializeField] private WoodcutterDatabase database;
    private WoodcutterData woodcutterData;
    [SerializeField] private CharacterSide characterSide;
    private void Update()
    {
        if (isDead == true) return;

        spriteRenderer.sprite = woodcutterData.idleSprite;
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            spriteRenderer.sprite = woodcutterData.choppingSprite;
            Debug.Log("Mouse Pressed");
            if (Input.mousePosition.x < Screen.width / 2f) //Left 
            {
                transform.position = woodcutterData.leftSidePosition;
                transform.eulerAngles = Vector3.zero;
                characterSide.CurrentSide = CharacterSide.Side.Left;
            }
            else //Right
            {
                transform.position = woodcutterData.rightSidePosition;
                transform.eulerAngles = new Vector3(0, 180f, 0);
                characterSide.CurrentSide = CharacterSide.Side.Right;
            }
        }
    }
    private void OnEnable()
    {
        woodcutterData = database.ReturnCurrentAppearance().woodCutter;
        isDead.Value = false;
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = woodcutterData.idleSprite;
    }
}