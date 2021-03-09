using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{

    public void ExitGame()
    {
        GameObject.Find("Saver").GetComponent<SaveData>().SaveIntoJson();
        Application.Quit();
    }

    public void PlayGame()
    {
        SceneManager.LoadScene("Lvl1");
    }
}
