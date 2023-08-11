using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using Newtonsoft.Json;

public class InputNameTimeTrialUI : MonoBehaviour
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
        timeScoreText = PlayerPrefs.GetFloat("TimeTrialScore");
        TimeSpan timescoretext = TimeSpan.FromSeconds(timeScoreText);
        timeScoreTextTMP.text = timescoretext.ToString(@"hh\:mm\:ss\:ff");
    }

    public void EnterInput()
    {
        inputname = inputField.text;
        timeScore = PlayerPrefs.GetFloat("TimeTrialScore");
        Debug.Log("Player name : " + inputname + "  " + " Best time score : " + timeScore);
        UploadBesttimescore(timeScore, inputname);
    }

    private void UploadBesttimescore(float score, string name)
    {
        LeaderboardTrailUnknownTimeTrial leaderboardTrailUnknownTimeTrial = new LeaderboardTrailUnknownTimeTrial
        {
            name = name,
            score = score
        };

        APIrequests.PostJson("https://trailunknown-leaderboard.azurewebsites.net/api/AddScoreTimeTrial?code=pj-lT00Y0jYVOGUlZRo5AYHF8_B1dCy5wECVw76MzvJ6AzFuopBMqg==",
            JsonConvert.SerializeObject(leaderboardTrailUnknownTimeTrial),
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
        PlayerPrefs.DeleteKey("TimeTrialScore");
    }
}
