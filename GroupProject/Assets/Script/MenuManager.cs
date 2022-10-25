using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class MenuManager : MonoBehaviour
{
    public void StartGame()
    {
        Debug.Log("Start game");
        SceneManager.LoadScene(2);
    }

    public void OpenSettingMenu()
    {
        Debug.Log("Turn to Setting Menu");
        SceneManager.LoadScene(1);
    }


    public void QuitGame()
    {
        Debug.Log("Quit Game!");
        Application.Quit();
    }

    public void OpenMainMenu()
    {
        Debug.Log("Turn to Main Menu");
        SceneManager.LoadScene(0);
    }
}
