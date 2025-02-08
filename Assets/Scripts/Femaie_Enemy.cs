using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Basic_Enemy : EnemyBase
{
    // Override phương thức OnCollisionEnter2D để thêm logic tự hủy
    protected override void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Femaie_Enemy collided with: " + collision.gameObject.name);

        // Tự động chết khi va chạm
        Die();
    }
}
