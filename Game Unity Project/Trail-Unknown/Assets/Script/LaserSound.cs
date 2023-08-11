using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserSound : MonoBehaviour
{
    public AudioSource electricSFX;
    private void OnTriggerEnter2D(Collider2D collider)
    {
        // Check if the collider is the player object
        if (collider.gameObject.CompareTag("Player"))
        {
            Debug.Log("Player hit the laser trap!");
            // Add your code to kill the player here
            electricSFX.Play();
            // Kill the player

        }
    }
}
