using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cell : MonoBehaviour
{

    public bool isAlive = false;
    public int numNeighbors = 0;


    public void SetAlive(bool alive)
    {
        isAlive = alive;

        if (alive)
        {
            GetComponent<SpriteRenderer>().enabled = true;
            //transform.gameObject.SetActive(true);
        }
        else
        {
            //transform.gameObject.SetActive(false);
            GetComponent<SpriteRenderer>().enabled = false;
        }

    }

}
