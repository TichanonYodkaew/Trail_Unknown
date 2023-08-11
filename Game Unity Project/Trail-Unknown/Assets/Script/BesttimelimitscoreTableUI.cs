using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BesttimelimitscoreTableUI : MonoBehaviour
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

        APIrequests.Get("https://trailunknown-leaderboard.azurewebsites.net/api/GetLeaderboardTimeLimit?code=EmC1wHOduEFDrIq1Ny0V47oZJWw52uJ9-aPw9HZmaPkFAzFuBP0q7Q==",
                (string error) => {
                    Debug.Log("Error: " + error);
                    ShowError(error);
                },
                (string response) => {
                    Debug.Log("Response: " + response);
                    LeaderboardTimeLimit leaderboardTimeLimit = JsonConvert.DeserializeObject<LeaderboardTimeLimit>(response);
                    //Debug.Log(leaderboardTimeLimit.leaderboardTrailUnknownTimeLimitList[0].name);
                    ShowLeaderboard(leaderboardTimeLimit);
                });

    }

    private void CreateBesttimescoreEntryTransform(LeaderboardTrailUnknownTimeLimit leaderboardTrailUnknownTimeLimit, Transform container, List<Transform> transformsList)
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

        string name = leaderboardTrailUnknownTimeLimit.name;
        entryTransform.Find("nameText").GetComponent<Text>().text = name;


        float score = leaderboardTrailUnknownTimeLimit.score;
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

    public void ShowLeaderboard(LeaderboardTimeLimit leaderboardTimeLimit)
    {
        if (leaderboardTimeLimit == null || leaderboardTimeLimit.leaderboardTrailUnknownTimeLimitList.Count == 0)
        {
            // Show a message to the user indicating that there is no data
            Debug.Log("No data available.");
            ShowError("No one pass this level for now.");
            return;
        }

        DisableLoadingUI();

        //Sort entry list by score
        for (int i = 0; i < leaderboardTimeLimit.leaderboardTrailUnknownTimeLimitList.Count; i++)
        {
            for (int j = i + 1; j < leaderboardTimeLimit.leaderboardTrailUnknownTimeLimitList.Count; j++)
            {
                if (leaderboardTimeLimit.leaderboardTrailUnknownTimeLimitList[j].score > leaderboardTimeLimit.leaderboardTrailUnknownTimeLimitList[i].score)
                {
                    //Swap
                    LeaderboardTrailUnknownTimeLimit tmp = leaderboardTimeLimit.leaderboardTrailUnknownTimeLimitList[i];
                    leaderboardTimeLimit.leaderboardTrailUnknownTimeLimitList[i] = leaderboardTimeLimit.leaderboardTrailUnknownTimeLimitList[j];
                    leaderboardTimeLimit.leaderboardTrailUnknownTimeLimitList[j] = tmp;
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
        foreach (LeaderboardTrailUnknownTimeLimit leaderboardTrailUnknownTimeLimit in leaderboardTimeLimit.leaderboardTrailUnknownTimeLimitList)
        {
            CreateBesttimescoreEntryTransform(leaderboardTrailUnknownTimeLimit, entryContainer, besttimescoreEntryTranformList);
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
