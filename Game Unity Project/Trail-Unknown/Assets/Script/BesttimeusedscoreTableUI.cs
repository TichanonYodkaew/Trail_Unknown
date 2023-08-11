using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BesttimeusedscoreTableUI : MonoBehaviour
{
    private Transform entryContainer;
    private Transform entryTemplate;
    private List<Transform> besttimescoreEntryTranformList;
    public TMP_Text showErrorText;
    public GameObject showErrorContainer;
    public GameObject LoadingUI;

    private void Awake()
    {
        entryContainer = transform.Find("besttimescoreEntryContainer");
        entryTemplate = entryContainer.Find("besttimescoreEntryTemplate");

        entryTemplate.gameObject.SetActive(false);

        APIrequests.Get("https://trailunknown-leaderboard.azurewebsites.net/api/GetLeaderboardTimeUsed?code=7dy_Xe3-sltHQvc1FcR0TByUS3YnhMlzeyo49BHl_Oe7AzFu07EC5g==",
                (string error) => {
                    Debug.Log("Error: " + error);
                    ShowError(error);
                },
                (string response) => {
                    Debug.Log("Response: " + response);
                    LeaderboardTimeUsed leaderboardTimeUsed = JsonConvert.DeserializeObject<LeaderboardTimeUsed>(response);
                    //Debug.Log(leaderboardTimeUsed.leaderboardTrailUnknownTimeUsedList[0].name);
                    ShowLeaderboard(leaderboardTimeUsed);
                });

    }

    private void CreateBesttimescoreEntryTransform(LeaderboardTrailUnknownTimeUsed leaderboardTrailUnknownTimeUsed, Transform container, List<Transform> transformsList)
    {
        float templateHeight = 60f;
        Transform entryTransform = Instantiate(entryTemplate, container);
        RectTransform entryRectTransform = entryTransform.GetComponent<RectTransform>();
        entryRectTransform.anchoredPosition = new Vector2(0, -templateHeight * transformsList.Count);
        entryRectTransform.gameObject.SetActive(true);

        int rank = transformsList.Count + 1;
        string rankString;
        switch (rank)
        {
            default:
                rankString = rank + "TH"; break;
            case 1: rankString = "1ST"; break;
            case 2: rankString = "2ND"; break;
            case 3: rankString = "3RD"; break;
        }

        entryTransform.Find("noText").GetComponent<Text>().text = rankString;

        string name = leaderboardTrailUnknownTimeUsed.name;
        entryTransform.Find("nameText").GetComponent<Text>().text = name;


        float score = leaderboardTrailUnknownTimeUsed.score;
        TimeSpan timescore = TimeSpan.FromSeconds(score);
        entryTransform.Find("scoreText").GetComponent<Text>().text = timescore.ToString(@"hh\:mm\:ss\:fff");

        // Set background visible odds and evens, easier to read
        entryTransform.Find("BGscore").gameObject.SetActive(rank % 2 == 1);

        if (rank == 1)
        {
            entryTransform.Find("noText").GetComponent<Text>().color = Color.green;
            entryTransform.Find("nameText").GetComponent<Text>().color = Color.green;
            entryTransform.Find("scoreText").GetComponent<Text>().color = Color.green;
        }

        else if (rank == 2)
        {
            entryTransform.Find("noText").GetComponent<Text>().color = Color.yellow;
            entryTransform.Find("nameText").GetComponent<Text>().color = Color.yellow;
            entryTransform.Find("scoreText").GetComponent<Text>().color = Color.yellow;
        }

        else if (rank == 3)
        {
            entryTransform.Find("noText").GetComponent<Text>().color = Color.red;
            entryTransform.Find("nameText").GetComponent<Text>().color = Color.red;
            entryTransform.Find("scoreText").GetComponent<Text>().color = Color.red;
        }


        transformsList.Add(entryTransform);
    }

    public void ShowLeaderboard(LeaderboardTimeUsed leaderboardTimeUsed)
    {
        if (leaderboardTimeUsed == null || leaderboardTimeUsed.leaderboardTrailUnknownTimeUsedList.Count == 0)
        {
            // Show a message to the user indicating that there is no data
            Debug.Log("No data available.");
            ShowError("No one pass this level for now.");
            return;
        }

        DisableLoadingUI();

        //Sort entry list by score
        for (int i = 0; i < leaderboardTimeUsed.leaderboardTrailUnknownTimeUsedList.Count; i++)
        {
            for (int j = i + 1; j < leaderboardTimeUsed.leaderboardTrailUnknownTimeUsedList.Count; j++)
            {
                if (leaderboardTimeUsed.leaderboardTrailUnknownTimeUsedList[j].score < leaderboardTimeUsed.leaderboardTrailUnknownTimeUsedList[i].score)
                {
                    //Swap
                    LeaderboardTrailUnknownTimeUsed tmp = leaderboardTimeUsed.leaderboardTrailUnknownTimeUsedList[i];
                    leaderboardTimeUsed.leaderboardTrailUnknownTimeUsedList[i] = leaderboardTimeUsed.leaderboardTrailUnknownTimeUsedList[j];
                    leaderboardTimeUsed.leaderboardTrailUnknownTimeUsedList[j] = tmp;
                }
            }
        }

        if (besttimescoreEntryTranformList != null)
        {
            foreach (Transform BesttimescoreEntryTransform in besttimescoreEntryTranformList)
            {
                Destroy(BesttimescoreEntryTransform.gameObject);
            }
        }

        besttimescoreEntryTranformList = new List<Transform>();
        foreach (LeaderboardTrailUnknownTimeUsed leaderboardTrailUnknownTimeUsed in leaderboardTimeUsed.leaderboardTrailUnknownTimeUsedList)
        {
            CreateBesttimescoreEntryTransform(leaderboardTrailUnknownTimeUsed, entryContainer, besttimescoreEntryTranformList);
        }
    }

    public void ShowError(string detailText)
    {
        DisableLoadingUI();
        showErrorContainer.SetActive(true);
        showErrorText.text = detailText;
    }

    public void DisableLoadingUI()
    {
        LoadingUI.SetActive(false);
    }
}
