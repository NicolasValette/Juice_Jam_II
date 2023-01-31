using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerController : CharacterController
{
    [SerializeField]
    CharacterController characterController;

    [SerializeField]
    PlayerData playerData;

    [SerializeField]
    float actualSpeed = 1.0f;

    [SerializeField]
    GameObject WeaponSpawner;

    [SerializeField]
    GameObject WeaponSpawnedObject;

    // Start is called before the first frame update
    void Start()
    {
        actualSpeed = playerData._mediumMoveSpeed;
    }

    private void OnEnable()
    {
        SpawnActualWeapon(0);        
    }

    // Update is called once per frame
    void Update()
    {
        UpdateInput();
    }
    private void UpdateInput()
    {
        HandlePlayerMovment();
    }
    #region Movement
    void HandlePlayerMovment()
    {
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            transform.Translate(Vector3.back * (Time.deltaTime * actualSpeed));
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.Translate(Vector3.forward * (Time.deltaTime * actualSpeed));
        }

        if (Input.GetKey(KeyCode.UpArrow))
        {
            transform.Translate(Vector3.up * (Time.deltaTime * actualSpeed));
        }
        else if (Input.GetKey(KeyCode.DownArrow))
        {
            transform.Translate(Vector3.down * (Time.deltaTime * actualSpeed));
        }
    }
    #endregion Movement

    #region Weapon & fire
    private void RemoveActualWeapon()
    {
        if (WeaponSpawnedObject != null)
            Destroy(WeaponSpawnedObject);
    }

    void SpawnActualWeapon(int index)
    {
        WeaponSpawnedObject = (GameObject)Instantiate(playerData._weapons._weaponsList[index]._weaponPrefab, 
            WeaponSpawner.transform.position, WeaponSpawner.transform.rotation, WeaponSpawner.transform);
    }
    
    #endregion Weapon & fire
}
