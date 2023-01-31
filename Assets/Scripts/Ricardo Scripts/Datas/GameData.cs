using OpenCover.Framework.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameData : ScriptableObject
{
    #region Variables
    [SerializeField]
    PlayerData player; 
    public PlayerData _player { get { return player; } }

    [SerializeField]
    UIData uI;
    public UIData _uI { get { return uI; } }


    [Serializable]
    public class ContentData
    {
        [SerializeField]
        List<LevelData> levelsList;
        public List<LevelData> _levelsList { get { return levelsList; } }

        [SerializeField]
        List<WeaponData> weaponsList;
        public List<WeaponData> _weaponsList { get { return weaponsList; } }

        [SerializeField]
        List<PetData> petsList;
        public List<PetData> _petsList { get { return petsList; } }

        [SerializeField]
        List<EnemyData> enemiesList;
        public List<EnemyData> _enemiesList { get { return enemiesList; } }

    }
    [SerializeField]
    ContentData gameContent = new ContentData();
    public ContentData _gameContent { get { return gameContent; } }

    #endregion Variables


}
