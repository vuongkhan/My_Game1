using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerBase : MonoBehaviour
{
    [Header("Movement Settings")]
    public float moveSpeed = 5f;
    public float jumpForce = 5f;

    protected Animator animator;
    protected Rigidbody2D rb;

    protected virtual void Start()
    {
        // Lấy thành phần Animator và Rigidbody2D từ GameObject
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    protected virtual void Update()
    {
        MoveForward();
        UpdateAnimator();
    }

    // Phương thức di chuyển cơ bản
    protected virtual void MoveForward()
    {
        transform.Translate(Vector2.right * moveSpeed * Time.deltaTime);
    }

    // Cập nhật Animator
    protected virtual void UpdateAnimator()
    {
        if (animator != null)
        {
            animator.SetFloat("Speed", moveSpeed);
        }
    }

    // Phương thức nhảy
    public virtual void Jump()
    {
        if (rb != null)
        {
            animator.SetTrigger("jump");
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }

    }

    // Xử lý va chạm
    protected virtual void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Collision detected with: " + collision.gameObject.name);
    }
    protected abstract void Attack();
}
