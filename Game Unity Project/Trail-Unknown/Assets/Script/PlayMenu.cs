using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayMenu : MonoBehaviour
{
    private int sceneToContinue;
    public AudioSource audioSource;
    public AudioClip audioclip;
    public AudioClip audioclipFailsound;
    public TMP_Text continuebutton;

    private void Start()
    {
        sceneToContinue = PlayerPrefs.GetInt("SaveLevel");

        if(sceneToContinue == 0)
        {
            Color alpha = continuebutton.color;
            alpha.a = 0.5f;
            continuebutton.color = alpha;
        }
    }
    public void NewGame()
    {
        PlayerPrefs.DeleteAll();
        audioSource.PlayOneShot(audioclip);
        StartCoroutine(Delayloadscene("Level 1"));
    }

    public void BackMenu()
    {
        audioSource.PlayOneShot(audioclip);
        StartCoroutine(Delayloadscene("Menu"));
    }

    public void ContinueGame()
    {       
        if (sceneToContinue != 0)
        {
            audioSource.PlayOneShot(audioclip);
            StartCoroutine(DelayContinueGame());
        }

        else
        {
            audioSource.PlayOneShot(audioclipFailsound);
            return;
        }
    }

    public void ChallengeMode()
    {
        audioSource.PlayOneShot(audioclip);
        StartCoroutine(Delayloadscene("ChallengeModeMenu"));
    }

    private IEnumerator DelayContinueGame()
    {
        yield return new WaitForSeconds(audioclip.length);
        SceneManager.LoadScene("Level " + sceneToContinue.ToString());
    }

    private IEnumerator Delayloadscene(string scenename)
    {
        yield return new WaitForSeconds(audioclip.length);
        SceneManager.LoadScene(scenename);
    }
}
