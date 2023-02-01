using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UIElements;

[CustomEditor(typeof(GameData))]
public class GameManager_Inspector : Editor
{
    GameData manager;
    //   GameManagerData manager = (GameManagerData)target;
    // GameManagerData target = this;

    void OnEnable()
    {
        manager = (GameData)target;
    }
    public override void OnInspectorGUI()
    {
        if (GUILayout.Button("Detect All"))
        {
            manager.DetectAllDatas();
        }

        DrawDefaultInspector();
    }
}