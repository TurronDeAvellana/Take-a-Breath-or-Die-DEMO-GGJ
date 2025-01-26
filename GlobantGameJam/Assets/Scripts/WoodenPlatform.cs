using System;
using UnityEngine;
using UnityEngine.Rendering;


public class WoodenPlatform : MonoBehaviour
{
    [SerializeField] private float waitTime;
    [SerializeField] private float respawnTime;
    private Vector3 initialPosition;
  

    void Start()
    {
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

        gameObject.SetActive(false);
        Invoke("RespawnPlatform", respawnTime);
    }

    private void RespawnPlatform()
    {
        gameObject.SetActive(true);
    }
}
