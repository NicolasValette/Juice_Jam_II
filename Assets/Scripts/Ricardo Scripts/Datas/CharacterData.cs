using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterData : ScriptableObject
{
    #region Variables
    [SerializeField]
    protected GameObject prefab;
    public GameObject _prefab { get { return prefab; } }

    [SerializeField]
    protected int maxHitPoints = 20;
    public int _maxHitPoints { get { return maxHitPoints; } }

    [SerializeField]
    protected float lowMoveSpeed = 0.5f;
    public float _lowMoveSpeed { get { return lowMoveSpeed; } }

    [SerializeField]
    protected float mediumMoveSpeed = 1f;
    public float _mediumMoveSpeed { get { return mediumMoveSpeed; } }

    [SerializeField]
    protected float maxMoveSpeed = 1.5f;
    public float _maxMoveSpeed { get { return maxMoveSpeed; } }

    [SerializeField]
    protected float turboMoveSpeed = 1.5f;
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
    protected WeaponsData weapons = new WeaponsData();
    public WeaponsData _weapons { get { return weapons; } }

    [SerializeField]
    protected GameObject fXSpawnerDestroyedObjPrefab;
    public GameObject _fXSpawnerDestroyedObjPrefab { get { return fXSpawnerDestroyedObjPrefab; } }
    
    [SerializeField]
    private AudioClip destroyedSound;
    public AudioClip _destroyedSound { get { return destroyedSound; } }
    #endregion Variables


}
