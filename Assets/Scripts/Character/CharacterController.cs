using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public abstract class CharacterController : MonoBehaviour
{
    #region Variables
    [SerializeField]
    EnemyData enemyData;
    public EnemyData _enemyData { get { return enemyData; } }

    [SerializeField]
    bool IsPlayer = false;
    public bool _IsPlayer { get { return IsPlayer; } }

    [SerializeField]
    PlayerController playerController;

    [SerializeField]
    protected int MaxLifePoint = 1;

    [SerializeField]
    protected int LifePoint = 1;
    enum HowDestroyEnum { DestroyObject, DisableComponent }
    [SerializeField]
    private HowDestroyEnum HowDestroy = new HowDestroyEnum();

    [SerializeField]
    private GameObject DestroyedFXPositionObj;

    [SerializeField]
    private AudioSource DestroyedSoundPlayer;

    #endregion Variables

    private void Start()
    {

    }

    #region Damages & death
    void OnCollisionEnter(Collision collision) // object is collided by anther object, verify if the other is an ennemy bullet
    {
        ProjectileController bulletController = collision.transform.GetComponent<ProjectileController>();

        if(bulletController != null) Debug.Log("OnCollisionEnter bulletController");


        if (LifePoint > 0)
        {
            // verify if the collision is an entering ennemy bullet
            if (bulletController != null)
            {
                Debug.Log("LifePoint > 0 OnCollisionEnter bulletController");
                // Receive damages from the bullet                  
                ReceiveDamages(bulletController.GetDamages());
            }            
        }
        else
        {
            // resurrection
        }
    }

    protected void ReceiveDamages(int damages) // the object receives damages from a colliding ennemy bullet
    {
        // verify if the object is not invincible
        if (MaxLifePoint != -1)
        {
            // kill / destroy self at 0 life point left
            if (LifePoint - damages <= 0)
            {
                LifePoint = 0;

                if (playerController == null)
                    playerController = FindObjectOfType<PlayerController>();

                // verify if the object is a turret
                if (transform != null
                    && playerController != null
                    && playerController.transform != null
                    && playerController.transform != transform)
                {
                    // TODO invoke event

                //    TurretDestroyed?.Invoke();

                }
                DestroySelf();
            }
            // apply damages amount
            else
            {
                LifePoint = LifePoint - damages;
            }

            // TODO update UI about life points
        /*    if (IsPlayer)
                uiManager.SetLifePointsDisplay(LifePoint);
            else if (healthbar != null)
                healthbar.UpdateHealthBar(LifePoint);
        */
        }
    }

    void DestroySelf() // destroy itself and depending objects
    {
     /*   if (FiringParticleSystem != null)
            FiringParticleSystem.Stop();
        */

        if (HowDestroy == HowDestroyEnum.DestroyObject)
        {
            // destroy the object
         //   Destroy(CanonTurret);
            Destroy(gameObject);

        }
        else if (HowDestroy == HowDestroyEnum.DisableComponent)
        {
            // disable the object
            this.enabled = false;
        }

        InstantiateFXForDestruction();
        PlayDestructionSound();

        // Open the Win Menu if the Player's tank is destroyed 
        if (IsPlayer)
        {
            // TODO Event
        //    TankDestroyed?.Invoke();
        //  playerController.gameStarted = false;
        }
    }

    void InstantiateFXForDestruction()
    {
        if (enemyData._fXSpawnerDestroyedObjPrefab != null)
        {
            // Instantiate the particle system at the impact position
            GameObject spawner = Instantiate<GameObject>(enemyData._fXSpawnerDestroyedObjPrefab, DestroyedFXPositionObj.transform.position,
               DestroyedFXPositionObj.transform.rotation);
        }
    }

    void PlayDestructionSound()
    {
        if (DestroyedSoundPlayer != null)
        {
            DestroyedSoundPlayer.enabled = true;
            DestroyedSoundPlayer.Stop();
            DestroyedSoundPlayer.loop = false;
            DestroyedSoundPlayer.PlayOneShot(enemyData._destroyedSound);
        }

    }
    #endregion Damages & death
}
