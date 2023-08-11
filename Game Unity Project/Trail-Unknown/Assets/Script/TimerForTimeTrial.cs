using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class TimerForTimeTrial : MonoBehaviour
{
    bool timeActive = false;
    public float startTime;
    public TMP_Text currentTimeTextMesh;
    public PlayerLife playerLife;
    private float Timeleft;
    private bool hasDied = false; // flag to track whether the player has already died
    [SerializeField]private AudioSource collectItemSound;
    public FinishLineChallengModeTimeTrial finishLineChallengModeTimeTrial;

    private float timeScore = 0;
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
            startTime = startTime - Time.deltaTime; // calculate the player's remaining time after factoring in the item bonus   
            timeScore = timeScore + Time.deltaTime;
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
        if (finishLineChallengModeTimeTrial.levelCompleted == true && Timeleft > 0)
        {
            PlayerPrefs.SetFloat("TimeTrialScore", timeScore);
        }
        else if (Timeleft < 0)
        {
            return;
        }
       
    }

    public void IncreaseTime(float amount)
    {
        startTime += amount;
    }

    private void DeathComing()
    {
        if (startTime < 0f && !hasDied) // check if time has run out and player hasn't died
        {
            timeActive = false;
            currentTimeTextMesh.text = "Time out!";
            currentTimeTextMesh.color= Color.red;
            playerLife.Die();
            hasDied = true; // set flag to true to indicate player has died
        }
    }

    public void PlayCollectSound()
    {
        collectItemSound.Play();
    }

    private void CheckDead()
    {
        if (playerLife.isDie == true)
        {
            currentTimeTextMesh.text = "??:??:??";
            currentTimeTextMesh.color = Color.red;
            StopTimer();
        }
    }

}
