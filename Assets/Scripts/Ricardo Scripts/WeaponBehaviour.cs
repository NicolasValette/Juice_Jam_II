using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponBehaviour : MonoBehaviour
{
    #region Variables

    [SerializeField]
    WeaponData weaponData;

    [SerializeField]
    bool Locked = true;

    [Serializable]
    public class ProjectileSpawnerData
    {
        public GameObject BulletSpawner;
        public GameObject FireFXDirection;
    }

    [SerializeField]
    protected List<ProjectileSpawnerData> ProjectileSpawnersList = new List<ProjectileSpawnerData>();

    #endregion Variables

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnEnable()
    {
        Locked = false;

        if (weaponData._autoFire)
            InvokeRepeating("fireProjectile", 0, weaponData._fireRate);
    }

    // Update is called once per frame
    void Update()
    {        

    }

    public void Fire()
    {
        if (Locked == false)
        {
            Locked = true;
            fireProjectile();
            Invoke("Unlock", weaponData._fireRate);
        }
    }

    void Unlock()
    {
        Locked = false;
    }

    private void fireProjectile()
    {
        for (int i = 0; i < ProjectileSpawnersList.Count; i++)
        {
            GameObject projectile = (GameObject)Instantiate(weaponData._projectilePrefab, 
                ProjectileSpawnersList[i].BulletSpawner.transform.position, ProjectileSpawnersList[i].BulletSpawner.transform.rotation);            
        }
    }
}
