using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class MainMenu : MonoBehaviour
{
    [SerializeField]
    private GameObject _menu;
    private void OnEnable()
    {
        EventManager.StartListening(EventManager.Events.OnNoteHit, ShakeMenu);
    }
   
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
    public void ShakeMenu()
    {
        _menu.transform.DOShakeScale(0.75f, 0.5f);
        _menu.transform.DOShakeRotation(0.75f, 10f, 5);
        _menu.transform.DOShakePosition(0.75f);
    }
}
