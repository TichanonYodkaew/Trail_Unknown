using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishLineChallengModeTimeUsed : MonoBehaviour
{
    public bool levelCompleted = false;
    [SerializeField] private GameObject inputTextfieldUI;
    [SerializeField] private TimerForTimeUsed timer;
    [SerializeField] private GameObject player;
    [SerializeField] private AudioSource passSound;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Player" && !levelCompleted)
        {
            passSound.Play();
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
