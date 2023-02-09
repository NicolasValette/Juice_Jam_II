using UnityEngine;
using DG.Tweening;

public class ShakeMiddle : MonoBehaviour
{
    [SerializeField]
    private RectTransform _redBar;
    [SerializeField]
    private float _shakeScaleStrength = 0.75f;
    [SerializeField]
    private float _shakePosStrength = 10f;

    private void OnEnable()
    {
        EventManager.StartListening(EventManager.Events.OnBeatChange, ShakeScaleBar);
        EventManager.StartListening(EventManager.Events.OnNoteHit, ShakePositionBar);
    }
    private void OnDisable()
    {
        EventManager.StopListening(EventManager.Events.OnBeatChange, ShakeScaleBar);
        EventManager.StopListening(EventManager.Events.OnNoteHit, ShakePositionBar);
    }
    public void ShakeScaleBar()
    {
        _redBar.DOShakeScale(0.5f, _shakeScaleStrength);
    }
    public void ShakePositionBar()
    {
        _redBar.DOShakePosition(0.5f, _shakePosStrength);
    }
}
