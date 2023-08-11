using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CloseSucess : MonoBehaviour
{
    public void CloseSuccessUI()
    {
        Time.timeScale = 1.0f;
        Debug.Log("load menu");
        SceneManager.LoadScene("Menu");
    }
}
