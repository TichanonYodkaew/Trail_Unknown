using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InputNameTimeUsedUI : MonoBehaviour
{
    public TMP_InputField inputField;
    public TMP_Text timeScoreTextTMP;
    private string inputname;
    private float timeScore;
    private float timeScoreText;
    public GameObject errorUI;
    public TMP_Text errorText;
    public GameObject successUI;

    private void Update()
    {
        timeScoreText = PlayerPrefs.GetFloat("TimeScore");
        TimeSpan timescoretext = TimeSpan.FromSeconds(timeScoreText);
        timeScoreTextTMP.text = timescoretext.ToString(@"hh\:mm\:ss\:ff");
    }

    public void EnterInput()
    {
        inputname = inputField.text;
        timeScore = PlayerPrefs.GetFloat("TimeScore");
        Debug.Log("Player name : " + inputname + "  " + " Best time score : " + timeScore);
        UploadBesttimescore(timeScore, inputname);
    }

    private void UploadBesttimescore(float score, string name)
    {
        LeaderboardTrailUnknownTimeUsed leaderboardTrailUnknownTimeUsed = new LeaderboardTrailUnknownTimeUsed
        {
            name = name,
            score = score
        };

        APIrequests.PostJson("https://trailunknown-leaderboard.azurewebsites.net/api/AddScoreTimeUsed?code=ii5mxhq2Wan2njss5rYnrGQwypPFXZc7B0H1EJf8PEFmAzFuAvRBlg==",
            JsonConvert.SerializeObject(leaderboardTrailUnknownTimeUsed),
            (string error) => {
                Debug.Log("Error: " + error);
                ShowErrorUI(error);
            },
            (string response) => {
                Debug.Log("Response: " + response);
                ShowSuccessUI();
            });
    }

    private void ShowErrorUI(string error)
    {
        errorUI.SetActive(true);
        errorText.text = error;
    }

    private void ShowSuccessUI()
    {
        successUI.SetActive(true);
        PlayerPrefs.DeleteKey("TimeScore");
    }
}
