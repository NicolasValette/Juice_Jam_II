using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShakeArrowToPlay : MonoBehaviour
{
    [SerializeField]
    private Image _arrow;
    [SerializeField]
    private Color _EndColor;
    [SerializeField]
    private Color _StartColor;
    [SerializeField]
    private TMP_Text _text;
    private void OnEnable()
    {
        EventManager.StartListening(EventManager.Events.OnBeatChange, TweenColor);
    }
    private void OnDisable()
    {
        EventManager.StopListening(EventManager.Events.OnBeatChange, TweenColor);
    }
    private void Update()
    {
        _text.text = RythmManager.Instance.Combo.ToString();
    }
    public void TweenColor()
    {

        _arrow.DOColor(_EndColor, RythmManager.Instance._secondePerBeat / 2).OnComplete(RevertTweenColor);
    }
    public void RevertTweenColor()
    {
        _arrow.DOColor(_StartColor, RythmManager.Instance._secondePerBeat / 2);
    }
    
}
