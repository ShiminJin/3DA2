using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement; // 新增命名空间
using TMPro;

public class carmove : MonoBehaviour
{
    private Rigidbody rb;
    private float moveX;
    private float moveY;
    private float moveSpeed = 5f;
    private float turnSpeed = 40f;
    private int count;
    public TextMeshProUGUI countText;
    public AudioSource clickAudio;

    // 新增：目标场景名称（在Unity编辑器中设置）
    public string targetSceneName = "NextScene"; // 替换为你的场景名称

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        count = 0;
        SetCountText();
    }

    public void OnMove(InputValue movevalue)
    {
        Vector2 moveVector = movevalue.Get<Vector2>();
        moveX = moveVector.x;
        moveY = moveVector.y;
    }

    private void FixedUpdate()
    {
        Vector3 movement = transform.forward * moveY;
        rb.velocity = movement.normalized * moveSpeed;

        float turn = moveX * turnSpeed * Time.fixedDeltaTime;
        Quaternion turnRotation = Quaternion.Euler(0, turn, 0);
        rb.MoveRotation(rb.rotation * turnRotation);
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("pk"))
        {
            count += 1;
            SetCountText();
            Destroy(other.gameObject);
            clickAudio.Play();

            // 新增：检查分数是否达到10
            if (count == 10)
            {
                SceneManager.LoadScene(targetSceneName); // 加载目标场景
            }
        }
    }

    void SetCountText()
    {
        if (countText != null)
            countText.text = "Score: " + count.ToString();
    }
}