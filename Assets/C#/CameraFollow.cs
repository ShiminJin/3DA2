using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    // Reference to the player GameObject that the camera will follow
    public GameObject player;

    // The offset between the camera's position and the player's position
    private Vector3 offset;

    // Start is called before the first frame update
    void Start()
    {
        // Calculate the initial offset between the camera and the player
        offset = transform.position - player.transform.position;
    }

    // LateUpdate is called once per frame, after Update
    // Using LateUpdate ensures the camera follows the player after all movement logic is processed
    void LateUpdate()
    {
        // Set the camera's position to maintain the same offset from the player
        transform.position = player.transform.position + offset;
    }
}