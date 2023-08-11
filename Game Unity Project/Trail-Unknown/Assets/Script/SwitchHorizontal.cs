using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class SwitchHorizontal : MonoBehaviour
{
    [SerializeField] DoorBehaviour doorBehaviour;
    [SerializeField] float switchMove;
    [SerializeField] bool isDoorOpenDoorSwitch;
    [SerializeField] bool isDoorCloseDoorSwitch;

    Vector3 switchUpPos;
    Vector3 switchDownPos;
    float switchSpeed = 1f;
    bool isPressingSwitch = false;

    // Start is called before the first frame update
    void Awake()
    {
        switchUpPos = transform.position;
        switchDownPos = new Vector3(transform.position.x - switchMove, transform.position.y, transform.position.z);
    }

    // Update is called once per frame
    void Update()
    {
        if (isPressingSwitch)
        {
            MoveSwitchDown();
        }
        else if (!isPressingSwitch)
        {
            MoveSwitchUp();
        }

    }

    void MoveSwitchDown()
    {
        if (transform.position != switchDownPos)
        {
            transform.position = Vector3.MoveTowards(transform.position, switchDownPos, switchSpeed * Time.deltaTime);
        }
    }

    void MoveSwitchUp()
    {
        if (transform.position != switchUpPos)
        {
            transform.position = Vector3.MoveTowards(transform.position, switchUpPos, switchSpeed * Time.deltaTime);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") || collision.CompareTag("Box") || collision.CompareTag("Grabbable"))
        {
            isPressingSwitch = true;

            if (isDoorOpenDoorSwitch && !doorBehaviour.isDoorOpen)
            {
                doorBehaviour.isDoorOpen = !doorBehaviour.isDoorOpen;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        isPressingSwitch = false;

        if (isDoorCloseDoorSwitch && doorBehaviour.isDoorOpen)
        {
            doorBehaviour.isDoorOpen = !doorBehaviour.isDoorOpen;
        }
    }
}
