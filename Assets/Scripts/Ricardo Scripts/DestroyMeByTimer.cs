using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyMeByTimer : MonoBehaviour
{
    [SerializeField]
    float Delay = 1.5f;

    private void OnEnable()
    {
        Invoke("DestroySelf", Delay);
    }

    void DestroySelf()
    {
        Destroy(gameObject);
    }
}
