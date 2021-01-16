using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimerBarController : MonoBehaviour
{
    [SerializeField] private SharedBool isDead;
    [SerializeField] private SharedFloat timer;
    [SerializeField] private SharedInt currentScore;
    
    [SerializeField] private Image leftDisplayImage;
    [SerializeField] private Image rightDisplayImage;
    
    public float timeToDie = 2f;
    public float timeIncrement = 0f;

    private void Start()
    {
        currentScore.valueChangeEvent.AddListener(UpdateTimeIncrement);
        timer.Value = 0f;
    }

    private void UpdateTimeIncrement()
    {
        timer.Value = 0f;
        timeIncrement = Mathf.Sqrt(currentScore.Value)/5f;
    }

    private void OnDestroy()
    {
        currentScore.valueChangeEvent.RemoveListener(UpdateTimeIncrement);
    }

    private void Update()
    {
        if (isDead.Value) return;
        timer.Value += timeIncrement * Time.deltaTime ;

        leftDisplayImage.fillAmount = timer.Value / timeToDie;
        rightDisplayImage.fillAmount = timer.Value / timeToDie;

        if (timer.Value > timeToDie) isDead.Value = true;
    }
}
