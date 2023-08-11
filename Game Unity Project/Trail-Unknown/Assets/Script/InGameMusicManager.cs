using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityRandom = UnityEngine.Random;

public class InGameMusicManager : MonoBehaviour
{
    private AudioSource audioSource;
    [SerializeField] private float tracktimer;
    [SerializeField] private float musicPlayed;
    [SerializeField] private bool[] beenPlayed;
    public AudioClip[] playList;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();

        beenPlayed = new bool[playList.Length];

        if (!audioSource.isPlaying)
        {
            ChangeMusic(UnityRandom.Range(0, playList.Length));
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (audioSource.isPlaying)
        {
            tracktimer += 1 * Time.deltaTime;
        }

        if (!audioSource.isPlaying || tracktimer >= audioSource.clip.length)
        {
            ChangeMusic(UnityRandom.Range(0, playList.Length));
        }

        restartRandomMusic();
    }

    public void ChangeMusic(int musicPicked)
    {
        if (!beenPlayed[musicPicked])
        {
            tracktimer = 0;
            musicPlayed++;
            beenPlayed[musicPicked] = true;
            audioSource.clip = playList[musicPicked];
            audioSource.Play();
        }
        else
        {
            audioSource.Stop();
        }

    }

    private void restartRandomMusic()
    {
        if (musicPlayed == playList.Length)
        {
            musicPlayed = 0;
            for (int i = 0; i < playList.Length; i++)
            {
                if (i == playList.Length)
                {
                    break;
                }
                else
                {
                    beenPlayed[i] = false;
                }
            }
        }
    }
}
