using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityRandom = UnityEngine.Random;

public class MusicManager : MonoBehaviour
{
    private static MusicManager instance = null;

    public static MusicManager Instance
    {
        get { return instance; }
    }

    private AudioSource audioSource;
    [SerializeField] private float tracktimer;
    [SerializeField] private float musicPlayed;
    [SerializeField] private bool[] beenPlayed;
    public AudioClip[] playList;

    public int[] indicesToDestroyMusicManager; // array of scene indices where MusicManager should be destroyed

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
        if(audioSource.isPlaying)
        {
            tracktimer += 1 * Time.deltaTime;
        }

        if (!audioSource.isPlaying || tracktimer >= audioSource.clip.length)
        {
            ChangeMusic(UnityRandom.Range(0, playList.Length));
        }

        restartRandomMusic();
    }

    void Awake()
    {
        //DontDestroyOnLoad(gameObject); // Don't destroy MusicManager when loading new scenes

        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        else
        {
            instance = this;
        }

        DontDestroyOnLoad(gameObject); // Don't destroy MusicManager when loading new scenes

    }

    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (Array.IndexOf(indicesToDestroyMusicManager, scene.buildIndex) != -1)
        {
            Destroy(gameObject); // Destroy MusicManager when entering a scene in indicesToDestroyMusicManager
        }   
    }

    private void ChangeMusic(int musicPicked)
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
                if ( i == playList.Length)
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
