using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyThisLandmine : MonoBehaviour
{
    public DisableActiveBlastArea disableActiveBlastArea;

    // Update is called once per frame
    void Update()
    {
        if (disableActiveBlastArea.checkIsActive == true)
        {
            Invoke("DestroyThisObject", 2.5f);
        }
    }

    private void DestroyThisObject()
    {
        Destroy(gameObject);
    }
}
