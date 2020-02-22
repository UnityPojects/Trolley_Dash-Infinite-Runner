using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{

    public static PlayerManager Instance;

    public float laneOffset = 100.0f;

    private void Awake()
    {
        Instance = this;
    }


    public void moveSideways(int direction)
    {
        float newXposition = transform.position.x + laneOffset * direction;

        if (Mathf.Abs(newXposition) <= laneOffset) {
            transform.position = new Vector3(newXposition, 0, transform.position.z);
        }
    }

}
