using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    private bool alradycheck = false;
    [SerializeField] private AudioSource checkSFX;
    [SerializeField] private AudioSource alreadycheckSFX;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag == "Player")
        {
            if (alradycheck == false)
            {
                alradycheck = true;
                checkSFX.Play();
                PlayerManager.lastCheckPointPos = transform.position;
            }
            else if (alradycheck == true)
            {
                alreadycheckSFX.Play();      
            }
            
        }
    }
}
