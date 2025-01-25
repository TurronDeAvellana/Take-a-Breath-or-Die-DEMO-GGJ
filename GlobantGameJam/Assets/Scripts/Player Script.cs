using UnityEngine;
using static UnityEditor.Searcher.SearcherWindow.Alignment;
using System.Collections;

public class PlayerScript : MonoBehaviour
{
    public float Speed;
    public float JumpForce;
    private float Horizontal;
    private Rigidbody2D Rigidbody2D;
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
        Debug.DrawRay(transform.position, UnityEngine.Vector3.down * 0.2f, Color.red);
        bool Grounded = Physics2D.Raycast(transform.position, UnityEngine.Vector3.down, 0.2f);

        if ((Input.GetKeyDown(KeyCode.W) || (Input.GetKeyDown(KeyCode.Space))) && Grounded)
        {
            Jump();
        }

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
        Rigidbody2D.AddForce(transform.up * JumpForce);
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
