using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterData : ScriptableObject
{
    #region Variables
    [SerializeField]
    GameObject prefab;
    public GameObject _prefab { get { return prefab; } }

    [SerializeField]
    int maxHitPoints = 20;
    public int _maxHitPoints { get { return maxHitPoints; } }

    [SerializeField]
    float lowMoveSpeed = 0.5f;
    public float _lowMoveSpeed { get { return lowMoveSpeed; } }

    [SerializeField]
    float mediumMoveSpeed = 1f;
    public float _mediumMoveSpeed { get { return mediumMoveSpeed; } }

    [SerializeField]
    float maxMoveSpeed = 1.5f;
    public float _maxMoveSpeed { get { return maxMoveSpeed; } }

    [SerializeField]
    float turboMoveSpeed = 1.5f;
    public float _turboMoveSpeed { get { return turboMoveSpeed; } }

    [Serializable]
    public class WeaponsData
    {
        [SerializeField]
        int maxWeaponsAmount = 2;
        public int _maxWeaponsAmount { get { return maxWeaponsAmount; } }

        [SerializeField]
        List<WeaponData> weaponsList;
        public List<WeaponData> _weaponsList { get { return weaponsList; } }
    }
    [SerializeField]
    WeaponsData weapons = new WeaponsData();
    public WeaponsData _weapons { get { return weapons; } }
    #endregion Variables


}
