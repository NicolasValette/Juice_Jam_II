using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RythmManager : MonoBehaviour
{

    [SerializeField]
    private MusicData _music;
    [SerializeField]
    private float _threshold;
    [SerializeField]
    public Transform SpawnNotePos;
    [SerializeField]
    public Transform RemoveNotePos;
    [SerializeField]
    public float BeatsShown { get; private set; } = 2f;
    [SerializeField]
    private GameObject _notePrefab;
    [SerializeField]
    private float _perfectThreshold = 0.05f;

    private AudioSource musicSource;
    private int _actualCombo = 0;
    public int Combo
    {
        get { return _actualCombo; }
    }
    private int _numberOfGood = 0;
    public int Good
    {
        get { return _numberOfGood; }
    }
    private int _numberOfPerfect = 0;
    public int Perfect
    {
        get { return _numberOfPerfect; }
    }

    public float _secondePerBeat;           //Number of second for each beat.
    public float _songPositionInSeconds;    //Position in the active song, in seconds.
    public float _songPositionInBeat;       //Position in the active song, in beat.
    public float _songDspTime;              //Time since the start of the song in seconds
    public float _songTime;
    public float _songTimeInBeats;

    public float _previousBeat;

    
    public static RythmManager Instance;
    private Queue<GameObject> _noteQueue;
    private int _eventInBeatListIndex = 0;
    // Start is called before the first frame update
    void Start()
    {
        Instance = this;
        InitAttributes();
        
    }

    // Update is called once per frame
    void Update()
    {
        _songPositionInSeconds = (float)AudioSettings.dspTime - _songDspTime;
        _songPositionInBeat = _songPositionInSeconds / _secondePerBeat;
        if (_previousBeat + 1 <= _songPositionInBeat)
        {
            _previousBeat++;
            GameObject note;
            note = Instantiate(_notePrefab, SpawnNotePos.position, SpawnNotePos.rotation);
            if (_noteQueue.Count >= BeatsShown)
            {
                GameObject missedNote = _noteQueue.Dequeue();
                missedNote.GetComponent<MoveNote>().Miss();
            }
            _noteQueue.Enqueue(note);
            EventManager.TriggerEvent(EventManager.Events.OnBeatChange);
        }
        if (_eventInBeatListIndex < _music._beatsList.Count && _music._beatsList[_eventInBeatListIndex]._timeCode <= _songPositionInSeconds)
        {
            Debug.Log("beatlist++");
            EventManager.TriggerEvent(_music._beatsList[_eventInBeatListIndex]._eventAction);
            _eventInBeatListIndex++;
        }    
    }

    private void InitAttributes()
    {
        musicSource = GetComponent<AudioSource>();
        musicSource.clip = _music._audioClip;
        _secondePerBeat = 60f / _music._bPM;
        _songDspTime = (float)AudioSettings.dspTime;
        _songTime = musicSource.clip.length;
        _songTimeInBeats = _songTime * _secondePerBeat;

        musicSource.Play();
        _previousBeat = 0;
        _noteQueue = new Queue<GameObject>();
    }
    public bool IsNoteCorrectlyHit()
    {
        GameObject actualNote = _noteQueue.Peek();
        float noteBeat = actualNote.GetComponent<MoveNote>().BeatOfNote - 1f;        //Because the math is done 1 beat behind;
        //Debug.Log("Pos : " + _songPositionInBeat + ", Treshold : " + _threshold + ", previous : " + _previousBeat +
        //    "\ntest : _songPositionInBeat >= _previousBeat - _threshold || _songPositionInBeat <= _previousBeat + _threshold = " +
        //    "\n "+_songPositionInBeat+" >= "+_previousBeat+ "-" +_threshold+" && "+_songPositionInBeat +"<=" +_previousBeat+" + "+_threshold +
        //    (_songPositionInBeat >= _previousBeat - _threshold && _songPositionInBeat <= _previousBeat + _threshold));


        //Debug.Log("Prev : " + _previousBeat);
        //Debug.Log("note : " + noteBeat);
        //Debug.Log("pos : " + _songPositionInBeat);
        bool isCorrect = (_songPositionInBeat >= noteBeat - _threshold && _songPositionInBeat <= noteBeat + _threshold);
        bool isGood = (_songPositionInBeat >= noteBeat - (_threshold/2f) && _songPositionInBeat <= noteBeat + (_threshold/2f));
        bool isPerfect = (_songPositionInBeat >= noteBeat - _perfectThreshold && _songPositionInBeat <= noteBeat + _perfectThreshold);
        //Debug.Log("correct : " + isCorrect);
        //Debug.Log("Good : " + isGood);
        //Debug.Log("correct : " + isPerfect);
        //Debug.Log("__________________");
        if (isCorrect)
        {
            _actualCombo++;
            actualNote.GetComponent<MoveNote>().Hit();
        }
        else
        {
            _actualCombo = 0;
        }
        if  (isGood)
        {
            _numberOfGood++;
        }
        if (isPerfect)
        {
            _numberOfPerfect++;
        }
        
        // return (_songPositionInBeat >= _previousBeat - _threshold && _songPositionInBeat <= _previousBeat + _threshold);
        return isCorrect;
    }
}
