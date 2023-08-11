using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class QuickTryAgain : MonoBehaviour
{
    public GameObject gameOverScreenMenu;
    public AudioSource audioSource;
    public AudioClip audioclip;

    private bool isRestarting = false;

    void Update()
    {
        if (gameOverScreenMenu.activeSelf && !isRestarting)
        {
            if (gameOverScreenMenu.activeSelf)
            {
                if (Input.GetKeyDown(KeyCode.R))
                {
                    StartCoroutine(DelayedRestart());
                }
            }
        }
    }

    private IEnumerator DelayedRestart()
    {
        isRestarting = true;
        yield return new WaitForSeconds(0.5f);

        audioSource.PlayOneShot(audioclip);
        yield return new WaitForSeconds(audioclip.length);

        Time.timeScale = 1f;
        GameOverScreenMenu.GameIsPause = false;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);

        isRestarting = false;
    }
}
