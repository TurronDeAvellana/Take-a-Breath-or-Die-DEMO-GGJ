using UnityEngine;

public class OxygenBarScript : MonoBehaviour
{
    public GameObject player;
    void Start()
    {
        
    }

    void Update()
    {
        if (player != null)
        {
            Vector3 scale = transform.localScale;
            scale.x = 0.5f * (player.GetComponent<PlayerScript>().Oxygen / 100);
            transform.localScale = scale;
        }
    }
}
