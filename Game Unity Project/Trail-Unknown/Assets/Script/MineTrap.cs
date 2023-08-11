using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MineTrap : MonoBehaviour
{

    public GameObject hitEffect;
    public GameObject blastArea;
    public AudioSource audioSource;
    public AudioClip warningSFX;
    private bool alradyHit = false;
    // Start is called before the first frame update
    void Start()
    {
        blastArea.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Player")
        {
            if (alradyHit == false)
            {
                alradyHit = true;
                audioSource.PlayOneShot(warningSFX);
                StartCoroutine(delayBomb());
            }         
        }
        else
        {
            return;
        }
    }

    private IEnumerator delayBomb()
    {
        yield return new WaitForSeconds(warningSFX.length);
        blastArea.SetActive(true);
        Instantiate(hitEffect, transform.position, transform.rotation);
        Destroy(this.gameObject);
    }

}
