using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultiObstacle : MonoBehaviour
{
    List<Transform> childrenObstacles;
    // Start is called before the first frame update
    void Start()
    {
        childrenObstacles = new List<Transform>(2);
        
        foreach(Transform child in gameObject.GetComponentInChildren<Transform>())
        {
            childrenObstacles.Add(child);
        }
    }

    public void SetPosition(Vector3 position)
    {
        transform.position = position;
    }

    public void OffsetPosition(Vector3 offset)
    {
        transform.position += offset; 
    }

}
