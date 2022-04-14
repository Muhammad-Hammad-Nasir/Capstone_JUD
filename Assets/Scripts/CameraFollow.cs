using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public GameObject player;
    public float rotationSpeed;

    void Update()
    {
        if (player != null)
        {
            transform.position = player.transform.position;
            float horizontalInput = Input.GetAxis("Horizontal");

            transform.Rotate(Vector3.up, horizontalInput * rotationSpeed * Time.deltaTime);
        }
    }
}
