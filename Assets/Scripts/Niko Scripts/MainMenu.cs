using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;
using TMPro;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField]
    private GameObject _menu;
    [SerializeField]
    private GameObject _shop;

    private void Start()
    {
        if (GameHandler.Instance.IsShopLevel)
        {
            _menu.SetActive(false);
            _shop.SetActive(true);
        }
        else
        {
            _menu.SetActive(true);
            _shop.SetActive(false);
        }
    }
    private void OnEnable()
    {
        EventManager.StartListening(EventManager.Events.OnBeatChange, ShakeMenu);
    }
    private void OnDisable()
    {
        EventManager.StopListening(EventManager.Events.OnBeatChange, ShakeMenu);
    }

    public void Quit()
    {
        Debug.Log("Quit");
        Application.Quit();
    }

    public void StartGame()
    {
        if (GameHandler.Instance.IsShopLevel || GameHandler.Instance.IsStartScreen)
        {
            _menu.SetActive(false);
            _shop.SetActive(true);
            GameHandler.Instance.IsStartScreen = false;
        }
        else
        {
            EventManager.StopListening(EventManager.Events.OnNoteHit, ShakeMenu);
            GameHandler.Instance.LoadNextLevel();
        }
       
    }
    public void ShakeMenu()
    {
        _menu.transform.DOShakeScale(0.75f, 0.5f);
        _menu.transform.DOShakeRotation(0.75f, 10f, 5);
        _menu.transform.DOShakePosition(0.75f);
        _shop.transform.DOShakeScale(0.75f, 0.5f);
        _shop.transform.DOShakeRotation(0.75f, 10f, 5);
        _shop.transform.DOShakePosition(0.75f);
    }
    public void PayToWin ()
    {
        if (GameHandler.Instance.goldAmount >= GameHandler.Instance.WinPrice)
        {
            EventManager.TriggerEvent(EventManager.Events.OnWin);
        }
        else
        {
            Debug.Log("PAUVRE");
            _shop.GetComponentInChildren<Text>().DOText("Not Enough Money, Go get more gold to buy your victory !\n You need 50 golds", 5f, true, ScrambleMode.All);
        }
    }
}
