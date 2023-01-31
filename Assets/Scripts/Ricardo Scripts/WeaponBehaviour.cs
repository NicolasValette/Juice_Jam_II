using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponBehaviour : MonoBehaviour
{
    #region Variables
    [SerializeField]
    List<GameObject> ProjectileSpawnersList= new List<GameObject>();

    [SerializeField]
    WeaponData weaponData;

    #endregion Variables

    // Start is called before the first frame update
    void Start()
    {
        if (weaponData._autoFire)
            InvokeRepeating("fireProjectile", 0, weaponData._fireRate);
    }

    private void OnEnable()
    {
        
    }

    // Update is called once per frame
    void Update()
    {        

    }

    public void Fire()
    {
       
    }

    private void fireProjectile()
    {
        for (int i = 0; i < ProjectileSpawnersList.Count; i++)
        {
            GameObject projectile = (GameObject)Instantiate(weaponData._projectilePrefab, 
                ProjectileSpawnersList[i].transform.position, ProjectileSpawnersList[i].transform.rotation, 
                ProjectileSpawnersList[i].transform);            
        }
    }
}
