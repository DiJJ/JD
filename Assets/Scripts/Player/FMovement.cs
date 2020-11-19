using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class FMovement : MonoBehaviour
{
    Rigidbody2D body;
    Vector2 input;
    [Tooltip("Maximal speed parameter")]
    public float maxSpeed = 6f;
    [Tooltip("How long does it take to reach max speed in seconds")]
    public float accelerationTime = 1.5f;
    [Tooltip("How long does it take to stop in seconds")]
    public float decelerationTime = 1.5f;

    void Awake()
    {
        body = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        Move();
    }

    void Move()
    {
        body.velocity = Vector2.ClampMagnitude(body.velocity, maxSpeed);
        if (!input.Equals(Vector2.zero))
        {
            float force = maxSpeed * body.mass / accelerationTime;
            body.AddForce(input * force, ForceMode2D.Force);
        }
        else if (body.velocity.magnitude >= 1f)
        {
            float force = maxSpeed * body.mass / decelerationTime;
            body.AddForce(-body.velocity.normalized * force, ForceMode2D.Force);
        }
        else
            body.velocity = Vector2.zero;
    }

    public void GetInput(InputAction.CallbackContext context)
    {
        if (!context.performed)
            return;
        input = context.ReadValue<Vector2>();
    }
}
