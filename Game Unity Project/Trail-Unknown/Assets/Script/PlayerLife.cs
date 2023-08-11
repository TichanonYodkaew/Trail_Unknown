using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerLife : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator anim;
    [SerializeField] private GameObject blood;
    public GameObject gameOverUI;
    public bool isDie = false;
    [SerializeField] private AudioSource bloodSplashSFX;
    [SerializeField] private AudioSource screamSFX;
    

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Trap")) 
        {
            Die();
        }
    }

    private void OnTriggerEnter2D(Collider2D collisiontrigger)
    {
        if (collisiontrigger.gameObject.CompareTag("Trap"))
        {
            Die();
        }
    }

    public void Die()
    {
        isDie = true;      
        rb.bodyType = RigidbodyType2D.Static;
        anim.SetTrigger("death");
        Instantiate (blood,transform.position, Quaternion.identity);
        
    }

    public void PlaysoundBloodsplash()
    {
        transform.gameObject.tag = "Untagged";
        bloodSplashSFX.Play();
    }

    public void PlaysoundScream()
    {
        screamSFX.Play();
    }

    public void ShowGameOverScreen()
    {
        gameOverUI.SetActive(true);
        Invoke("PauseForShowGameOverScreen",5f);
    }

    public void PauseForShowGameOverScreen() 
    {
        Time.timeScale = 0f;
        GameOverScreenMenu.GameIsPause = true;
    }

}
