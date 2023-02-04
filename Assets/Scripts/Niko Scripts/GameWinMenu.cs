using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;
using TMPro;
using UnityEngine.UI;

public class GameWinMenu : MonoBehaviour
{
    [SerializeField]
    private GameObject _menu = null;
    [SerializeField]
    private AudioClip _hoverClip;

    private AudioSource _audioSource;
    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
    }
    private void OnEnable()
    {
        EventManager.StartListening(EventManager.Events.OnBeatChange, ShakeMenu);
    }
    private void OnDisable()
    {
        EventManager.StopListening(EventManager.Events.OnBeatChange, ShakeMenu);
    }

    
    public void ReturnToMainScreen()
    {
        Debug.Log("Return");
        PlaySelectOption();
        GameHandler.Instance.ReturnMainScreen();
    }


    public void ShakeMenu()
    {
        _menu?.transform.DOShakeScale(0.75f, 0.25f);
        _menu?.transform.DOShakeRotation(0.75f, 7f, 5);
        _menu?.transform.DOShakePosition(0.5f);
       
    }
    public void PayToWin()
    {
     
    }
    public void PlaySelectOption()
    {
        _audioSource.clip = _hoverClip;
        _audioSource.Play();
    }
}
