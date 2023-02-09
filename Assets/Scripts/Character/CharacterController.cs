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
    Animator animator;
    public Animator _animator { get { return animator; } }

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
        EnnemyController ennemyController = collision.transform.GetComponent<EnnemyController>();

        if (LifePoint > 0)
        {
            // verify if the collision is an entering ennemy bullet
            if (bulletController != null)
            {
                Debug.Log("LifePoint > 0 OnCollisionEnter bulletController");
                // Receive damages from the bullet                  
                ReceiveDamages(bulletController.GetDamages());
            } 
            else if(IsPlayer && ennemyController != null)
            {
                ReceiveDamages(ennemyController.enemyData._collisionDamages);
            }
        }
        else
        {
            // resurrection
        }
    }

    public void ReceiveDamages(int damages) // the object receives damages from a colliding ennemy bullet
    {
        Debug.Log("ReceiveDamages");
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
        if (IsPlayer)
        {
            animator.CrossFade("Die", 0.2f);
            GetComponent<AudioSource>()?.Play();
            EventManager.TriggerEvent(EventManager.Events.OnPlayerDeath);
            Invoke("Destroying", 2f);
            
        }
        else
        {
            if (HowDestroy == HowDestroyEnum.DestroyObject)
            {
                Destroying();
            }
            else if (HowDestroy == HowDestroyEnum.DisableComponent)
            {
                this.enabled = false;
            }
        }
        InstantiateFXForDestruction();
        PlayDestructionSound();
    }

    void Destroying()
    {
        Destroy(gameObject);
    }

    void InstantiateFXForDestruction()
    {
        if (enemyData != null && enemyData._fXSpawnerDestroyedObjPrefab != null)
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
            if (enemyData != null)           
                DestroyedSoundPlayer.PlayOneShot(enemyData._destroyedSound);
            else if (IsPlayer == true)
                DestroyedSoundPlayer.PlayOneShot(transform.GetComponent<PlayerController>()._playerData._destroyedSound);
        }

    }
    #endregion Damages & death
}
