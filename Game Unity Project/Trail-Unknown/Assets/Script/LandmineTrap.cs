using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LandmineTrap : MonoBehaviour
{
    public GameObject bombEffect;
    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Player")
        {
            Instantiate(bombEffect, transform.position, transform.rotation);
            Destroy(this.gameObject);
        }
    }
}
