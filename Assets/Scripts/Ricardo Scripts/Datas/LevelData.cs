using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelData : ScriptableObject
{
    #region Variables
    [SerializeField]
    MusicData musicData;

    [SerializeField]
    float difficultyMultiplicator = 1.0f;
   
    [SerializeField]
    List<EnemyPhaseData> progressionPhasesList;

    [Serializable]
    public class EnemyPhaseData
    {       
        [SerializeField]
        float normalEnemiesFrequency = 1.0f;
        public float _normalEnemiesFrequency { get { return normalEnemiesFrequency; } }

        [SerializeField]
        float normalEnemiesDuration = 5.0f;
        public float _normalEnemiesDuration { get { return normalEnemiesDuration; } }

        [SerializeField]
        float normalStayDuration = 5.0f;
        public float _normalStayDuration { get { return normalStayDuration; } }
    
        [SerializeField]
        List<EnemyData> normalEnemiesList;
        public List<EnemyData> _normalEnemiesList { get { return normalEnemiesList; } }

        [SerializeField]
        float difficultEnemiesFrequency = 1.0f;
        public float _difficultEnemiesFrequency { get { return difficultEnemiesFrequency; } }
     
        [SerializeField]
        float difficultEnemiesDuration = 5.0f;
        public float _difficultEnemiesDuration { get { return difficultEnemiesDuration; } }
     
        [SerializeField]
        float difficultStayDuration = 5.0f;
        public float _difficultStayDuration { get { return difficultStayDuration; } }
      
        [SerializeField]
        List<EnemyData> difficultEnemiesList;
        public List<EnemyData> _difficultEnemiesList { get { return difficultEnemiesList; } }

        [SerializeField]
        GameObject notePrefab;
        public GameObject _notePrefab { get { return notePrefab; } }
    }
      
    [Serializable]
    public class EndingData
    {
        [SerializeField]
        float difficultyMultiplicator = 1.5f;
        public float _difficultyMultiplicator { get { return difficultyMultiplicator; } }

        [SerializeField]
        EnemyData bossEnemy;
        public EnemyData _bossEnemy { get { return bossEnemy; } }

        [SerializeField]
        List<EnemyPhaseData> phasesList;
        public List<EnemyPhaseData> _phasesList { get { return phasesList; } }
    }
    [SerializeField]
    EndingData ending = new EndingData();
    public EndingData _ending { get { return ending; } }
    #endregion Variables


}
