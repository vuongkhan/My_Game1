using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyBase : MonoBehaviour
{
    [Header("Enemy Settings")]
    public float moveSpeed = 2f; // Tốc độ di chuyển (nếu cần)
    public int health = 100;    // Máu của kẻ địch
    public PlayerBase playerPrefab; // Prefab PlayerBase (để spawn)

    [Header("Death Effects")]
    public GameObject deathEffectPrefab; // Hiệu ứng hạt khi chết
    public AudioClip deathSound;         // Âm thanh khi chết

    protected Rigidbody2D rb;
    protected Animator animator;

    // Tham chiếu tới ControllerManager
    public ControllerManager controllerManager;

    protected virtual void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        controllerManager = FindObjectOfType<ControllerManager>();
        if (controllerManager == null)
        {
            Debug.LogError("ControllerManager not found in the scene!");
        }
    }

    // Gây sát thương cho kẻ địch
    public virtual void TakeDamage(int damage)
    {
        health -= damage;

        if (animator != null)
        {
            animator.SetTrigger("Hurt");
        }

        if (health <= 0)
        {
            Die();
        }
    }

    // Phương thức Die được sửa đổi
    protected virtual void Die()
    {
        // Spawn hiệu ứng hạt nếu được cấu hình
        if (deathEffectPrefab != null)
        {
            GameObject deathEffect = Instantiate(deathEffectPrefab, transform.position, Quaternion.identity);
            Destroy(deathEffect, 2f); // Hủy hiệu ứng sau 2 giây
        }

        // Phát âm thanh khi chết
        if (deathSound != null)
        {
            AudioSource.PlayClipAtPoint(deathSound, transform.position);
        }

        // Spawn một PlayerBase mới
        if (playerPrefab != null)
        {
            PlayerBase newPlayer = Instantiate(playerPrefab, transform.position, Quaternion.identity);

            if (controllerManager != null)
            {
                controllerManager.AddPlayer(newPlayer);
                Debug.Log("New Player spawned and added to ControllerManager after enemy death.");
            }
            else
            {
                Debug.LogWarning("ControllerManager not found to add Player.");
            }
        }
        else
        {
            Debug.LogError("PlayerPrefab is not assigned in the EnemyBase.");
        }

        // Hủy kẻ địch sau hiệu ứng
        Destroy(gameObject); // Hủy sau 0.5 giây để hiệu ứng có thể diễn ra
    }

    // Xử lý va chạm
    protected virtual void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Enemy collided with: " + collision.gameObject.name);

        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerBase player = collision.gameObject.GetComponent<PlayerBase>();
            if (player != null)
            {
                Debug.Log("Enemy collided with player!");
                // Logic xử lý nếu va chạm với Player
            }
        }
    }
}
