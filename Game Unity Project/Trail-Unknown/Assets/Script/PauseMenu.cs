using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool GameIsPause = false;

    public GameObject pauseMenuUI;    

    private int currentSceneIndex;

    public AudioSource audioSource;
    public AudioClip audioclip;

    public int[] sceneLevels;

    void Start()
    {
        currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if(GameIsPause)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    public void Resume()
    {
        audioSource.PlayOneShot(audioclip);
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPause = false;
    }

    void Pause()
    {
        audioSource.PlayOneShot(audioclip);
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPause= true;
    }

    public void LoadMenu()
    {
        CheckcSceneForSave();
        PlayerManager.lastCheckPointPos = new Vector2(0, 0);
        audioSource.PlayOneShot(audioclip);
        Time.timeScale = 1f;
        StartCoroutine(Delayloadscene("Menu"));
    }

    public void Quit()
    {
        CheckcSceneForSave();
        audioSource.PlayOneShot(audioclip);
        PlayerManager.lastCheckPointPos = new Vector2(0, 0);
        Time.timeScale = 1f;
        StartCoroutine(DelayQuit());
    }

    public void RestartLevel()
    {
        audioSource.PlayOneShot(audioclip);
        PlayerManager.lastCheckPointPos = new Vector2(0, 0);
        Time.timeScale = 1f;
        GameIsPause = false;
        StartCoroutine(DelayRestart());
    }

    private void CheckcSceneForSave()
    {
        if (sceneLevels.Contains(currentSceneIndex))
        {
            PlayerPrefs.SetInt("SaveLevel", currentSceneIndex);
            PlayerPrefs.Save();
        }
        else
        {
            return;
        }                
    }

    private IEnumerator Delayloadscene(string scenename)
    {
        yield return new WaitForSeconds(audioclip.length);
        SceneManager.LoadScene(scenename);
    }

    private IEnumerator DelayRestart()
    {
        yield return new WaitForSeconds(audioclip.length);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private IEnumerator DelayQuit()
    {
        yield return new WaitForSeconds(audioclip.length);
        Application.Quit();
    }
}
