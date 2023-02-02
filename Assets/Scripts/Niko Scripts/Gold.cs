using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Gold : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        transform.DORotate((transform.rotation.eulerAngles + Vector3.right), 0.5f);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.X))
        {
            transform.DOLocalRotate(new Vector3(0, 0, 360), 0.5f, RotateMode.FastBeyond360).SetRelative(true).SetEase(Ease.Linear).OnKill(Stop);
            //transform.DOShakePosition(0.5f);
        }
    }
    public void Stop ()
    {
        Destroy(gameObject);
    }

}
