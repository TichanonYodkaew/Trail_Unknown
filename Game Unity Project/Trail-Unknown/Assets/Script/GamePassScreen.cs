using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GamePassScreen : MonoBehaviour
{
    public string[] greatwords; //= { "You died?", "Seriously?", "Oh come on!", "Sadly.", "Next time?", "Keep trying?", "Badday boi", "Again?", "GitGud", "You serious!", "You missed!", "Better Nextday" ,"Next again" ,"Need help?" ,"Some hint?" ,"Are you Okay?", "Cool mind.", "Oh! You missed.", "That was ...Awkward" ,"OMG!" , "Oh! Man..."};
    public TMP_Text greatTextTmp;

    // Start is called before the first frame update
    void Start()
    {
        int activeSceneIndex = SceneManager.GetActiveScene().buildIndex;
        if (activeSceneIndex == 7)
        {
            return;
        }
        else
        {
            string ShowAGoodWord = RandomWord();
            greatTextTmp.text = ShowAGoodWord;
        }
        
    }

    private string RandomWord()
    {
        // Clone the greatwords array
        string[] shuffledWords = (string[])greatwords.Clone();

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
}
