using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ShakeMenu : MonoBehaviour
{
    [SerializeField]
    private RectTransform _gold;
    [SerializeField]
    private RectTransform _combo;
    private void OnEnable()
    {
        EventManager.StartListening(EventManager.Events.OnGoldWin, ShakeGold);
        EventManager.StartListening(EventManager.Events.OnNoteHit, ShakeCombo);
    }
    private void OnDisable()
    {
        EventManager.StopListening(EventManager.Events.OnGoldWin, ShakeGold);
        EventManager.StopListening(EventManager.Events.OnNoteHit, ShakeCombo);
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void ShakeGold()
    {
        _gold.DOShakeScale(0.5f);
       
    }
    public void ShakeCombo()
    {
        _combo.DOShakeScale(0.5f);
    }
}
