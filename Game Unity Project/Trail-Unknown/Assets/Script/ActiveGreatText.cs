using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveGreatText : MonoBehaviour
{
    public GameObject containGreatText;

    public void ActiveText()
    {
        containGreatText.SetActive(true);
    }
}
