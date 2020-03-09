using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleManager : MonoBehaviour
{
    public GameObject[] obstacles;

    public static ObstacleManager Instance;

    List<Transform> obstacleSpawns;
    Vector3 lastSpawnPos;
    float threshold;
    int index = 0;
    int[] xPos = new int[] { -1, 0, 1 };
    const int minSpawns = 5;
    float spawnOffset = 0.0f;

    private void Awake()
    {
        Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        obstacleSpawns = new List<Transform>(minSpawns);
        InitiateObstacles();
        lastSpawnPos = PlayerManager.Instance.transform.position;
        threshold = FloorManager.Instance.floorLength;
    }

    void InitiateObstacles()
    {
        for (int index = 0; index < minSpawns; index++)
        {
            Spawn(FloorManager.Instance.floorLength/3.0f * index, xPos);
            spawnOffset += FloorManager.Instance.floorLength / 3.0f * index;
        }
    }


    void Spawn(float offset, int[] xPositionMultiples)
    {
        Vector3 newPosition = transform.position;
        newPosition.y = 0.5f;
        int randIndex = Random.Range(0, xPositionMultiples.Length);
        newPosition.x = Constants.laneOffset*xPositionMultiples[randIndex];
        newPosition.z = offset + PlayerManager.Instance.transform.position.z;
        int index = Random.Range(0, obstacles.Length);
        Transform obstacle = Instantiate(obstacles[index], newPosition, obstacles[index].transform.rotation).transform;
        obstacleSpawns.Add(obstacle);
    }

    void PoolSpawn(float offset, int[] xPositionMultiples)
    {
        Vector3 newPosition = transform.position;
        newPosition.y = 0.5f;
        int randIndex = Random.Range(0, xPositionMultiples.Length);
        newPosition.x = Constants.laneOffset * xPositionMultiples[randIndex];
        newPosition.z = PlayerManager.Instance.transform.position.z + offset;
        obstacleSpawns[index].position = newPosition;
        index = (index+1) % obstacleSpawns.Count;
    }

    public void PlaceObstacle()
    {
        Vector3 playerPos = PlayerManager.Instance.transform.position;
        if(playerPos.z < lastSpawnPos.z)
        {
            lastSpawnPos = playerPos;
        }
        if(playerPos.z - lastSpawnPos.z > 2*FloorManager.Instance.floorLength / 3.0f)
        { 
            lastSpawnPos = playerPos;
            PoolSpawn(FloorManager.Instance.floorLength / 3.0f + spawnOffset, xPos);
        }
    }

    public void ResetPosition()
    {
        foreach(Transform obstacle in obstacleSpawns)
        {
            obstacle.position -= Vector3.forward * Constants.maxDistance;
        }
    }

    // Update is called once per frame
    void Update()
    {
        PlaceObstacle();
    }
}
