using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void Quit()
    {
        Debug.Log("Quit");
        Application.Quit();
    }

    public void StartGame()
    {
        RythmManager.Instance.Stop();
        SceneManager.LoadScene("WIP Scene Niko");
    }
}
