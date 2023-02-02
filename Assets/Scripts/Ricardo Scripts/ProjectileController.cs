using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileController : MonoBehaviour
{
    #region Variable
    enum MoveSystemEnum { ByForce, ByTranslate }
    [SerializeField]
    private MoveSystemEnum moveSystem = new MoveSystemEnum();
    public enum FiredByEnum { Player, Enemy }
    [SerializeField]
    public FiredByEnum FiredBy = new FiredByEnum();

    [SerializeField]
    bool NoDestroyOnEnnemyCollision = false;

    [SerializeField]
    private bool explodeByItself = false;

    [SerializeField]
    private int damages = 1;

    [SerializeField]
    private float maxDistance = 1;

    [SerializeField]
    private float moveSpeed = 20;

    bool moving = true;

    [SerializeField]
    private float bounceForce = 20;

    Vector3 startPosition;

    Rigidbody _rigidBody;

    [SerializeField]
    private GameObject fXSpawnerFireObjPrefab;

  //  [SerializeField]
 //   private GameObject fXSpawnerProjectileObjPrefab;

    [SerializeField]
    private GameObject fXSpawnerImpactObjPrefab;

    //   [SerializeField]
    public GameObject emiter;
    public GameObject dirObj;
    Collision _collision;

    #endregion Variable

    void OnEnable()
    {
        startPosition = transform.position;
        _rigidBody = GetComponent<Rigidbody>();
        moving = true;
        PlayFireFXs();
    }


    private void Update()
    {
        if (moving) // the bullet has been shot and a force is applied to it
        {
            if (moveSystem == MoveSystemEnum.ByForce)
            {
                //apply a force to the bullet to move it
                _rigidBody.AddForce(transform.up * moveSpeed);
            }
            else if (moveSystem == MoveSystemEnum.ByTranslate)
            {
                transform.Translate(Vector3.up * (Time.deltaTime * moveSpeed));
            }
        }

        // get the distance between
        if (GetDistance() >= maxDistance) DestroySelf();
    }

    float GetDistance() // returns the distance between the Bullet and its starting position 
    {
        float distance = Vector3.Distance(startPosition, transform.position);

        return distance;
    }

    public int GetDamages() // returns the damages from the bullet to apply to the target
    {
        return damages;
    }

    void OnCollisionEnter(Collision collision) // when the bullet collides with an object
    {
        Debug.Log("OnCollisionEnter");
        if (collision.transform.tag != "NoBulletCollision")
        {
            _collision = collision;
            if (collision.transform.GetComponent<EnnemyController>() != null)
                collision.transform.GetComponent<EnnemyController>().ReceiveDamages(GetDamages());

            if ((collision.transform.GetComponent<EnnemyController>() == null
                || (collision.transform.GetComponent<EnnemyController>() && NoDestroyOnEnnemyCollision == false))
                || (collision.transform.GetComponent<EnnemyController>() == null
                || (collision.transform.GetComponent<EnnemyController>() && NoDestroyOnEnnemyCollision == false))
                || (collision.transform.GetComponent<EnnemyController>() == null
                || (collision.transform.GetComponent<EnnemyController>() && NoDestroyOnEnnemyCollision == false)))
            {
                // destroy the bullet after it to collide with anything     
                DestroySelf();
            }
        }
    }

    void ApplyExplosionForce(Vector3 _position, Transform _target) // Apply an Explosion Force at the collision point
    {
        // get the rigidbody of the collided object to apply the explosion force to

        if (_target != null)
        {
            Rigidbody otherRigidBody = _target.GetComponent<Rigidbody>();

            // apply the explosion force to the target rigidbody if relevant
            if (otherRigidBody != null)
                otherRigidBody.AddExplosionForce(bounceForce, _position, 5);
        }
    }

    #region FXs
    void PlayFireFXs()
    {
        if (fXSpawnerFireObjPrefab != null)
        {
            //   GameObject DirObj = emiter.GetComponent<BaseController>().GetFireFXDirectionObj();

            if (dirObj != null)
            {
                // Instantiate the particle system at the impact position
                GameObject spawner = Instantiate<GameObject>(fXSpawnerFireObjPrefab, startPosition,
                   dirObj.transform.rotation);

                // assign the origin position of the weapon fire               
                //   spawner.GetComponent<FXSpawner>().SourceObjPosition = DirObj.transform.position;


                spawner.GetComponent<FXSpawner>().startPosition = dirObj.transform.position;

                // launch the FXs system
                spawner.GetComponent<FXSpawner>().InitSystem(true);
            }
        }
    }

    void PlayImpactFXs(Vector3 _position)
    {
        if (fXSpawnerImpactObjPrefab != null)
        {
            // Instantiate the particle system at the impact position
            GameObject spawner = Instantiate<GameObject>(fXSpawnerImpactObjPrefab, _position, new Quaternion(0, 0, 0, 0));

            if (spawner != null && spawner.GetComponent<FXSpawner>() != null)
            {
                // assign the origin position of the weapon fire
                spawner.GetComponent<FXSpawner>().SourceObjPosition = startPosition;

                // launch the FXs system
                spawner.GetComponent<FXSpawner>().InitSystem(true);
            }
        }
    }
    #endregion FXs

    void DestroySelf() // Destroy the bullet by itself
    {
        if (explodeByItself || _collision != null)
        {
            if (_collision != null)
            {
                PlayImpactFXs(_collision.GetContact(0).point);

                // Apply an Explosion Force at the collision point
                ApplyExplosionForce(_collision.GetContact(0).point, _collision.transform);
            }
            else
            {
                PlayImpactFXs(transform.position);
                // Apply an Explosion Force at the collision point
                ApplyExplosionForce(transform.position, null);
            }
        }

        Destroy(gameObject);
    }
}
