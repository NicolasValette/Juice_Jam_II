using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RythmManager : MonoBehaviour
{

    [SerializeField]
    private List<MusicData> _musics;
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
    private GameObject _GoldenNotePrefab;
    [SerializeField]
    private float _perfectThreshold = 0.05f;
    [SerializeField]
    private GameObject _goldPrefab;
    private int _musicIndex;
  

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
    public bool IsPlaying {get; private set;} = false;

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
    private int _countBeforeGolden = 0;
    public int CountBeforeGolden { get { return _countBeforeGolden;} }
    // Start is called before the first frame update
    void Awake()
    {
        Instance = this;
        InitAttributes();
        
    }

    // Update is called once per frame
    void Update()
    {
        if (IsPlaying)
        {
            PlayMusic();
        }
    }
    public void PlayMusic()
    {
        _songPositionInSeconds = (float)AudioSettings.dspTime - _songDspTime;
        _songPositionInBeat = _songPositionInSeconds / _secondePerBeat;
        if (_previousBeat + 1 <= _songPositionInBeat)
        {
            _previousBeat++;
            GameObject note;
            if (_countBeforeGolden >= GameHandler.Instance.GoldSpawn)
            {
                note = Instantiate(_GoldenNotePrefab, SpawnNotePos.position, SpawnNotePos.rotation);
                _countBeforeGolden = 0;
            }
            else
            {
                note = Instantiate(_notePrefab, SpawnNotePos.position, SpawnNotePos.rotation);
                _countBeforeGolden++;
            }
            if (_noteQueue.Count >= BeatsShown)
            {
                GameObject missedNote = _noteQueue.Dequeue();
                //missedNote.GetComponent<MoveNote>().Miss();
            }
            _noteQueue.Enqueue(note);
            Debug.Log("OnBeatChange");
            EventManager.TriggerEvent(EventManager.Events.OnBeatChange);
        }
        if (_eventInBeatListIndex < _musics[_musicIndex]._beatsList.Count && _musics[_musicIndex]._beatsList[_eventInBeatListIndex]._timeCode <= _songPositionInSeconds)
        {
            Debug.Log("beatlist++");
            EventManager.TriggerEvent(_musics[_musicIndex]._beatsList[_eventInBeatListIndex]._eventAction);
            _eventInBeatListIndex++;
        }
        if (_songTime < _songPositionInSeconds)
        {
            Debug.Log("EndSong");
            EventManager.TriggerEvent(EventManager.Events.EndSong);
        }
    }
    private void InitAttributes()
    {
        _musicIndex = Random.Range(0, _musics.Count);
        musicSource = GetComponent<AudioSource>();
        musicSource.clip = _musics[_musicIndex]._audioClip;
        _secondePerBeat = 60f / _musics[_musicIndex]._bPM;
        
        _previousBeat = 0;
        _noteQueue = new Queue<GameObject>();
    }
    public bool IsNoteCorrectlyHit()
    {
        
        if (!_noteQueue.TryPeek(out GameObject actualNote))
        {
            return false;
        }
        
        float noteBeat = actualNote.GetComponent<MoveNote>().BeatOfNote - 1f;        //Because the math is done 1 beat behind;
        Debug.Log("Pos : " + _songPositionInBeat + ", Treshold : " + _threshold + ", noteBeat : " + noteBeat +
            "\ntest : _songPositionInBeat >= noteBeat - _threshold && _songPositionInBeat <= noteBeat + _threshold = " +
            "\n " + _songPositionInBeat + " >= " + noteBeat + "-" + _threshold + " && " + _songPositionInBeat + "<=" + noteBeat + " + " + _threshold + " = " +
            (_songPositionInBeat >= noteBeat - _threshold && _songPositionInBeat <= noteBeat + _threshold));


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
            if (_actualCombo >= GameHandler.Instance.FirstfirstThresholdFire && GameHandler.Instance.ComboMultipl == 1)
            {
               GameHandler.Instance.ComboMultipl = 2;
               GameHandler.Instance.PowerUp();
            }
            if (_actualCombo >= GameHandler.Instance.SecondThresholdFire && GameHandler.Instance.ComboMultipl == 2)
            {
                GameHandler.Instance.ComboMultipl = 4;
                GameHandler.Instance.PowerUp();
            }
            _actualCombo++;
            actualNote.GetComponent<MoveNote>().Hit();
        }
        else
        {
            _actualCombo = 0;
            GameHandler.Instance.ComboMultipl = 1;
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
    public void StartSong()
    {
        _songDspTime = (float)AudioSettings.dspTime;
        _songTime = musicSource.clip.length;
        _songTimeInBeats = _songTime * _secondePerBeat;
        musicSource.Play();
        IsPlaying = true;
        EventManager.TriggerEvent(EventManager.Events.OnStartSong);
    }
    public void Stop()
    {
        IsPlaying = true;
        musicSource.Stop();
    }
    public void WinGold(Transform noteTransform)
    {
        if (GameHandler.Instance.IsGameOn)
        {
            Instantiate(_goldPrefab, noteTransform.position, noteTransform.rotation);
        }
    }
}
