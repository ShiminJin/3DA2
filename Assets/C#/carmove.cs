using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class carmove : MonoBehaviour
{
    private Rigidbody rb;
    private float moveX;
    private float moveY;
    private float moveSpeed = 5f;
    private float turnSpeed = 40f; // Turning speed
    private int count;
    public TextMeshProUGUI countText; // Ensure the UI text component is assigned in Unity
    public AudioSource clickAudio;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        count = 0; // Initialize score
        SetCountText(); // Display initial score
    }

    public void OnMove(InputValue movevalue)
    {
        Vector2 moveVector = movevalue.Get<Vector2>();
        moveX = moveVector.x; // Horizontal input (turning)
        moveY = moveVector.y; // Vertical input (forward/backward)
    }

    private void FixedUpdate()
    {
        // Movement logic
        Vector3 movement = transform.forward * moveY; // Use the car's forward vector
        rb.velocity = movement.normalized * moveSpeed;

        // Turning logic
        float turn = moveX * turnSpeed * Time.fixedDeltaTime; // Calculate turning angle
        Quaternion turnRotation = Quaternion.Euler(0, turn, 0); // Create rotation quaternion
        rb.MoveRotation(rb.rotation * turnRotation); // Apply turning
    }

    public void OnTriggerEnter(Collider other)
    {
        // Check if the tag is "pk"
        if (other.CompareTag("pk"))
        {
            count += 1; // Increase score
            SetCountText(); // Update UI
            Destroy(other.gameObject); // Destroy the object
            clickAudio.Play();
        }
    }

    void SetCountText()
    {
        if (countText != null) // Prevent null reference exception
            countText.text = "Score: " + count.ToString();
    }
}