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
            Vector3 position = player.transform.position;
            position.y += 0.25f;
            transform.position = position;
            Vector3 scale = transform.localScale;
            Debug.Log(player.GetComponent<PlayerScript>().Oxygen);
            scale.x = 0.5f * (player.GetComponent<PlayerScript>().Oxygen / 100);
            transform.localScale = scale;
        }
    }
}
