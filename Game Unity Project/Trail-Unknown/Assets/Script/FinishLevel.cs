using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FinishLevel : MonoBehaviour
{
    public int nextLevel;
    public int nextSceneLoad;
    private bool levelCompleted = false;
    public GameObject passScreen;
    public AudioSource passAudioSource;

    void Start()
    {
        nextSceneLoad = SceneManager.GetActiveScene().buildIndex + 1;
    }

    // Update is called once per frame
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Player" && !levelCompleted)
        {
            passAudioSource.Play();
            ShowPassscreen();
            levelCompleted = true;
            PlayerManager.lastCheckPointPos = new Vector2(0, 0);

            if(SceneManager.GetActiveScene().buildIndex == 7)
            {               
                Debug.Log("You Win");
                PlayerPrefs.DeleteKey("SaveLevel");
                Invoke("CompleteGame", 2f);              
            }
            else
            {
                if(nextSceneLoad > PlayerPrefs.GetInt("levelAt"))
                {
                    PlayerPrefs.SetInt("levelAt",nextSceneLoad);
                }              
                Debug.Log("You pass Level: " + (nextSceneLoad - 1));
                Invoke("CompleteLevel", 2f);
            }
            
        }
    }

    private void CompleteLevel()
    {
        SceneManager.LoadScene("Level " + nextLevel.ToString());
    }

    private void CompleteGame()
    {
        SceneManager.LoadScene("Menu");
    }

    private void ShowPassscreen()
    {
        passScreen.SetActive(true);
    }
}
