using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabObjectScript : MonoBehaviour
{   
    [SerializeField] private float grabDistance = 1f;
    [SerializeField] private LayerMask grabbableLayer;
    [SerializeField] private Transform holdPosition;
    [SerializeField] private float throwForce = 5f;

    private GameObject box;
    private bool isGrabbing = false;
    private bool canThrow = true;
    private RaycastHit2D hit;

    [SerializeField] private AudioSource grabSFX;
    [SerializeField] private AudioSource dropSFX;
    [SerializeField] private AudioSource throwSFX;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (isGrabbing)
            {               
                DropObject();
            }
            else
            {                
                GrabObject();
            }
        }

        if (Input.GetKeyDown(KeyCode.F))
        {
            if (isGrabbing && canThrow)
            {              
                ThrowObject();
            }
        }

    }

    private void FixedUpdate()
    {
        if (isGrabbing)
        {
            box.transform.position = holdPosition.position;
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position, transform.right * transform.localScale.x * grabDistance);
    }

    private void GrabObject()
    {       
        hit = Physics2D.Raycast(transform.position, transform.right * transform.localScale.x, grabDistance, grabbableLayer);
        if (hit.collider != null && hit.collider.gameObject.CompareTag("Grabbable"))
        {
            grabSFX.Play();
            box = hit.collider.gameObject;
            box.GetComponent<Rigidbody2D>().isKinematic = false;
            box.transform.SetParent(holdPosition);
            isGrabbing = true;
            canThrow = true;
        }
    }

    private void ThrowObject()
    {
        throwSFX.Play();
        box.GetComponent<Rigidbody2D>().isKinematic = false;
        box.transform.SetParent(null);
        box.GetComponent<Rigidbody2D>().AddForce(new Vector2(transform.localScale.x, 1) * throwForce, ForceMode2D.Impulse);
        box = null;
        isGrabbing = false;
        canThrow = false;
    }

    private void DropObject()
    {
        dropSFX.Play();
        box.GetComponent<Rigidbody2D>().isKinematic = false;
        box.transform.SetParent(null);
        box = null;
        isGrabbing = false;
        canThrow = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (isGrabbing && box != null && collision.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            Vector3 boxPos = new Vector3(transform.position.x + (0.5f * transform.localScale.x), transform.position.y, transform.position.z);
            box.transform.position = boxPos;
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (isGrabbing && box != null && collision.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            Vector3 boxPos = new Vector3(transform.position.x + (0.5f * transform.localScale.x), transform.position.y, transform.position.z);
            box.transform.position = boxPos;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (isGrabbing && box != null && collision.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            canThrow = true;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (isGrabbing && box != null && collision.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            Vector3 boxPos = new Vector3(transform.position.x + (0.5f * transform.localScale.x), transform.position.y, transform.position.z);
            box.transform.position = boxPos;
        }
    }
    
}
