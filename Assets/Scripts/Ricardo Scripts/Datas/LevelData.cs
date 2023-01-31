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
    List<EnemyData> normalEnemiesList;
    [SerializeField]
    List<EnemyData> DifficultEnemiesList;
    #endregion Variables


}
