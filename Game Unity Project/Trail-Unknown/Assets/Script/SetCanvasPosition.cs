using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetCanvasPosition : MonoBehaviour
{
    public GameObject showposition;
    // Update is called once per frame
    void Update()
    {
        transform.position = showposition.transform.position;
    }
}
