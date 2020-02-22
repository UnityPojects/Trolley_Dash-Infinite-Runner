using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementController : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        transform.position += Vector3.forward * 15.0f * Time.deltaTime;       
    }
}
