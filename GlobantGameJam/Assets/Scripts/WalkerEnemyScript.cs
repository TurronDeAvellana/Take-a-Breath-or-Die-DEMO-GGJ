using UnityEngine;

public class WalkerEnemyScript : MonoBehaviour
{
    public GameObject player;
    private Rigidbody2D RB;
    private float speed = 1.0f;
    
    void Start()
    {
        RB = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        CheckBorders();
    }

    private void FixedUpdate()
    {
        RB.linearVelocityX = speed;
    }

    private bool IsOnLeft() 
    {
        // Define the Rays
        Vector3 LeftRay = transform.position;
        LeftRay.x -= GetComponent<SpriteRenderer>().bounds.size.x / 2;

        // Draws it
        Debug.DrawRay(LeftRay, UnityEngine.Vector3.down * 0.5f, Color.red);

        //Defines if it's on the left border
        return !Physics2D.Raycast(LeftRay, UnityEngine.Vector3.down, 0.5f);
    }
    private bool IsOnRight() 
    {
        // Define the Rays
        Vector3 RightRay = transform.position;
        RightRay.x += GetComponent<SpriteRenderer>().bounds.size.x / 2;
        
        // Draws it
        Debug.DrawRay(RightRay, UnityEngine.Vector3.down * 0.5f, Color.red);

        //Defines if it's on the right border
        return !Physics2D.Raycast(RightRay, UnityEngine.Vector3.down, 0.5f);
    }

    private void CheckBorders() 
    {
        if (IsOnLeft())
        {
            speed = 1;
            Vector2 scale = transform.localScale;
            scale.x = Mathf.Abs(scale.x);
            transform.localScale = scale;
        }

        if (IsOnRight())
        {
            speed = -1;
            Vector2 scale = transform.localScale;
            scale.x = Mathf.Abs(scale.x) * -1;
            transform.localScale = scale;
        }
    }
}
