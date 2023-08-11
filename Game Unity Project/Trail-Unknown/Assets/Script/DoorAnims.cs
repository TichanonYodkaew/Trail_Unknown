using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorAnims : MonoBehaviour
{
    Vector3 doorClosePos;
    Vector3 doorOpenPos;
    float doorSpeed = 1f;
    public bool isDoorOpen = false;

    public AudioSource doorSFX;
    public AudioSource doorFailSFX;

    [SerializeField] float moveHeigh = 1f;

    void Awake()
    {
        doorClosePos = transform.position;
        doorOpenPos = new Vector3(transform.position.x,transform.position.y + moveHeigh, transform.position.z);
    }

    void Update()
    {
        if (isDoorOpen)
        {
            OpenDoor();
        }
        else if (!isDoorOpen)
        {
            CloseDoor();
        }
    }

    public void OpenDoor()
    {
        if (transform.position != doorOpenPos)
        {
            doorSFX.Play();
            transform.position = Vector3.MoveTowards(transform.position, doorOpenPos, doorSpeed * Time.deltaTime);//
        }
    }

    public void CloseDoor()
    {
        if (transform.position != doorClosePos)
        {
            doorSFX.Play();
            transform.position = Vector3.MoveTowards(transform.position, doorClosePos, doorSpeed * Time.deltaTime);
        }
    }

    public void OpenFail()
    {
        doorFailSFX.Play();
        transform.position = doorClosePos;
    }
}
