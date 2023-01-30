using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    [SerializeField]
    private Transform _cameraPosition;
    private Vector3 _orignalCameraPos;
    [SerializeField]
    private float _shakeDuration = 2f;
    [SerializeField]
    private float _shakeAmount = 0.7f;

    private bool canShake = false;
    private float _shakeTimer;
    // Start is called before the first frame update
    void Start()
    {
        _orignalCameraPos = _cameraPosition.localPosition;
    }

    private void OnEnable()
    {
        EventManager.StartListening(EventManager.Events.OnNoteHit, ShakeCamera);
    }
    private void OnDisable()
    {
        EventManager.StopListening(EventManager.Events.OnNoteHit, ShakeCamera);
    }

    void Update()
    {
     

        if (canShake)
        {
            StartCameraShakeEffect();
        }
    }

    public void ShakeCamera()
    {
        //Debug.Log("Shake on");
        canShake = true;
        _shakeTimer = _shakeDuration;
    }

    public void StartCameraShakeEffect()
    {
        //Debug.Log("Shake");
        if (_shakeTimer > 0)
        {
            _cameraPosition.localPosition = _orignalCameraPos + Random.insideUnitSphere * _shakeAmount;
            _shakeTimer -= Time.deltaTime;
        }
        else
        {
            _shakeTimer = 0f;
            _cameraPosition.localPosition = _orignalCameraPos;
            canShake = false;
        }
    }

}
