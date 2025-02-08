using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boy_Player : PlayerBase
{
    protected override void Start()
    {
        base.Start();
        // Thêm logic khởi tạo riêng nếu cần
    }

    protected override void Update()
    {
        base.Update();
        // Thêm logic cập nhật riêng nếu cần
    }

    protected override void OnCollisionEnter2D(Collision2D collision)
    {
        base.OnCollisionEnter2D(collision);
        Debug.Log("PlayerController specific collision logic.");
        Attack();
    }

    public override void Jump()
    {
        base.Jump();
        Debug.Log("PlayerController specific jump logic.");
    }
    protected override void Attack()
    {
        Debug.Log("sf");
    }
}
