using UnityEngine;
using static UnityEditor.Searcher.SearcherWindow.Alignment;
using System.Collections;
using UnityEditor;

public class PlayerScript : MonoBehaviour
{
    public float Speed;
    public float JumpForce;
    public GameObject Prefab;
    private float Horizontal;
    private Rigidbody2D Rigidbody2D;
    private SpriteRenderer SpriteRenderer;

    public float Oxygen;
    public bool IsReducing;

    // Game feel attributes
    private float CoyoteTime = 0.2f;
    private float CoyoteTimeCounter;

    private float JumpBufferTime = 0.2f;
    private float JumpBufferCounter;

    // Dash start attributes 
    private bool canDash = true;
    private bool isDashing;
    private float dashPower = 10f; // Increased dash power
    private float dashingTime = 0.1f; // Reduced dashing time
    private float dashingCooldown = 2f; // Increased cooldown to 2 seconds

    [SerializeField] private TrailRenderer tr;

    // Last direction faced
    private float lastHorizontalDirection = 1f;

    // Dash limit attributes
    private int availableDashes = 1; // Initial number of dashes

    void Start()
    {
        Rigidbody2D = GetComponent<Rigidbody2D>();
        SpriteRenderer = GetComponent<SpriteRenderer>();
        Oxygen = 100f;
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

        // Dash
        if (Input.GetKeyDown(KeyCode.LeftShift) && canDash && availableDashes > 0)
        {
            StartCoroutine(Dash());
        }

        if (Input.GetKeyDown(KeyCode.L))
        {
            AddDash();
        }

        // Oxygen
        if (IsReducing)
        {
            Oxygen -= Time.deltaTime * 10;
            if (Oxygen < 0)
            {
                Oxygen = 0;
                Die();
            }
        }


    }

    private void Jump()
    {
        // Gets the key inputs
        bool IsPressing = Input.GetKeyDown(KeyCode.W) ||
                          Input.GetKeyDown(KeyCode.Space) ||
                          Input.GetKeyDown(KeyCode.UpArrow);
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
            Rigidbody2D.linearVelocity = new Vector2(Rigidbody2D.linearVelocity.x, JumpForce);
            JumpBufferCounter = 0;
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
        Debug.DrawRay(transform.position, Vector3.down * 0.2f, Color.red);
        Debug.DrawRay(LeftRay, Vector3.down * 0.2f, Color.red);
        Debug.DrawRay(RightRay, Vector3.down * 0.2f, Color.red);
        //Defines if it's grounded
        return (Physics2D.Raycast(transform.position, Vector3.down, 0.2f) ||
                Physics2D.Raycast(LeftRay, Vector3.down, 0.2f) ||
                Physics2D.Raycast(RightRay, Vector3.down, 0.2f));
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
        availableDashes = 1; // Reset the number of available dashes
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Damage"))
        {
            Die();
        }
    }

    private void Die()
    {
        Destroy(gameObject);
    }

    public void AddOxygen(float Quantity)
    {
        Oxygen += Quantity;
        if (Oxygen > 100)
        {
            Oxygen = 100;
        }
    }

    public void ToggleOxygen(bool boolean)
    {
        IsReducing = boolean;
    }

    // Add a dash
    private void AddDash()
    {
        availableDashes = 1; // Ensure only one dash is available at a time
    }
}
