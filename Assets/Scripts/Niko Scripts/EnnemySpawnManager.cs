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
        
    }
    private void OnEnable()
    {
        EventManager.StartListening(EventManager.Events.EnnemySpawn, Spawn);
        EventManager.StartListening(EventManager.Events.OnBeatChange, Spawn);
    }
    private void OnDisable()
    {
        EventManager.StopListening(EventManager.Events.EnnemySpawn, Spawn);
        EventManager.StopListening(EventManager.Events.OnBeatChange, Spawn);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Spawn()
    {
        for (int i=0; i<_spawnList.Count; i++)
        {
            if (i%2 == 0)
            {
                continue;
            }
            GameObject ennemy = Instantiate(_ennemyPrefab, _spawnList[i].position, _spawnList[i].rotation);
            ennemy.GetComponent<EnnemyController>().PlayerPosition = _playerPos;
        }
    }
   
}
