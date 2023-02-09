using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnnemySpawnManager : MonoBehaviour
{
    [SerializeField]
    private List<Transform> _spawnList;
    [SerializeField]
    GameObject _ennemyPrefab;
    [SerializeField]
    Transform _playerPos;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log(gameObject.name);
    }
    private void OnEnable()
    {
        EventManager.StartListening(EventManager.Events.EnnemySpawn, Spawn);
        EventManager.StartListening(EventManager.Events.OnBeatChange, Spawn);
        EventManager.StartListening(EventManager.Events.OnPlayerDeath, StopSpawn);
    }
    private void OnDisable()
    {
        EventManager.StopListening(EventManager.Events.EnnemySpawn, Spawn);
        EventManager.StopListening(EventManager.Events.OnBeatChange, Spawn);
        EventManager.StopListening(EventManager.Events.OnPlayerDeath, StopSpawn);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Spawn()
    {
        Debug.Log("Spawn");
        for (int i=0; i<_spawnList.Count; i++)
        {
            
            GameObject ennemy = Instantiate(_ennemyPrefab, _spawnList[i].position, _spawnList[i].rotation);
            ennemy.GetComponent<EnnemyController>().PlayerPosition = _playerPos;
        }
    }
    public void StopSpawn()
    {
        gameObject.SetActive(false);
    }
   
}
