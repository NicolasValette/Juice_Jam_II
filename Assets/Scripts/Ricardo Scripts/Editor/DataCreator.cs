using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class DataCreator : MonoBehaviour
{
    #region Game Content
    [MenuItem("Window/My Game/Data Creator/Create Weapon Data")]
    public static void CreateWeaponData()
    {
        WeaponData newData = new WeaponData();
        newData.name = "(rename me) New Weapon Data";

        Selection.activeObject = newData;

        string path = "Assets/Content/Datas/Weapons/" + newData.name+".asset";
        AssetDatabase.CreateAsset(newData, path);
    }

    [MenuItem("Window/My Game/Data Creator/Create Level Data")]
    public static void CreateLevelData()
    {
        LevelData newData = new LevelData();
        newData.name = "(rename me) New Level Data";

        Selection.activeObject = newData;

        string path = "Assets/Content/Datas/Levels/" + newData.name + ".asset";
        AssetDatabase.CreateAsset(newData, path);
    }

    [MenuItem("Window/My Game/Data Creator/Create Enemy Data")]
    public static void CreateEnemyData()
    {
        EnemyData newData = new EnemyData();
        newData.name = "(rename me) New Enemy Data";

        Selection.activeObject = newData;

        string path = "Assets/Content/Datas/Enemies/" + newData.name + ".asset";
        AssetDatabase.CreateAsset(newData, path);
    }

    [MenuItem("Window/My Game/Data Creator/Create Music Data")]
    public static void CreateMusicData()
    {
        MusicData newData = new MusicData();
        newData.name = "(rename me) New Music Data";

        Selection.activeObject = newData;

        string path = "Assets/Content/Datas/Musics/" + newData.name + ".asset";
        AssetDatabase.CreateAsset(newData, path);
    }

    [MenuItem("Window/My Game/Data Creator/Create Pet Data")]
    public static void CreatePetData()
    {
        PetData newData = new PetData();
        newData.name = "(rename me) New Pet Data";

        Selection.activeObject = newData;

        string path = "Assets/Content/Datas/Pets/" + newData.name + ".asset";
        AssetDatabase.CreateAsset(newData, path);
    }

    [MenuItem("Window/My Game/Data Creator/Create Pickup Data")]
    public static void CreatePickupData()
    {
        PickupData newData = new PickupData();
        newData.name = "(rename me) New Pickup Data";

        Selection.activeObject = newData;

        string path = "Assets/Content/Datas/Pickups/" + newData.name + ".asset";
        AssetDatabase.CreateAsset(newData, path);
    }
    #endregion Game Content

    #region Game Settings
    [MenuItem("Window/My Game/Data Creator/Create Player Data")]
    public static void CreatePlayerData()
    {
        PlayerData newData = new PlayerData();
        newData.name = "(rename me) New Player Data";

        Selection.activeObject = newData;

        string path = "Assets/Content/Datas/Game Settings/" + newData.name + ".asset";
        AssetDatabase.CreateAsset(newData, path);
    }

    [MenuItem("Window/My Game/Data Creator/Create UI Data")]
    public static void CreateUIData()
    {
        UIData newData = new UIData();
        newData.name = "(rename me) New UI Data";

        Selection.activeObject = newData;

        string path = "Assets/Content/Datas/Game Settings/" + newData.name + ".asset";
        AssetDatabase.CreateAsset(newData, path);
    }

    [MenuItem("Window/My Game/Data Creator/Create Game Data")]
    public static void CreateGameData()
    {
        GameData newData = new GameData();
        newData.name = "(rename me) New Game Data";

        Selection.activeObject = newData;

        string path = "Assets/Content/Datas/Game Settings/" + newData.name + ".asset";
        AssetDatabase.CreateAsset(newData, path);
    }
    #endregion Game Settings
}
