using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CooldownDisplay : MonoBehaviour
{
    public GameObject containText;
    public PlayerMovement playermovement;
    private float dashcooldown;

    private bool timeActive = false;

    public TMP_Text coolDownText;
    // Start is called before the first frame update
    void Start()
    {
        dashcooldown = playermovement.dashingCooldown;
    }

    // Update is called once per frame
    void Update()
    {
        StartDisplay();
    }

    private void DeactivateText()
    {
        containText.SetActive(false);
        dashcooldown = playermovement.dashingCooldown;
    }

    private void StartDisplay()
    {
        if (playermovement.isDashing == true)
        {
            containText.SetActive(true);
            timeActive = true;
        }
        if (timeActive == true)
        {
            dashcooldown = dashcooldown - Time.deltaTime;
            TimeSpan time = TimeSpan.FromSeconds(dashcooldown);
            coolDownText.text = time.ToString(@"ss\:ff");
            if (dashcooldown <= 0)
            {
                timeActive = false;
                coolDownText.text = "Ready!";
                Invoke("DeactivateText", 0.5f);
            }
        }
    }

}
