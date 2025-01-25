using UnityEngine;
using UnityEngine.UIElements;
using static UnityEditor.Searcher.SearcherWindow.Alignment;
using System.Collections;

public class PlayerScript : MonoBehaviour
{
    public float Speed;
    public float JumpForce;
    private float Horizontal;
    private Rigidbody2D Rigidbody2D;

    private float CoyoteTime = 0.2f;
    private float CoyoteTimeCounter;

    private float JumpBufferTime = 0.2f;
    private float JumpBufferCounter;

    private SpriteRenderer SpriteRenderer;

    // Dash start attributes 
    private bool canDash = true;
    private bool isDashing;
    private float dashPower = 10f; // Increased dash power
    private float dashingTime = 0.1f; // Reduced dashing time
    private float dashingCooldown = 1f;

    [SerializeField] private TrailRenderer tr;

    // Last direction faced
    private float lastHorizontalDirection = 1f;

    // Dash limit attributes
    private int availableDashes = 0; // Initial number of dashes

    void Start()
    {
        Rigidbody2D = GetComponent<Rigidbody2D>();
        SpriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        // Horizontal movement
        Horizontal = Input.GetAxisRaw("Horizontal");

        if (Horizontal != 0)
        {
            lastHorizontalDirection = Horizontal;
        }

        SpriteRenderer.flipX = lastHorizontalDirection < 0.0f;

        // Jump
        Jump();

        // Dashes
        if (Input.GetKeyDown(KeyCode.LeftShift) && canDash && availableDashes > 0)
        {
            StartCoroutine(Dash());
        }

        if (Input.GetKeyDown(KeyCode.L))
        {
            AddDash();
        }
    }

    private void Jump()
    {

        // Gets the key inputs
        bool IsPressing = Input.GetKeyDown(KeyCode.W) ||
                          Input.GetKeyDown(KeyCode.Space) ||
                          Input.GetKeyDown(KeyCode.UpArrow);

        bool IsReleasing = Input.GetKeyUp(KeyCode.W) ||
                           Input.GetKeyUp(KeyCode.Space) ||
                           Input.GetKeyUp(KeyCode.UpArrow);

        // Controls the Coyote time
        if (IsGrounded()) 
        {
            CoyoteTimeCounter = CoyoteTime;
        }
        else
        {
            CoyoteTimeCounter -= Time.deltaTime;
        }

        // Controls the Jump buffer

        if (IsPressing) 
        {
            JumpBufferCounter = JumpBufferTime;
        }
        else 
        {
            JumpBufferCounter -= Time.deltaTime;
        }

        // Controls the jump 

        if (CoyoteTimeCounter > 0 && JumpBufferCounter > 0) 
        {
            Rigidbody2D.linearVelocity = new Vector2(Rigidbody2D.linearVelocityX, JumpForce);
            JumpBufferCounter = 0;
        }

        if (IsReleasing && Rigidbody2D.linearVelocityY > 0f)
        {
            Rigidbody2D.linearVelocity = new Vector2(Rigidbody2D.linearVelocityX, Rigidbody2D.linearVelocityY * 0.5f);
            CoyoteTimeCounter = 0;
        }
    }

    private bool IsGrounded() 
    {
        // Define the Rays
        Vector3 LeftRay = transform.position;
        LeftRay.x -= GetComponent<SpriteRenderer>().bounds.size.x / 2 - 0.03f;
        Vector3 RightRay = transform.position;
        RightRay.x += GetComponent<SpriteRenderer>().bounds.size.x / 2 - 0.03f;

        // Draws them
        Debug.DrawRay(transform.position, UnityEngine.Vector3.down * 0.2f, Color.red);
        Debug.DrawRay(LeftRay, UnityEngine.Vector3.down * 0.2f, Color.red);
        Debug.DrawRay(RightRay, UnityEngine.Vector3.down * 0.2f, Color.red);

        //Defines if it's grounded
           return (Physics2D.Raycast(transform.position, UnityEngine.Vector3.down, 0.2f) ||
                   Physics2D.Raycast(LeftRay, UnityEngine.Vector3.down, 0.2f) ||
                   Physics2D.Raycast(RightRay, UnityEngine.Vector3.down, 0.2f));
    }

    private void FixedUpdate()
    {
        if (isDashing)
        {
            return;
        }

        Rigidbody2D.linearVelocity = new Vector2(Horizontal * Speed, Rigidbody2D.linearVelocity.y);
    }

    // Dash
    private IEnumerator Dash()
    {
        canDash = false;
        isDashing = true;
        availableDashes--; // Decrease the number of available dashes
        float originalGravity = Rigidbody2D.gravityScale;
        Rigidbody2D.gravityScale = 0f;
        Rigidbody2D.linearVelocity = new Vector2(lastHorizontalDirection * dashPower, 0f);
        tr.emitting = true;
        yield return new WaitForSeconds(dashingTime);
        Rigidbody2D.linearVelocity = Vector2.zero; // Stop the player after the dash
        tr.emitting = false;
        Rigidbody2D.gravityScale = originalGravity;
        isDashing = false;
        yield return new WaitForSeconds(dashingCooldown);
        canDash = true;
    }

    // Add a dash
    private void AddDash()
    {
        availableDashes++;
    }
}
