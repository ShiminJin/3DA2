using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro; // 添加 TMPro 命名空间

public class carmove : MonoBehaviour
{
    private Rigidbody rb;
    private float moveX;
    private float moveY;
    private float moveSpeed = 20;
    private int count;
    public TextMeshProUGUI countText; // 确保在 Unity 中关联了 UI 文本组件
    public AudioSource clickAudio;


    void Start()
    {
        rb = GetComponent<Rigidbody>();
        count = 0; // 初始化分数
        SetCountText(); // 初始显示分数
    }

    public void OnMove(InputValue movevalue)
    {
        Vector2 moveVector = movevalue.Get<Vector2>();
        moveX = moveVector.x;
        moveY = moveVector.y;
    }

    private void FixedUpdate()
    {
        Vector3 movement = new Vector3(moveX, 0.0f, moveY);
        rb.velocity = movement.normalized * moveSpeed;
    }

    public void OnTriggerEnter(Collider other)
    {
        // 检查标签是否为 "pk"
        if (other.CompareTag("pk"))
        {
            count += 1; // 增加分数
            SetCountText(); // 更新UI
            Destroy(other.gameObject); // 销毁物体
            //other.gameObject.SetActive(false);
            clickAudio.Play();

        }
    }

    void SetCountText()
    {
        if (countText != null) // 防止空引用异常
            countText.text = "Score: " + count.ToString();
    }
}