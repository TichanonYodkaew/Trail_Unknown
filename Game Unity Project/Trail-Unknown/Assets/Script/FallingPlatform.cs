using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingPlatform : MonoBehaviour
{
    [SerializeField] private float fallDelay = 1f;
    [SerializeField] private float destroyDelay = 2f;

    [SerializeField] private Rigidbody2D rb;

    private Animator anim;
    private AudioSource audioSource;
    public GameObject bombEffect;

    private bool alradyHit = false;

    private void Start()
    {
        anim = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter2D(Collider2D collisionTrigger)
    {
        if (collisionTrigger.gameObject.CompareTag("Player"))
        {
            if (alradyHit == false)
            {
                alradyHit = true;
                audioSource.Play();
                anim.SetTrigger("Alert");
                StartCoroutine(Fall());
            }          
        }
        else
        {
            return;
        }
    }
    private IEnumerator Fall()
    {
        yield return new WaitForSeconds(fallDelay);
        rb.bodyType = RigidbodyType2D.Dynamic;
        DestroyThisObject();
    }

    private void DestroyThisObject()
    {
        Instantiate(bombEffect, transform.position, transform.rotation);
        Destroy(gameObject, destroyDelay);
    }
}
