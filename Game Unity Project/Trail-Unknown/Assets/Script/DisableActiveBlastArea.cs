using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableActiveBlastArea : MonoBehaviour
{
    public float timeDeactive = 0.75f;
    public bool checkIsActive = false;

    // Update is called once per frame
    void Update()
    {
        if (gameObject.activeSelf)
        {
            checkIsActive= true;
            Invoke("DisableBlastArea", timeDeactive);
        }
    }

    private void DisableBlastArea()
    {
        gameObject.SetActive(false);
    }
}
