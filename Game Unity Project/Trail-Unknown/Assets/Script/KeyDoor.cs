using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyDoor : MonoBehaviour
{

    [SerializeField] private Key.KeyType keyType;

    private DoorAnims doorAnims;

    private void Awake()
    {
        doorAnims = GetComponent<DoorAnims>();
    }

    public Key.KeyType GetKeyType()
    {
        return keyType;
    }

    public void OpenDoor()
    {
        doorAnims.isDoorOpen = !doorAnims.isDoorOpen;
    }

    public void OpenFail()
    {
        doorAnims.OpenFail();
    }

}
