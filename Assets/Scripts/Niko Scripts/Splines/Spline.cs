using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Spline : MonoBehaviour
{
    [SerializeField]
    private Vector3[] _waypoints;
    [SerializeField]
    private Transform _pointA;
    [SerializeField]
    private Transform _pointB;
    [SerializeField]
    private Transform _pointC;
    [SerializeField]
    private Transform _pointD;
    [SerializeField]
    private Transform _pointAB;
    [SerializeField]
    private Transform _pointBC;
    [SerializeField]
    private Transform _pointCD;
    [SerializeField]
    private Transform _pointABC;
    [SerializeField]
    private Transform _pointBCD;
    [SerializeField]
    private Transform _pointABCD;

    private float _interpolate;
    // Start is called before the first frame update
    void Start()
    {

        //transform.DOPath(_waypoints, 5f, PathType., PathMode.Full3D, 10, Color.red);
    }

    // Update is called once per frame
    void Update()
    {
        _interpolate = (_interpolate + Time.deltaTime) %1f;
        _pointAB.position = Vector3.Lerp(_pointA.position, _pointB.position, _interpolate);
        _pointBC.position = Vector3.Lerp(_pointB.position, _pointC.position, _interpolate);
        _pointCD.position = Vector3.Lerp(_pointC.position, _pointD.position, _interpolate);

        _pointABC.position = Vector3.Lerp(_pointAB.position, _pointBC.position, _interpolate);
        _pointBCD.position = Vector3.Lerp(_pointBC.position, _pointCD.position, _interpolate);

        _pointABCD.position = Vector3.Lerp(_pointABC.position, _pointBCD.position, _interpolate);
    }
}
