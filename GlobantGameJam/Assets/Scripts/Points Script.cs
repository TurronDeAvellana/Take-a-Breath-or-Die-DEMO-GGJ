using Unity.VisualScripting;
using UnityEngine;

public class PointsScript : MonoBehaviour
{
    public GameObject Bubble;
    void Start()
    {
        
    }

    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        /*
        Bubble.GetComponent<BubbleScript>().AddPoint();
        */
        Debug.Log(collision);
        Destroy(gameObject);
    }

    public void SetBubble (GameObject NewBubble)
    {
        Bubble = NewBubble;
    }
}
