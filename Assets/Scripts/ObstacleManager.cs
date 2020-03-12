using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleManager : MonoBehaviour
{
    public GameObject[] obstacles;

    public static ObstacleManager Instance;

    List<MultiObstacle> obstacleSpawns;
    Vector3 lastSpawnPos;
    Vector3 lastPlayerPos;
    int index = 0;
    const int minSpawns = 4;
    float spawnOffset = 0.0f;

    private void Awake()
    {
        Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        obstacleSpawns = new List<MultiObstacle>(minSpawns);
        InitiateObstacles();
        lastPlayerPos = PlayerManager.Instance.transform.position;
    }

    void InitiateObstacles()
    {
        for (int index = 0; index < minSpawns; index++)
        {
            Spawn(FloorManager.Instance.floorLength/3.0f * (index-2));
        }
        spawnOffset = FloorManager.Instance.floorLength / 3.0f * (minSpawns-1);
    }


    void Spawn(float offset)
    {
        Vector3 newPosition = transform.position;
        newPosition.y = 0.5f;
        newPosition.x = 0.0f;
        newPosition.z = offset + PlayerManager.Instance.transform.position.z;
        int index = Random.Range(0, obstacles.Length);
        GameObject obstacle = Instantiate(obstacles[index], newPosition, obstacles[index].transform.rotation);
        MultiObstacle mObs = obstacle.GetComponent<MultiObstacle>();
        mObs.RandomiseChildPositions();
        obstacleSpawns.Add(mObs);
        lastSpawnPos = newPosition;
    }

    void PoolSpawn(float offset)
    {
        Vector3 newPosition = transform.position;
        newPosition.y = 0.5f;
        newPosition.z = PlayerManager.Instance.transform.position.z + offset;
        newPosition.x = 0.0f;
        obstacleSpawns[index].SetPosition(newPosition);
        obstacleSpawns[index].RandomiseChildPositions();
        index = (index + 1) % obstacleSpawns.Count;

        lastSpawnPos = newPosition;
    }

    public void PlaceObstacle()
    {
        Vector3 playerPos = PlayerManager.Instance.transform.position;
        if(playerPos.z < lastPlayerPos.z)
        {
            lastPlayerPos = playerPos;
        }
        if(playerPos.z - lastPlayerPos.z > FloorManager.Instance.floorLength / 3.0f)
        {
            lastPlayerPos = playerPos;
            PoolSpawn(spawnOffset);
        }
    }

    public void ResetPosition()
    {
        foreach(MultiObstacle obstacle in obstacleSpawns)
        {
            obstacle.OffsetPosition( -Vector3.forward * Constants.maxDistance);
        }
        lastSpawnPos -= Vector3.forward * Constants.maxDistance; 
    }

    // Update is called once per frame
    void Update()
    {
        PlaceObstacle();
    }
}
