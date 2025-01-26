using UnityEngine;
using System.Linq;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine.UIElements;


public class GameMaster : MonoBehaviour
{
    public GameObject PrefabPoints;
    private int[] Points = { 2 };
    private int ActualPoints;
    private int Level;
    private Vector3[] LevelOne =  
            { 
                new Vector3(-0.67f,-0.21f,0),
                new Vector3(0.04f,0.13f,0),
                new Vector3(-0.68f,0.5f,0),
                new Vector3(0.36f,0.64f,0),
                new Vector3(0.99f,0.35f,0)
            };

    void Start()
    {
        Level = 1;
        ActualPoints = 0;
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
        ActualPoints--;
    }
}
