using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCheck : MonoBehaviour
{
    public Transform groundCheck; // Điểm kiểm tra dưới chân nhân vật
    public float distanceCheck = 0.2f; // Khoảng cách Raycast
    public LayerMask groundLayer; // Layer của mặt đất

    private Animator animator; // Tham chiếu đến Animator
    private bool isGrounded; // Biến kiểm tra mặt đất

    void Start()
    {
        // Tìm Animator trên Player
        animator = GetComponentInParent<Animator>();

        if (animator == null)
        {
            Debug.LogError("⚠ Không tìm thấy Animator trên Player!");
        }
    }

    void Update()
    {
        // Cập nhật trạng thái mặt đất
        isGrounded = IsGrounded();

        // Cập nhật giá trị vào Animator
        if (animator != null)
        {
            animator.SetBool("IsGrounded", isGrounded);
        }

        // Debug giá trị ra Console
        Debug.Log($"GroundCheck: isGrounded = {isGrounded}");
        if (animator != null)
        {
            Debug.Log($"Animator: IsGrounded = {animator.GetBool("IsGrounded")}");
        }
    }

    public bool IsGrounded()
    {
        if (groundCheck == null)
        {
            Debug.LogError("⚠ groundCheck chưa được gán trong Inspector!");
            return false;
        }

        // Kiểm tra mặt đất bằng Raycast
        bool groundHit = Physics2D.Raycast(groundCheck.position, Vector2.down, distanceCheck, groundLayer);

        // Vẽ Debug Ray (màu xanh nếu chạm đất, đỏ nếu không)
        Debug.DrawRay(groundCheck.position, Vector2.down * distanceCheck, groundHit ? Color.green : Color.red);

        return groundHit;
    }

    // Hàm lấy trạng thái từ Animator (để script khác có thể truy cập)
    public bool GetIsGrounded()
    {
        if (animator != null)
        {
            return animator.GetBool("IsGrounded");
        }
        return false;
    }
}
