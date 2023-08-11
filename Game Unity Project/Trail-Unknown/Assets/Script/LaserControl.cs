using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserControl : MonoBehaviour
{
    public float onDuration = 1f; // The duration that the laser is on
    public float offDuration = 1f; // The duration that the laser is off
    public GameObject laserObject; // The laser object to turn on and off
    private bool isActive = false; // The laser's active state
    private float timer = 0f; // The timer to track the duration that the laser is on or off

    // Start is called before the first frame update
    void Start()
    {
        // Set the laser object to inactive
        laserObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        // Increment the timer
        timer += Time.deltaTime;

        // Check if the timer has reached the end of the on or off duration
        if (isActive && timer >= onDuration)
        {
            // Turn off the laser object
            ToggleLaser(false);
        }
        else if (!isActive && timer >= offDuration)
        {
            // Turn on the laser object
            ToggleLaser(true);
        }
    }

    void ToggleLaser(bool active)
    {
        // Toggle the laser's active state
        isActive = active;
        // Enable or disable the laser object based on the active state
        laserObject.SetActive(isActive);
        // Reset the timer
        timer = 0f;
    }
}
