using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PLJump : MonoBehaviour
{
    Rigidbody2D body;
    InputAction jump;
    bool input;
    float jumpPower = 6f;

    public Transform groundCheck, wallCheck;
    public float radius = 0.2f;
    public LayerMask ground;
    bool isGrounded, isWalled;

    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        jump = GetComponent<PlayerInput>().actions.FindAction("Jump");
    }

    
    void Update()
    {
        CheckGround();
        CheckWall();
        if (jump.triggered)
            Jump();
    }

    void FixedUpdate()
    {
        if (body.velocity.y < 0)
            body.gravityScale = 2f;
        else if (body.velocity.y >= 0 && input)
            body.gravityScale = 0.5f;
        else if (body.velocity.y >= 0 && !input)
            body.gravityScale = 2f;
    }

    void Jump()
    {
        if (isGrounded)
            body.AddForce(transform.up * jumpPower, ForceMode2D.Impulse);
        else if (isWalled)
        {
            if (transform.rotation.y <= 0.1f)
                transform.rotation = new Quaternion(0f, 180f, 0f, 1f);
            else if (transform.rotation.y >= 0.9f)
                transform.rotation = new Quaternion(0f, 0f, 0f, 1f);

            StartCoroutine(DisableMovement(.2f));
            body.velocity = Vector2.zero;
            body.velocity = (transform.up + transform.right) * jumpPower;
        }
    }

    public bool CheckGround()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, radius, ground);
        return isGrounded;
    }

    public bool CheckWall()
    {
        isWalled = Physics2D.OverlapCircle(wallCheck.position, radius, ground);
        return isWalled;
    }

    IEnumerator DisableMovement(float time)
    {
        GetComponent<PLMovement>().movementEnabled = false;
        yield return new WaitForSeconds(time);
        GetComponent<PLMovement>().movementEnabled = true;
    }

    public void GetButton(InputAction.CallbackContext context)
    {

        if (context.started)
            input = true;
        else if (context.performed)
            input = true;
        else if (context.canceled)
            input = false;
        
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(groundCheck.position, radius);
        Gizmos.DrawWireSphere(wallCheck.position, radius);
    }
}
