using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushObject : MonoBehaviour
{
    private bool checkisnearbox = false;
    private Animator anim;
    private PlayerMovement playermovement;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        playermovement= GetComponent<PlayerMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        CheckNearBox();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Box")
        {
            checkisnearbox = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Box")
        {
            checkisnearbox = false;
        }
    }

    private void CheckNearBox()
    {
        if (checkisnearbox && playermovement.dirX != 0f)
        {
            anim.SetBool("Push", true);
        }
        else
        {
            anim.SetBool("Push", false);
        }
    }

}
