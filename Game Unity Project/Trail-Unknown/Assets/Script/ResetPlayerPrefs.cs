using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetPlayerPrefs : MonoBehaviour
{
    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Delete) && Input.GetKeyDown(KeyCode.End))
        {
            PlayerPrefs.DeleteAll();
            PlayerPrefs.Save();
            audioSource.Play();
        }
    }
}
