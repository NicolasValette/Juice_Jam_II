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
    private GameObject _menu = null;
    [SerializeField]
    private GameObject _shop= null;
    [SerializeField]
    private GameObject _arrow = null;
    [SerializeField]
    private AudioClip _messageGoldClip;
    [SerializeField]
    private AudioClip _menuHover;
    [SerializeField]
    private GameObject _tuto = null;
    [SerializeField]
    private GameObject _spaceImage = null;

    private AudioSource _audioSource;
    private bool _isReadyToPlay;
    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        if (GameHandler.Instance.IsShopLevel)
        {
            _menu?.SetActive(false);
            _shop?.SetActive(true);
        }
        else
        {
            _menu?.SetActive(true);
            _shop?.SetActive(false);
        }
        if (!GameHandler.Instance.DisplayTutorial)
        {
            _tuto?.SetActive(false);
        }
    }
    private void OnEnable()
    {
        EventManager.StartListening(EventManager.Events.OnBeatChange, ShakeMenu);
        EventManager.StartListening(EventManager.Events.OnNoteHit, StartGame);
    }
    private void OnDisable()
    {
        EventManager.StopListening(EventManager.Events.OnBeatChange, ShakeMenu);
        EventManager.StopListening(EventManager.Events.OnNoteHit, StartGame);
    }

    public void Quit()
    {
        Debug.Log("Quit");
        PlaySelectOption();
        Application.Quit();
    }
    public void ReturnToMainScreen()
    {
        Debug.Log("Return");
        PlaySelectOption();
        GameHandler.Instance.ReturnMainScreen();
    }

    public void Return()
    {
        _menu?.SetActive(true);
        _shop?.SetActive(false);
        GameHandler.Instance.IsStartScreen = true;
    }

    public void PreloadMenu()
    {
        PlaySelectOption();
        if (GameHandler.Instance.IsShopLevel || GameHandler.Instance.IsStartScreen)
        {
            _menu?.SetActive(false);
            _shop?.SetActive(true);
            GameHandler.Instance.IsStartScreen = false;
        }
    }
    public void StartGame()
    {
        if (_isReadyToPlay && RythmManager.Instance.Combo>=2)
        {
            EventManager.StopListening(EventManager.Events.OnBeatChange, ShakeMenu);
            GameHandler.Instance.LoadNextLevel();
        }
    }

    public void ShakeMenu()
    {
        _menu?.transform.DOShakeScale(0.75f, 0.25f);
        _menu?.transform.DOShakeRotation(0.75f, 7f, 5);
        _menu?.transform.DOShakePosition(0.5f);
        _shop?.transform.DOShakeScale(0.75f, 0.5f);
        _shop?.transform.DOShakeRotation(0.75f, 7f, 5);
        _shop?.transform.DOShakePosition(0.5f);
        _tuto?.transform.DOShakeScale(0.75f, 0.5f);
        _tuto?.transform.DOShakeRotation(0.75f, 7f, 5);
        _tuto?.transform.DOShakePosition(0.5f);
        _spaceImage?.transform.DOShakeScale(0.75f, 0.75f);
        _spaceImage?.transform.DOShakeRotation(0.75f, 7f, 5);
        _spaceImage?.transform.DOShakePosition(0.5f);
        _arrow?.transform.DOShakeScale(0.75f, 0.25f);
        _arrow?.transform.DOShakePosition(0.5f, 5f);
    }
    public void PayToWin()
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
            _isReadyToPlay = true;
            _shop.GetComponentInChildren<Button>().enabled = false;
            _spaceImage.SetActive(true);
            _arrow.SetActive(true);
        }
    }
    public void PlaySelectOption()
    {
        _audioSource.clip = _menuHover;
        _audioSource.Play();
    }
}
