using UnityEngine;

public class Blubbleanimation : MonoBehaviour
{
    public float shrinkDuration = 4.0f; 
    public float explosionRadius = 5.0f; 
    public LayerMask PlayerLayer;
    public string playerLayerName = "Player";
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

    }

    void Explode()
    {
        Debug.Log("¡La burbuja explotó!");
        Destroy(gameObject); 
    }


    private void OnTriggerExit2D(Collider2D other)
    {
        Debug.Log("Un objeto salió del área: " + other.name);

        if (other.CompareTag("Player"))
        {
            Debug.Log("El jugador salió del área. ¡Explotando!");
            Explode();
        }
    }

}
