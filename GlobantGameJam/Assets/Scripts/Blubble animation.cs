using UnityEngine;

public class Blubbleanimation : MonoBehaviour
{
    public float shrinkDuration = 4.0f; 
    public float explosionRadius = 5.0f; 
    public LayerMask PlayerLayer;
    public string playerLayerName = "Player";
    public GameObject otherObject;  
    private Vector3 initialScale;
    private float timer;
    private bool isShrinking = false;
   


    void Start()
    {
        initialScale = transform.localScale; 
        timer = 0.0f;

        Invoke("StartShrinking", 0.5f);
    }

    void StartShrinking()
    {
        isShrinking = true;
    }

    void Update()
    {
        if (!isShrinking) return;

        timer += Time.deltaTime;
        float scale = Mathf.SmoothStep(1.0f, 0.0f, timer / shrinkDuration);
        transform.localScale = initialScale * scale;

        if (timer >= shrinkDuration)
        {
            Explode();
        }

        if (GetComponent<SpriteRenderer>().bounds.size.x <= otherObject.GetComponent<SpriteRenderer>().bounds.size.x)
        {
            Explode();
        }

        
    }

    void Explode()
    {
        otherObject.GetComponent<PlayerScript>().ToggleOxygen(true);
        Debug.Log("¡La burbuja explotó!");
        Destroy(gameObject); 
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player") 
        {
            GameObject player = collision.gameObject;
            player.GetComponent<PlayerScript>().ToggleOxygen(false);
        }
        
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            otherObject.GetComponent<PlayerScript>().ToggleOxygen(true);
            Explode();
        }
    }

}
