using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public float maxFowardPosition;
    public Transform MovementController;

    float prevForwardPosition;

    private void Awake()
    {
        Instance = this;
        maxFowardPosition = Constants.maxDistance;
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
