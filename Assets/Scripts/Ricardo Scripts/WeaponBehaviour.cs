using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponBehaviour : MonoBehaviour
{
    #region Variables

    [SerializeField]
    CharacterController characterController;

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

    private float _timeSinceLastShot;
    #endregion Variables

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void OnEnable()
    {
        //if (!Locked && weaponData._autoFire)
        //    InvokeRepeating("fireProjectile", 0, weaponData._fireRate);
        Unlock();
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("fire");
        if (!Locked)
        {
            StartCoroutine(fireProjectile2());
            Lock();
        }
    }
   


    public void Fire(CharacterController _characterController)
    {
        characterController = _characterController;
        Debug.Log("ALLO");
        if (Locked == false)
        {
            Locked = true;
            fireProjectile();
            Invoke("Unlock", weaponData._fireRate);
        }
    }

    public void ByPlayer(CharacterController _characterController)
    {
        characterController = _characterController;
    }
    public void Unlock()
    {
        Locked = false;
    }
    public void Lock()
    {
        Locked = true;
    }

    private void fireProjectile()
    {
        for (int i = 0; i < ProjectileSpawnersList.Count; i++)
        {
            InstantiateBulletPrefab(ProjectileSpawnersList[i]);
        }
    }
    private IEnumerator fireProjectile2()
    {
        Debug.Log("Fire");
        for (int i = 0; i < ProjectileSpawnersList.Count; i++)
        {
            InstantiateBulletPrefab(ProjectileSpawnersList[i]);
        }
        yield return new WaitForSeconds(60f/(weaponData._fireRate * GameHandler.Instance.ComboMultipl));
        Unlock();
    }

    protected void InstantiateBulletPrefab(ProjectileSpawnerData bulletSpawner) // create the bullet
    {
        if (bulletSpawner != null)
        {
            // Create a bullet and place it on the correct trajectory
            GameObject bullet = Instantiate<GameObject>(weaponData._projectilePrefab, bulletSpawner.BulletSpawner.transform.position, bulletSpawner.BulletSpawner.transform.rotation);
            ProjectileController bulletController = bullet.transform.GetComponent<ProjectileController>();
            bulletController.emiter = bulletSpawner.BulletSpawner;
            bulletController.dirObj = bulletSpawner.FireFXDirection;

            bullet.gameObject.SetActive(true);

            // display ammo count on the UI
            if (characterController._IsPlayer)
            {
                bulletController.FiredBy = ProjectileController.FiredByEnum.Player;
            }
            else bulletController.FiredBy = ProjectileController.FiredByEnum.Enemy;
        }
    }
}
