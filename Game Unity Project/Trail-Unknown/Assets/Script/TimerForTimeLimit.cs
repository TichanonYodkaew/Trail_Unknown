using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TimerForTimeLimit : MonoBehaviour
{
    bool timeActive = false;
    public float startTime;
    public TMP_Text currentTimeTextMesh;
    public PlayerLife playerLife;
    private float Timeleft;
    private bool hasDied = false; // flag to track whether the player has already died
    public FinishLineChallengModeTimeLimit finishLineChallengModeTimeLimit;
    // Start is called before the first frame update
    void Start()
    {
        StartTimer();
    }

    // Update is called once per frame
    void Update()
    {
        if (timeActive == true && !hasDied) // check if time is active and player hasn't died
        {
            startTime = startTime - Time.deltaTime;
            Debug.Log(startTime);
            TimeSpan time = TimeSpan.FromSeconds(startTime);
            currentTimeTextMesh.text = time.ToString(@"mm\:ss\:ff");
            Timeleft = startTime;
            DeathComing();
        }

        CheckDead();

    }

    private void StartTimer()
    {
        timeActive = true;
    }

    public void StopTimer()
    {
        timeActive = false;
        if (finishLineChallengModeTimeLimit.levelCompleted == true && Timeleft > 0)
        {
            PlayerPrefs.SetFloat("TimeLimitScore", Timeleft);
        }
        else if (Timeleft <= 0)
        {
            return;
        }
        
    }

    private void DeathComing()
    {
        if (startTime < 0 && !hasDied) // check if time has run out and player hasn't died
        {
            timeActive = false;
            currentTimeTextMesh.text = "Time out!";
            currentTimeTextMesh.color = Color.red;
            playerLife.Die();
            hasDied = true; // set flag to true to indicate player has died
        }
    }

    private void CheckDead()
    {
        if ( playerLife.isDie == true)
        {
            currentTimeTextMesh.text = "Oops!";
            currentTimeTextMesh.color = Color.red;
            StopTimer();
        }
    }
}

