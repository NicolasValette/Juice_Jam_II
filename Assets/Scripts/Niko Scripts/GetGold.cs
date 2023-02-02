using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetGold : MonoBehaviour
{
    [SerializeField]
    private Transform _goldSpawner;
    [SerializeField]
    private GameObject _goldPrefab;
    // Start is called before the first frame update
    private void OnEnable()
    {
        EventManager.StartListening(EventManager.Events.OnNoteHit, WinGold);
    }
    private void OnDisable()
    {
        EventManager.StopListening(EventManager.Events.OnNoteHit, WinGold);
    }
    public void WinGold()
    {
        Instantiate(_goldPrefab, _goldSpawner.position, _goldSpawner.rotation);
    }
}
