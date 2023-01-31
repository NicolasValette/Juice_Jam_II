using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData : ScriptableObject
{
    #region Variables
    [SerializeField]
    GameObject prefab;
    [SerializeField]
    int maxHitPoints = 20;
    [SerializeField]
    float lowMoveSpeed = 0.5f;
    [SerializeField]
    float mediumMoveSpeed = 1f;
    [SerializeField]
    float maxMoveSpeed = 1.5f;
    [SerializeField]
    float turboMoveSpeed = 1.5f;

    [Serializable]
    class WeaponsData
    {
        [SerializeField]
        int maxWeaponsAmount = 2;
        [SerializeField]
        List<WeaponData> weaponsList;
    }
    [SerializeField]
    WeaponsData weapons = new WeaponsData();

    #endregion Variables


}
