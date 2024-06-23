using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Path : MonoBehaviour
{
    public List<Transform> points;

    private void Start()
    {
        if (points.Count == 0)
        {
            Debug.LogWarning("Path has no points.");
        }
    }

    public Vector3 GetPositionFromIndex(int index)
    {
        if (0 <= index && index <= points.Count)
        {
            return points[index].position;
        }
        else
        {
            Debug.LogWarning(string.Format($"Item at index: {index} in path does not exist"));
            return Vector3.zero;
        }
    }
}
