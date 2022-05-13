using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{

    //This is the code used for my GUI.
    public void LoadGame()
    {
        SceneManager.LoadScene("GOL");
       
    }

    public void LoadMenu()
    {
        SceneManager.LoadScene("UserInterface");
    }
    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Quit!");

    }

}