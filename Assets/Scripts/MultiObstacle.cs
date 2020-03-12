using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultiObstacle : MonoBehaviour
{
    List<Transform> childrenObstacles;
    readonly int[] lanePositions = new int[] { -1, 0, 1 };
    // Start is called before the first frame update
    void Awake()
    {
        childrenObstacles = new List<Transform>(2);
        
        foreach(Transform child in gameObject.GetComponentInChildren<Transform>())
        {
            childrenObstacles.Add(child);
        }

        //RandomiseChildPositions();
    }

    public void RandomiseChildPositions()
    {
        Vector3 newPosition = new Vector3(0, 0.5f, 0);
        int randIndex = Random.Range(0, lanePositions.Length);

        foreach (Transform child in childrenObstacles)
        {
            newPosition.x = Constants.laneOffset * lanePositions[randIndex];
            child.localPosition = newPosition;

            if (childrenObstacles.Count > 1)
            {
                int prevIndex = randIndex;
                while (randIndex == prevIndex)
                {
                    randIndex = Random.Range(0, lanePositions.Length);
                }
            }
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
