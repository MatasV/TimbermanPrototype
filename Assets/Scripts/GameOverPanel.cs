using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverPanel : MonoBehaviour
{
    [SerializeField] private SharedBool isDead;
    [SerializeField] private GameObject panel;
    private void Awake()
    {
        TurnOnPanel();
        isDead.valueChangeEvent.AddListener(EvaluateChanges);
    }

    private void EvaluateChanges()
    {
        if (isDead == true)
        {
            TurnOnPanel();
        }
        else
        {
            TurnOffPanel();
        }
    }
    
    private void TurnOnPanel()
    {
        panel.SetActive(true);
    }

    private void TurnOffPanel()
    {
        panel.SetActive(false);
    }
}
