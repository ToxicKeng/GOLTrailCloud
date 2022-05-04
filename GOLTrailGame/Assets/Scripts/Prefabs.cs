using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Prefabs : MonoBehaviour
{
    public Game script;
    public void PresetInputData(int val)
    {
        //if (val == 0)
        //{
        //    UserInputPreset();
        //    PlaceCells(1);

        //}

        if (val == 1)
        {
            script.UserInputPreset();
            script.PlaceCells(2);

        }

        if (val == 2)
        {
            script.UserInputPreset();
            script.PlaceCells(3);

        }

        if (val == 3)
        {
            script.UserInputPreset();
            script.PlaceCells(4);
        }
        if (val == 4)
        {
            script.UserInputPreset();
            script.PlaceCells(5);
        }
        if (val == 5)
        {
            script.UserInputPreset();
            script.PlaceCells(6);
        }
    }


}