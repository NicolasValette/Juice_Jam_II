using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Timeline;

public class MusicData : ScriptableObject
{
    #region Variables
    [SerializeField]
    AudioClip audioClip;
    public AudioClip _audioClip { get { return audioClip; } }

    [SerializeField]
    float bPM = 0f;
    public float _bPM { get { return bPM; } }

    [SerializeField]
    TimelineAsset timeLine;
    public TimelineAsset _timeLine { get { return timeLine; } }

    [Serializable]
    public class BeatData
    {
        [SerializeField]
        float timeCode = 0;
        public float _timeCode { get { return timeCode; } }

        [SerializeField]
        EventManager.Events eventAction = EventManager.Events.None;
        public EventManager.Events _eventAction { get { return eventAction; } }
    }

    [SerializeField]
    List<BeatData> beatsList;
    public List<BeatData> _beatsList { get{ return beatsList; } }

    #endregion Variables


}
