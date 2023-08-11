using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonustimeDisableActive : MonoBehaviour
{
    public float timeDeactive = 0.5f;

    private void Start()
    {
        gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (gameObject.activeSelf)
        {
            Invoke("DisableBonusTime",timeDeactive);
        }
    }

    private void DisableBonusTime()
    {
        gameObject.SetActive(false);
    }

}
