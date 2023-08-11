using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fix_Jump_Pad : MonoBehaviour
{
    private PlayerMovement playerMovement;
    private Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        playerMovement= GetComponent<PlayerMovement>();
        anim= GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "JumpPad")
        {
            playerMovement.canDoubleJump = true;
            anim.SetBool("DoubleJump", false);
        }
    }
}
