using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PLMovement : MonoBehaviour
{
    Rigidbody2D body;
    Vector2 input;

    float speed = 5f;
    float airSpeed = 7f;
    bool isGrounded;

    public bool movementEnabled = true;

    void Awake()
    {
        body = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        switch (input.x)
        {
            case 1f:
                transform.rotation = new Quaternion(0f, 0f, 0f, 1f); break;
            case -1f:
                transform.rotation = new Quaternion(0f, 180f, 0f, 1f); break;
        }
    }

    void FixedUpdate()
    {
        Move();
    }

    void Move()
    {
        if (!movementEnabled)
            return;

        isGrounded = GetComponent<PLJump>().CheckGround();
        float speedScale = speed * Convert.ToInt32(isGrounded)
                         + airSpeed * Convert.ToInt32(!isGrounded);
        body.velocity = new Vector2(input.x * speedScale, body.velocity.y);
    }

    public void GetAxis(InputAction.CallbackContext context)
    {
        input = context.ReadValue<Vector2>();
    }
}
