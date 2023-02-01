using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphreRythmScript : MonoBehaviour
{
    private MeshRenderer _meshRenderer;
    private bool isColor2;
   
    // Start is called before the first frame update
    void Start()
    {
        transform.Rotate(transform.up, 90f);
        _meshRenderer = GetComponent<MeshRenderer>();
    }
    private void OnEnable()
    {
        EventManager.StartListening(EventManager.Events.OnBeatChange, SwitchColor);
    }
    private void OnDisable()
    {
        EventManager.StopListening(EventManager.Events.OnBeatChange, SwitchColor);

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            if (RythmManager.Instance.IsNoteCorrectlyHit())
            {
                EventManager.TriggerEvent(EventManager.Events.OnNoteHit);
            }
        }
    }

    public void SwitchColor()
    {
        Color color ;
        if (isColor2)
        {
            color = Color.white;
        }
        else
        {
            color = Color.black;
        }
        _meshRenderer.material.color = color;
        isColor2 = !isColor2;
    }
}
