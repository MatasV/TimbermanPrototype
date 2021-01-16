using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopController : MonoBehaviour
{
    [SerializeField] private WoodcutterDatabase woodcutterDatabase;
    [SerializeField] private HighScore highScore;
    
    [SerializeField] private Transform treeTransform;
    [SerializeField] private TMP_Text scoreRequiredText;
    [SerializeField] private TMP_Text woodCutterName;
    [SerializeField] private TMP_Text currentHighScoreText;
    [SerializeField] private SpriteRenderer woodCutterSprite;
    [SerializeField] private SpriteRenderer backgroundSprite;
    [SerializeField] private SpriteRenderer groundSprite;
    [SerializeField] private Button selectButton;
    
    private List<AppearanceData> appearanceData = new List<AppearanceData>();
    private int currentSelectedIndex = 0;
    private float lastYLocation = 0f;
    
    private void Start()
    {
        appearanceData = woodcutterDatabase.appearanceData.ToList();
        
        lastYLocation = treeTransform.position.y;
        
        currentSelectedIndex = woodcutterDatabase.selectedAppearaceIndex;
        DisplayAppearance(currentSelectedIndex);
    }

    public void Next()
    {
        if (currentSelectedIndex + 1 > woodcutterDatabase.appearanceData.Length - 1)
        {
            currentSelectedIndex = 0;
        }
        else
        {
            currentSelectedIndex++;
        }
        DisplayAppearance(currentSelectedIndex);
    }
    private void CalculateNextYLocation(GameObject obj)
    {
        var spriteRenderer = obj.GetComponent<SpriteRenderer>();
        lastYLocation += obj.transform.localScale.y * spriteRenderer.bounds.size.y * 2;
    }
    
    private void DisplayAppearance( int index)
    {
        var currentAppearanceData = woodcutterDatabase[index];
        
        (from Transform child in treeTransform select child.gameObject).ToList().ForEach(Destroy); //Clearing Children
        
        CalculateNextYLocation(Instantiate(currentAppearanceData.treeData.NeutralTrunk, new Vector3(0, lastYLocation, 0), Quaternion.identity, treeTransform));
        CalculateNextYLocation(Instantiate(currentAppearanceData.treeData.LeftTrunk, new Vector3(0, lastYLocation, 0), Quaternion.identity, treeTransform));
        CalculateNextYLocation(Instantiate(currentAppearanceData.treeData.RightTrunk, new Vector3(0, lastYLocation, 0), Quaternion.identity, treeTransform));
        CalculateNextYLocation(Instantiate(currentAppearanceData.treeData.NeutralTrunk, new Vector3(0, lastYLocation, 0), Quaternion.identity, treeTransform));
        lastYLocation = treeTransform.position.y;

        scoreRequiredText.text = "Score Required: " + currentAppearanceData.scoreRequired;
        currentHighScoreText.text = "(Current: " + highScore.MaxScore + ")";
        woodCutterName.text = currentAppearanceData.woodCutter.woodcutterName;

        woodCutterSprite.sprite = currentAppearanceData.woodCutter.idleSprite;
        backgroundSprite.sprite = currentAppearanceData.backgroundData.backGroundSprite;
        groundSprite.sprite = currentAppearanceData.backgroundData.groundSprite;

        selectButton.interactable = highScore.MaxScore >= currentAppearanceData.scoreRequired;
    }

    public void Select()
    {
        woodcutterDatabase.SetSelectedIndex(currentSelectedIndex);
    }
}
