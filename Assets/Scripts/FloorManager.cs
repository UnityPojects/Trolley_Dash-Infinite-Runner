using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorManager : MonoBehaviour
{

    public GameObject[] floors;
    public float floorLength = 30.0f;
    public int numOfFloors = 7;

    int index = 0;
    List<Transform> floorSpawns;
    float jumpLength = 0;
    Vector3 initialPosition;

    public static FloorManager Instance;

    private void Awake()
    {
        Instance = this;
        floorSpawns = new List<Transform>(numOfFloors);
        index = 0;
        jumpLength = floorLength * numOfFloors;
    }

    public void Pool()
    {
        floorSpawns[index].position = Vector3.forward*(jumpLength + floorSpawns[index].position.z);
        index = (++index) % numOfFloors;
    }

    public void ResetPosition()
    {
        for (int i = 0; i < numOfFloors; i++)
        {
            floorSpawns[i].position = Vector3.forward*(initialPosition.z + i*floorLength);
        }
        index = 0;
    }

    // Start is called before the first frame update
    void Start()
    {
        SpawnMany(numOfFloors);
        initialPosition = floorSpawns[0].position;
    }
    
    void Spawn(float offset)
    {
        Vector3 newPosition = transform.position;
        newPosition.y = 0.0f;
        newPosition.x = 0.0f;
        newPosition.z += floorLength* offset - floorLength/2.0f;
        int index = Random.Range(0, floors.Length - 1);
        Transform floor = Instantiate(floors[index], newPosition, floors[index].transform.rotation).transform;

        floorSpawns.Add(floor);
    }

    void SpawnMany(int num)
    {
        for(int i = 0; i < numOfFloors; i++)
        {
            Spawn(i);
        }
    }
}
