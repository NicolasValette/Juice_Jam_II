using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnnemyController : CharacterController
{
    #region Variables
    [HideInInspector]
    public Transform PlayerPosition;

    #endregion Variables

    private void OnEnable()
    {
        LifePoint = _enemyData._maxHitPoints;

        EventManager.StartListening(EventManager.Events.OnNoteHit, Explode);
        Invoke("Explode", _enemyData._stayDuration);
    }
    private void OnDisable()
    {
        EventManager.StopListening(EventManager.Events.OnNoteHit, Explode);
    }

    void Update()
    {
        if (_enemyData._moveType == EnemyData.MoveTypeEnum.RushOnPlayer)
            AimAndRushPlayer();
    }

    void AimAndRushPlayer()
    {
        Vector3 dest = Vector3.Normalize(PlayerPosition.position - transform.position);
        transform.Translate(dest * _enemyData._mediumMoveSpeed * Time.deltaTime);
    }

    private void OnCollisionEnter(Collision collision)
    {
     //   Debug.Log("collision");
        if (collision.gameObject.GetComponent<PlayerController>() != null)
        {
            Destroy(gameObject);
        }
    }

    public void Explode()
    {
        Debug.Log("Explode");
        ParticleSystem ps = GetComponent<ParticleSystem>();
        ps.Play();
        GetComponent<Renderer>().enabled = false;
        EventManager.StopListening(EventManager.Events.OnNoteHit, Explode);
        Destroy(gameObject, ps.main.duration);
    }
}
