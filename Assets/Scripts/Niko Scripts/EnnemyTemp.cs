using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnnemyTemp : MonoBehaviour
{
    [HideInInspector]
    public Transform PlayerPosition;
    [SerializeField]
    private float _speed;
    
    // Start is called before the first frame update
    void Start()
    {
       
    }
    private void OnEnable()
    {
        EventManager.StartListening(EventManager.Events.OnNoteHit, Explode);
    }
    private void OnDisable()
    {
        EventManager.StopListening(EventManager.Events.OnNoteHit, Explode);
    }
    // Update is called once per frame
    void Update()
    {
        Vector3 dest = Vector3.Normalize(PlayerPosition.position - transform.position);
        transform.Translate(dest * _speed * Time.deltaTime);
    }
    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("collision");
        if (collision.gameObject.CompareTag("Player"))
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
