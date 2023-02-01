using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RythmInputController : MonoBehaviour
{
    // Start is called before the first frame update
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            if (RythmManager.Instance.IsNoteCorrectlyHit())
            {
                EventManager.TriggerEvent(EventManager.Events.OnNoteHit);
            }
        }
    }
}
