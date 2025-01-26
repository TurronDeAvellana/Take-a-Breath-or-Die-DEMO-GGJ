using Unity.VisualScripting;
using UnityEngine;

public class PointsScript : MonoBehaviour
{
    public GameObject Master;
    void Start()
    {
        
    }

    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Master.GetComponent<GameMaster>().AddPoint();
        Debug.Log(collision);
        Destroy(gameObject);
    }

    public void SetMaster (GameObject NewMaster)
    {
        Master = NewMaster;
    }
}
