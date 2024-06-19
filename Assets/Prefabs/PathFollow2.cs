using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathFollow2 : MonoBehaviour
{
    public GameObject obj;
    public Vector3[] pathPoints;
    public int numberOfPoints;
    public float speed;

    private Vector3 actualPosition;
    private int x;

    void Start()
    {
        x = 1;
        pathPoints = new Vector3[]
        {
            new Vector3(-8.18f, 1.58f, 0),
            new Vector3(-8.14f, -2f, 0),
            new Vector3(-4.5f, -2.08f, 0)
        };
    }

    void Update()
    {
        actualPosition = obj.transform.position;
        obj.transform.position = Vector3.MoveTowards(actualPosition, pathPoints[x], speed * Time.deltaTime);

        if (actualPosition == pathPoints[x] && x != pathPoints.Length - 1)
        {
            x++;
        }
    }

    public void SetInitialPosition(int index)
    {
        Vector3 offset = new Vector3(0, -1 * index, 0); // Adjust this value to set the gap between customers
        obj.transform.position = pathPoints[0] + offset;
    }
}