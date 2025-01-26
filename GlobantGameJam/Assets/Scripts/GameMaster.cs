using UnityEngine;
using System.Linq;


public class GameMaster : MonoBehaviour
{
    public GameObject PrefabPoints;
    private int Points;

    void Start()
    {
        Points = 0;
    }

    void Update()
    {
        
    }

    private Vector3[] SelectPoints(int NumberOfPoints,
                                   Vector3[] ListOfPoints) 
    {
        Vector3[] SelectedPoints = { };

        while (SelectedPoints.Length < NumberOfPoints) 
        {
            int selector = Random.Range(0, SelectedPoints.Length);
            if (!SelectedPoints.Contains(ListOfPoints[selector]))
            {
                SelectedPoints.Append(ListOfPoints[selector]);
            }
        }

        return SelectedPoints;
    }

    private void CreatePoints(Vector3[] positions)
    {
        foreach (var position in positions)
        {
            GameObject instance = Instantiate(PrefabPoints, position, Quaternion.identity);
            instance.GetComponent<PointsScript>().SetMaster(gameObject);
        }
    }
    public void AddPoint() 
    {
        Points--;
    }
}
