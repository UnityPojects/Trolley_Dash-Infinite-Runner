using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public float maxFowardPosition = 2000.0f;
    public Transform MovementController;

    float prevForwardPosition;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        prevForwardPosition = MovementController.position.z;
    }

    void EndOfSegmentTrigger()
    {
        if(MovementController.position.z - prevForwardPosition >= FloorManager.Instance.floorLength)
        {
            FloorManager.Instance.Pool();
            prevForwardPosition += FloorManager.Instance.floorLength;
        }
    }

    void ResetPositions()
    {
        if (MovementController.position.z > maxFowardPosition)
        {
            FloorManager.Instance.ResetPosition();
            MovementController.position = Vector3.zero;
            prevForwardPosition = MovementController.position.z;
        }
    }

    // Update is called once per frame
    void Update()
    {
        EndOfSegmentTrigger();
        ResetPositions();
    }
}
