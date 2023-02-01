using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyData : CharacterData
{
    #region Variables
    [SerializeField]
    protected float stayDuration = 5f;
    public float _stayDuration { get { return stayDuration; } }

    #endregion Variables


}
