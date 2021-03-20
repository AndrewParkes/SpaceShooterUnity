using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void LoadGame()
    {
        SceneManager.LoadScene(1);
    }

    public void LoadGame2P()
    {
        SceneManager.LoadScene(2);
    }

    public void quit()
    {
        Application.Quit();
    }

    public void loadMenu()
    {
        SceneManager.LoadScene(0);
    }
}
