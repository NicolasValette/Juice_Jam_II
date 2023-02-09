using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;

public class Countdown : MonoBehaviour
{
    [SerializeField]
    private int _startCount = 3;
    [SerializeField]
    private TMP_Text _countdownText;
    [SerializeField]
    private Transform _countdownBox;
    [SerializeField]
    private float _strenght = 5f;
    int currentCount = 0;
    // Start is called before the first frame update
    void Start()
    {
        if (GameHandler.Instance.IsStartScreen || GameHandler.Instance.IsShopLevel || GameHandler.Instance.IsGameOver)
        {
            RythmManager.Instance.StartSong();
        }
        else
        {
            Debug.Log("Start");
            _countdownText.text = _startCount.ToString();
            DOVirtual.Int(0, 3, 3, UpdateCountdown).SetEase(Ease.Linear);
            _countdownBox.DOScale(2f, 0.4f).SmoothRewind();
            _countdownBox.DOPunchPosition(Vector3.up, 0.5f);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void UpdateCountdown(int count)
    {
        Debug.Log("Countdown : " + count);
        if (count > currentCount)
        {
            currentCount++;
            _countdownText.text = (_startCount - currentCount).ToString();
            //_countdownBox.DOScale(2f, 0.4f).SmoothRewind();
            _countdownBox.DOPunchPosition(Vector3.up * _strenght, 0.5f);
        }
        else if (currentCount == _startCount)
        {

            //_countdownBox.DOScale(2f, 0.4f).SmoothRewind();
            _countdownBox.DOShakeScale(0.5f).OnKill(DisableBox);
            RythmManager.Instance.StartSong();
        }
    }
    public void DisableBox()
    {
        _countdownBox.gameObject.SetActive(false);
    }
}
