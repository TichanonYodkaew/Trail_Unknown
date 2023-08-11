using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System.Linq;

public class GameOverScreenMenu : MonoBehaviour
{
    public static bool GameIsPause = false;
    private int currentSceneIndex;
    public string[] cursewords;
    public TMP_Text topicgameOverUI;
    public AudioSource audioSource;
    public AudioClip audioclip;
    public int[] sceneLevels;
    public  bool gameIsPause = false;

    private void Start()
    {
        // log whatever comes out of the RandomWord string.
        string ShowNotAGoodWord = RandomWord();
        topicgameOverUI.text = ShowNotAGoodWord;
        currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
    }

    public void TryAgainLevel()
    {
        audioSource.PlayOneShot(audioclip);
        Time.timeScale = 1f;
        GameIsPause = false;
        gameIsPause= false;
        StartCoroutine(DelayRestart());
    }
    public void LoadMenu()
    {
        CheckcSceneForSave();
        audioSource.PlayOneShot(audioclip);
        PlayerManager.lastCheckPointPos = new Vector2(0, 0);
        GameIsPause = false;
        gameIsPause = false;
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

    private string RandomWord()
    {
        // Clone the cursewords array
        string[] shuffledWords = (string[])cursewords.Clone();

        // Shuffle the array using Fisher-Yates Shuffle algorithm
        for (int i = shuffledWords.Length - 1; i > 0; i--)
        {
            int randomIndex = Random.Range(0, i + 1);
            string temp = shuffledWords[i];
            shuffledWords[i] = shuffledWords[randomIndex];
            shuffledWords[randomIndex] = temp;
        }

        // Get the first word from the shuffled array
        string randomWord = shuffledWords[0];

        // return it (this will be the string that the script will use)
        return randomWord;
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
