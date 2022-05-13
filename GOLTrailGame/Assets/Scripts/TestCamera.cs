using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestCamera : MonoBehaviour
{
    public void Change()
    {
        if (Input.GetKeyUp(KeyCode.C))
        {
            TestInput();
        }
    }

    public void TestInput()
    {
        GetComponent<Camera>().backgroundColor = Color.white;
    }
}