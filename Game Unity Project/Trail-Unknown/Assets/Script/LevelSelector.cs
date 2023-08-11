using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelSelector : MonoBehaviour
{
    public int level;
    public Text levelText;
    public AudioSource audioSource;
    public AudioClip audioclip;
    // Start is called before the first frame update
    void Start()
    {
        levelText.text = level.ToString();
    }

    public void OpenLevel() {
        audioSource.PlayOneShot(audioclip);
        StartCoroutine(DelayOpenLevel());
    }

    private IEnumerator DelayOpenLevel()
    {
        yield return new WaitForSeconds(audioclip.length);
        SceneManager.LoadScene("Level " + level.ToString());
    }
}
