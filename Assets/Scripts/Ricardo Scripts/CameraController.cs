using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] PlayerController playerController;
    [SerializeField] float moveSpeed = 1.0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        FollowPlayer();
    }

    private void FollowPlayer()
    {
        Vector3 cameraDestinationPos = new Vector3(playerController.transform.position.x, playerController.transform.position.y, transform.position.z);

        transform.position = Vector3.Lerp(transform.position, cameraDestinationPos, Time.deltaTime * moveSpeed);
    }
}
