using System;
using UnityEngine;
using UnityEngine.Rendering;


public class WoodenPlatform : MonoBehaviour
{
    [SerializeField] private float waitTime;
    [SerializeField] private float respawnTime;
    private Vector3 initialPosition;
    Animator animator;
  

    void Start()
    {
        animator = GetComponent<Animator>();
        initialPosition = transform.position;
    }

    private void Update()
    {
       
    }

   
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {

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

        animator.SetBool("IsBroken", true);
        gameObject.GetComponent<BoxCollider2D>().enabled = false;
        Invoke("RespawnPlatform", respawnTime);
    }

    private void RespawnPlatform()
    {
        animator.SetBool("IsBroken", false);
    }

    public void EnableCollider()
    {
        gameObject.GetComponent<BoxCollider2D>().enabled = true;
    }

}
