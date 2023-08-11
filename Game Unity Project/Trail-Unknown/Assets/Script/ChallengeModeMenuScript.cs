using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChallengeModeMenuScript : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip audioclip;
    public void BacktoPlayMenu()
    {
        audioSource.PlayOneShot(audioclip);
        StartCoroutine(Delayloadscene("PlayMenu"));
    }

    public void PlayTimeUsed()
    {
        audioSource.PlayOneShot(audioclip);
        StartCoroutine(Delayloadscene("ChallengeModeTimeUsed"));
    }

    public void PlayTimeLimit()
    {
        audioSource.PlayOneShot(audioclip);
        StartCoroutine(Delayloadscene("ChallengeModeTimelimit"));
    }

    public void PlayTimeTrial()
    {
        audioSource.PlayOneShot(audioclip);
        StartCoroutine(Delayloadscene("ChallengeModeTimeTrial"));
    }

    public void LeaderboardTimeUsed()
    {
        audioSource.PlayOneShot(audioclip);
        StartCoroutine(Delayloadscene("LeaderboardTimeUsed"));
    }

    public void LeaderboardTimeLimit()
    {
        audioSource.PlayOneShot(audioclip);
        StartCoroutine(Delayloadscene("LeaderboardTimeLimit"));
    }

    public void LeaderboardTimeTrial()
    {
        audioSource.PlayOneShot(audioclip);
        StartCoroutine(Delayloadscene("LeaderboardTimeTrial"));
    }

    private IEnumerator Delayloadscene(string scenename)
    {
        yield return new WaitForSeconds(audioclip.length);
        SceneManager.LoadScene(scenename);
    }
}
