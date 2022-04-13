using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public GameObject player;

    private Vector3 playerOffset;

    void Start()
    {
        playerOffset = new Vector3(0, 1.25f, 0);
    }

    void LateUpdate()
    {
        if (player != null)
        {
            transform.position = player.transform.position + playerOffset;
            transform.eulerAngles = new Vector3(25, player.transform.eulerAngles.y, transform.rotation.z);
        }
    }
}
