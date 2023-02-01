using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreOnScreen : MonoBehaviour
{
    [SerializeField]
    private TMP_Text _scoreText;
    [SerializeField]
    private TMP_Text _comboText;
    [SerializeField]
    private TMP_Text _songDurText;

    private int _goodNotes = 0;
    // Start is called before the first frame update
    void Start()
    {
        WriteScore();
    }
    private void OnEnable()
    {
        EventManager.StartListening(EventManager.Events.OnNoteHit, HitNote);
    }
    private void OnDisable()
    {
        EventManager.StopListening(EventManager.Events.OnNoteHit, HitNote);
    }

    // Update is called once per frame
    void Update()
    {
        WriteScore();
    }

    public void HitNote()
    {
        _goodNotes++;
    }

    private void WriteScore()
    {
        _scoreText.text = $"Correct Notes : {_goodNotes}\n" +
            $"Good notes : {RythmManager.Instance.Good}\n" +
            $"PERFECT : {RythmManager.Instance.Perfect}";
        _comboText.text = $"COMBO {RythmManager.Instance.Combo}";
        _songDurText.text = $"Song duration {(int)RythmManager.Instance._songPositionInSeconds} second";
    }
}
