using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using Newtonsoft.Json;
public class InputNameTimLimitUI : MonoBehaviour
{
    [SerializeField] private TMP_InputField inputField;
    public TMP_Text timeScoreTextTMP;
    private string inputname;
    private float timeScore;
    private float timeScoreText;
    public GameObject errorUI;
    public TMP_Text errorText;
    public GameObject successUI;
    private void Update()
    {
        timeScoreText = PlayerPrefs.GetFloat("TimeLimitScore");
        TimeSpan timescoretext = TimeSpan.FromSeconds(timeScoreText);
        timeScoreTextTMP.text = timescoretext.ToString(@"hh\:mm\:ss\:ff");
    }

    public void EnterInput()
    {
        inputname = inputField.text;
        timeScore = PlayerPrefs.GetFloat("TimeLimitScore");
        Debug.Log("Player name : " + inputname + "  " + " Best time score : " + timeScore);
        UploadBesttimescore(timeScore, inputname);
    }

    private void UploadBesttimescore(float score, string name)
    {
        LeaderboardTrailUnknownTimeLimit leaderboardTrailUnknownTimeLimit = new LeaderboardTrailUnknownTimeLimit
        {
            name = name,
            score = score
        };

        APIrequests.PostJson("https://trailunknown-leaderboard.azurewebsites.net/api/AddScoreTimeLimit?code=KzL9SUblhWErj15dxD3ObmWD3AwRlXNjxp8wCQsiub8pAzFuwCfKeA==",
            JsonConvert.SerializeObject(leaderboardTrailUnknownTimeLimit),
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
        PlayerPrefs.DeleteKey("TimeLimitScore");
    }
}
