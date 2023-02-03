using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Gold : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        transform.DOLocalRotate(new Vector3(360, 0, 360), 0.5f, RotateMode.FastBeyond360).SetRelative(true).SetEase(Ease.Linear).OnKill(Stop);
        transform.DOShakePosition(0.5f, 0.1f);
        GetComponent<AudioSource>().Play();
    }

    //private void OnEnable()
    //{
    //    EventManager.StartListening(EventManager.Events.OnNoteHit, ShakeCoin);
    //}
    //private void OnDisable()
    //{
    //    EventManager.StopListening(EventManager.Events.OnNoteHit, ShakeCoin);
    //}
    // Update is called once per frame
    void Update()
    {

    }
    public void Stop()
    {
        //EventManager.StopListening(EventManager.Events.OnNoteHit, ShakeCoin);
        Destroy(gameObject);
    }

}
