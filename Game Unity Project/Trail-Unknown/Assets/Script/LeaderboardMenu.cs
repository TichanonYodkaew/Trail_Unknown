using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LeaderboardMenu : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip audioclip;
    public void BackToPlayMenu()
    {
        audioSource.PlayOneShot(audioclip);
        StartCoroutine(_BackToPlayMenu());
    }

    private IEnumerator _BackToPlayMenu()
    {
        yield return new WaitForSeconds(audioclip.length);
        SceneManager.LoadScene("ChallengeModeMenu");
    }
}
