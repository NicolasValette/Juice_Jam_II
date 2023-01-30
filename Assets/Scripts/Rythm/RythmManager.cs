using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RythmManager : MonoBehaviour
{

    [SerializeField]
    private float _songBPM;                 //Beat per minute of the song.
    [SerializeField]
    private AudioSource musicSource;
    [SerializeField]
    private float _threshold;

    public float _secondePerBeat;          //Number of second for each beat.
    public float _songPositionInSeconds;   //Position in the active song, in seconds.
    public float _songPositionInBeat;      //Position in the active song, in beat.
    public float _dspSongTime;
    //Seconds since the start opf the songs

    public int _previousBeat;

    public static RythmManager Instance;
    // Start is called before the first frame update
    void Start()
    {
        Instance = this;
        InitAttributes();
    }

    // Update is called once per frame
    void Update()
    {
        _songPositionInSeconds = (float)AudioSettings.dspTime - _dspSongTime;
        _songPositionInBeat = _songPositionInSeconds / _secondePerBeat;
        if (_previousBeat + 1 <= _songPositionInBeat)
        {
            _previousBeat++;
            EventManager.TriggerEvent(EventManager.Events.OnBeatChange);
        }
    }

    private void InitAttributes()
    {
        musicSource = GetComponent<AudioSource>();

        _secondePerBeat = 60f / _songBPM;
        _dspSongTime = (float)AudioSettings.dspTime;
        musicSource.Play();
        _previousBeat = 0;
    
    }
    public bool IsNoteCorrectlyHit()
    {
        //Debug.Log("Pos : " + _songPositionInBeat + ", Treshold : " + _threshold + ", previous : " + _previousBeat +
        //    "\ntest : _songPositionInBeat >= _previousBeat - _threshold || _songPositionInBeat <= _previousBeat + _threshold = " +
        //    "\n "+_songPositionInBeat+" >= "+_previousBeat+ "-" +_threshold+" && "+_songPositionInBeat +"<=" +_previousBeat+" + "+_threshold +
        //    (_songPositionInBeat >= _previousBeat - _threshold && _songPositionInBeat <= _previousBeat + _threshold));



        
        return (_songPositionInBeat >= _previousBeat - _threshold && _songPositionInBeat <= _previousBeat + _threshold);
    }
}
