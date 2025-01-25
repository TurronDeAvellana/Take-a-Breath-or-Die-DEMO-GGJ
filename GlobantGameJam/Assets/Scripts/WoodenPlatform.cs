using System;
using UnityEngine;
using UnityEngine.Rendering;


public class WoodenPlatform : MonoBehaviour
{
    [SerializeField] private float waitTime;
    [SerializeField] private float respawnTime;
    public Collider2D playerCollider;
    public Collider2D platformCollider;
    private Vector3 initialPosition;
    private bool isPlayerBelow = false;

    void Start()
    {
        initialPosition = transform.position;
    }

    private void Update()
    {
        if (isPlayerBelow && Input.GetButtonDown("Jump"))
        {
            Physics2D.IgnoreCollision(playerCollider, platformCollider, true);
        }
        else if (!isPlayerBelow && playerCollider.bounds.min.y > platformCollider.bounds.max.y)
        {
            Physics2D.IgnoreCollision(playerCollider, platformCollider, false);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider == platformCollider)
        {

            Physics2D.IgnoreCollision(playerCollider, platformCollider, false);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isPlayerBelow = playerCollider.bounds.max.y < platformCollider.bounds.min.y;

            foreach (ContactPoint2D contact in collision.contacts)
            {
                if (contact.normal.y < -0.5f)
                {
                    Invoke("DestroyPlatform", waitTime);
                    break;
                }
            }
        }
    }

    private void DestroyPlatform()
    {

        gameObject.SetActive(false);
        Invoke("RespawnPlatform", respawnTime);
    }

    private void RespawnPlatform()
    {
        gameObject.SetActive(true);
    }
}
