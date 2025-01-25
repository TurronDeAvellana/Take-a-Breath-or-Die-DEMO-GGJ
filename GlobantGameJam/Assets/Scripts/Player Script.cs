using UnityEngine;
using static UnityEditor.Searcher.SearcherWindow.Alignment;

public class PlayerScript : MonoBehaviour
{
    public float Speed;
    public float JumpForce;
    private float Horizontal;
    private Rigidbody2D Rigidbody2D;
    

    void Start()
    {
        Rigidbody2D = GetComponent<Rigidbody2D>();
    }
    void Update()
    {
        // Movimiento horizontal
        Horizontal = Input.GetAxisRaw("Horizontal");
        if (Horizontal < 0.0f)
        {
            transform.localScale = new UnityEngine.Vector3(-1.0f, 1.0f, 1.0f);
        }
        else if (Horizontal > 0.0f)
        {
            transform.localScale = new UnityEngine.Vector3(1.0f, 1.0f, 1.0f);
        }
        // Jump

        Debug.DrawRay(transform.position, UnityEngine.Vector3.down * 0.2f, Color.red);
        bool Grounded;

        if (Physics2D.Raycast(transform.position, UnityEngine.Vector3.down, 0.2f))
        {
            Grounded = true;
        }
        else
        {
            Grounded = false;
        }

        if (Input.GetKeyDown(KeyCode.W) && Grounded)
        {
            Jump();
        }
    }
    private void Jump()
    {
        Rigidbody2D.AddForce(transform.up * JumpForce);
    }
    private void FixedUpdate()
    {
        Rigidbody2D.linearVelocity = new UnityEngine.Vector2(Horizontal * Speed, Rigidbody2D.linearVelocity.y);
    }
}
