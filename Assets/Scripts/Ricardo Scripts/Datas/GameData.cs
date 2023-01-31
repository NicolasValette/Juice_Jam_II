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
    [SerializeField]
    UIData uI;

    [Serializable]
    class ContentData
    {
        [SerializeField]
        List<LevelData> levelsList;
        [SerializeField]
        List<WeaponData> weaponsList;
        [SerializeField]
        List<PetData> petsList;
        [SerializeField]
        List<EnemyData> enemiesList;
    }
    [SerializeField]
    ContentData gameContent = new ContentData();

    #endregion Variables


}
