using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Windows;

public class FinishLineChallengModeTimeLimit : MonoBehaviour
{
    public bool levelCompleted = false;
    [SerializeField] private GameObject inputTextfieldUI;
    [SerializeField] private TimerForTimeLimit timer;
    [SerializeField] private GameObject player;
    [SerializeField] private AudioSource passSFX;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Player" && !levelCompleted)
        {
            passSFX.Play();
            player.SetActive(false);
            levelCompleted = true;
            timer.StopTimer();
            inputTextfieldUI.SetActive(true);
            pauseFortypeName();
        }
    }

    public void pauseFortypeName()
    {
        Time.timeScale = 0f;
    }

}
