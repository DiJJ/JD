using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PLAnims : MonoBehaviour
{
    Animator animator;

    Rigidbody2D body;

    InputActionAsset controls;

    public Transform groundCheck;
    public float radius = 1f;
    public LayerMask ground;
    bool isGrounded;

    void Start()
    {
        animator = GetComponentInChildren<Animator>();
        body = GetComponent<Rigidbody2D>();
        controls = GetComponent<PlayerInput>().actions;
    }

    void Update()
    {
        animator.SetFloat("moveSpeed", Mathf.Abs(body.velocity.x));

        animator.SetBool("is Grounded", isGrounded);
    }

    void FixedUpdate()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, radius, ground);
    }
}
