using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyBoardInputs : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {

        }
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            PlayerManager.Instance.moveSideways(-1);
        }
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            PlayerManager.Instance.moveSideways(1);

        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {

        }
    }
}
