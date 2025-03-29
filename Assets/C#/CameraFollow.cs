using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public GameObject player;
    private Vector3 offset;

    void Start()
    {
        // Calculate initial offset (relative to the local coordinates of the car)

        offset = player.transform.InverseTransformPoint(transform.position);
    }

    void LateUpdate()
    {
        // Update camera position
        transform.position = player.transform.TransformPoint(offset);
        
        // Synchronize Y-axis rotation (car steering), fix X-axis at 44 degrees

        float fixedX = 44.042f;
        transform.eulerAngles = new Vector3(
            fixedX,
            player.transform.eulerAngles.y,
            0
        );
    }
}