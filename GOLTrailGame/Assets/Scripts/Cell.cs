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
            //GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Prefabs/Red");
        }
        else
        {
            GetComponent<SpriteRenderer>().enabled = false;

            
            //GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Prefabs/Yellow");
            //GetComponent<SpriteRenderer>().sprite = Instantiate(Resources.Load("Prefabs/Cell", typeof(Cell)), new Vector2(x, y), Quaternion.identity) as Cell;

        }

    }

}
