using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour{

    public AudioSource audioSource;
    public AudioClip audioclip;
    public void PlayGame()
    {
        audioSource.PlayOneShot(audioclip);
        StartCoroutine(DelayPlayGame());       
    }

    public void QuitGame()
    {
        Time.timeScale = 1f;
        audioSource.PlayOneShot(audioclip);
        StartCoroutine(DelayQuitGame());             
    }

    private IEnumerator DelayPlayGame()
    {
        yield return new WaitForSeconds(audioclip.length);
        SceneManager.LoadScene("PlayMenu");
    }

    private IEnumerator DelayQuitGame()
    {
        yield return new WaitForSeconds(audioclip.length);
        Application.Quit();
    }


}
