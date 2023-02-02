using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyData : CharacterData
{
    #region Variables
    public enum MoveTypeEnum
    {
        RushOnPlayer,
        Strait
    }
    [SerializeField]
    protected MoveTypeEnum moveType = new MoveTypeEnum();
    public MoveTypeEnum _moveType { get { return moveType; } }

    [SerializeField]
    protected float stayDuration = 5f;
    public float _stayDuration { get { return stayDuration; } }
    
    [SerializeField]
    protected int collisionDamages = 5;
    public int _collisionDamages { get { return collisionDamages; } }
    #endregion Variables


}
