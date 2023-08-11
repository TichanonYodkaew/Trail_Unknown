using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TimerForTimeUsed : MonoBehaviour
{
    bool timeActive = false;
    public float currentTime;
    public TMP_Text currentTimeTextMesh;
    public float curTime;
    public PlayerLife playerLife;
    public FinishLineChallengModeTimeUsed finishLineChallengModeTimeUsed;

    // Start is called before the first frame update
    void Start()
    {
        currentTime = 0;
        StartTimer();
    }

    // Update is called once per frame
    void Update()
    {
        CheckDead();

        if (timeActive == true)
        {
            currentTime = currentTime + Time.deltaTime;
        }
        TimeSpan time = TimeSpan.FromSeconds(currentTime);
        currentTimeTextMesh.text = time.ToString(@"hh\:mm\:ss\:ff");
        curTime = currentTime;
    }

    private void StartTimer()
    {
        timeActive = true;
    }

    public void StopTimer()
    {
        timeActive = false;
        if (finishLineChallengModeTimeUsed.levelCompleted == true)
        { 
            PlayerPrefs.SetFloat("TimeScore", curTime);
        }
        else
        {
            return;
        }
            
    }

    private void CheckDead()
    {
        if (playerLife.isDie == true)
        {
            currentTimeTextMesh.color = Color.red;
            StopTimer();
        }
    }
}
