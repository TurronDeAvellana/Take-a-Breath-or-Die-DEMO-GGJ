using UnityEngine;

public class PointsScript : MonoBehaviour
{
    public GameObject Master;
    public AudioClip audio;
    void Start()
    {
        
    }

    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Master.GetComponent<GameMaster>().AddPoint();
            GameObject player = collision.gameObject;
            player.GetComponent<PlayerScript>().AddOxygen(100);
            Camera.main.GetComponent<AudioSource>().PlayOneShot(audio);
            Destroy(gameObject);
        }
    }

    public void SetMaster (GameObject NewMaster)
    {
        Master = NewMaster;
    }
}
