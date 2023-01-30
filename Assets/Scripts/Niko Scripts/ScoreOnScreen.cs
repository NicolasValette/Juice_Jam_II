using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreOnScreen : MonoBehaviour
{
    [SerializeField]
    private TMP_Text _scoreText;

    private int _goodNotes = 0;
    // Start is called before the first frame update
    void Start()
    {
        _scoreText.text = $"Correct Notes : {_goodNotes}";
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
        _scoreText.text = $"Correct Notes : {_goodNotes}";
    }

    public void HitNote()
    {
        _goodNotes++;
    }
}
