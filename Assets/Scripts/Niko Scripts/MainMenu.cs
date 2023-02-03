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
    [SerializeField]
    private AudioClip _messageGoldClip;
    [SerializeField]
    private AudioClip _menuHover;

    private AudioSource _audioSource;

    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
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
        PlaySelectOption();
        Application.Quit();
    }

    public void Return()
    {
        _menu.SetActive(true);
        _shop.SetActive(false);
        GameHandler.Instance.IsStartScreen = true;
    }

    public void StartGame()
    {
        PlaySelectOption();
        if (GameHandler.Instance.IsShopLevel || GameHandler.Instance.IsStartScreen)
        {
            _menu.SetActive(false);
            _shop.SetActive(true);
            GameHandler.Instance.IsStartScreen = false;
        }
        else
        {
            EventManager.StopListening(EventManager.Events.OnBeatChange, ShakeMenu);
            GameHandler.Instance.LoadNextLevel();
        }
       
    }
    public void ShakeMenu()
    {
        _menu.transform.DOShakeScale(0.75f, 0.25f);
        _menu.transform.DOShakeRotation(0.75f, 7f, 5);
        _menu.transform.DOShakePosition(0.5f);
        _shop.transform.DOShakeScale(0.75f, 0.5f);
        _shop.transform.DOShakeRotation(0.75f, 7f, 5);
        _shop.transform.DOShakePosition(0.5f);
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
            _audioSource.clip = _messageGoldClip;
            _audioSource.Play();
            _shop.GetComponentInChildren<Text>().DOText("Not Enough Money, Go get more gold to buy your victory !\n You need 50 golds", 5f, true, ScrambleMode.All);
        }
    }
    public void PlaySelectOption()
    {
        _audioSource.clip = _menuHover;
        _audioSource.Play();
    }
}
