using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{

    private static int SCREEN_WIDTH = 64;       //- 1024 pixels
    private static int SCREEN_HEIGHT = 48;      //- 768 pixels

    public float speed = 0.1f;

    private float timer = 0;

    public bool simulationEnabled = false;


    List<Cell> yellowCells = new List<Cell>();

    //public Sprite yellowSprite;


    //[SerializeField] float IterationDelay = 0.3f;
    Cell[,] grid = new Cell[SCREEN_WIDTH, SCREEN_HEIGHT];

    // Start is called before the first frame update
    void Start()
    {
        PlaceCells();
    }

    // Update is called once per frame
    void Update()
    {

        if (simulationEnabled)
        {
            if (timer >= speed)
            {
                timer = 0f;
                CountNeighbors();

                PopulationControl();

            }
            else
            {
                timer += Time.deltaTime;
            }
        }
        UserInput();
       
        
    }


   

    void UserInput()
    {
        if (Input.GetMouseButtonDown(0)) 
        { 
            Vector2 mousePoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            
            int x = Mathf.RoundToInt(mousePoint.x);
            int y = Mathf.RoundToInt(mousePoint.y);

            if(x >= 0 && y >= 0 && x < SCREEN_WIDTH && y < SCREEN_HEIGHT)
            {
                //-- we are in bounds
                grid[x, y].SetAlive(!grid[x, y].isAlive);
                if(grid[x, y].GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Prefabs/Yellow")) {
                    grid[x, y].GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Prefabs/Red");
                }
            }
        }
        if (Input.GetKeyUp(KeyCode.P))
        {
            //-pause simultaion
            simulationEnabled = false;

        }

        if (Input.GetKeyUp(KeyCode.B))
        {
            //build or resume simultaion
            simulationEnabled = true;

        }
        //Some code I wrote myself to reset the simulation
        for (int y = 0; y < SCREEN_HEIGHT; y++)
        {
            for (int x = 0; x < SCREEN_WIDTH; x++)
            {
                if (Input.GetKeyUp(KeyCode.R))
                {
                    if (grid[x, y].isAlive)
                    {
                        grid[x, y].SetAlive(false);
                        grid[x, y].GetComponent<SpriteRenderer>().enabled = false;
                    }
                    RemoveYellowCells();
                }
            }
        }

    }

    void PlaceCells()
    {
        for (int y = 0; y < SCREEN_HEIGHT; y++)
        {
            for (int x = 0; x < SCREEN_WIDTH; x++)
            {
                Cell cell = Instantiate(Resources.Load("Prefabs/Cell", typeof(Cell)), new Vector2(x, y), Quaternion.identity) as Cell;
                 grid[x, y] = cell;
                grid[x, y].SetAlive(RandomAliveCell());

                //DeadCell Deadcell = Instantiate(Resources.Load("Prefabs/Yellow16x16", typeof(DeadCell)), new Vector2(x, y), Quaternion.identity) as DeadCell;
                

            }
        }
    }


    void CountNeighbors()
    {
        for (int y = 0; y < SCREEN_HEIGHT; y++)
        {
            for (int x = 0; x < SCREEN_WIDTH; x++)
            {
                int numNeighbors = 0;

                //- North
                if (y+1 < SCREEN_HEIGHT)
                {
                    if (grid[x,y+1].isAlive)
                    {
                        numNeighbors++;
                    }
                }
                //-East
                if (x+1 < SCREEN_WIDTH)
                {
                    if (grid[x+1,y].isAlive)
                    {
                        numNeighbors++;
                    }
                }
                //-South
                if (y-1 >= 0)
                {
                    if (grid[x,y-1].isAlive)
                    {
                        numNeighbors++;
                    }
                }
                //-West
                if (x-1 >= 0)
                {
                    if (grid[x-1,y].isAlive)
                    {
                        numNeighbors++;
                    }
                }
                //-NothEast
                if (x + 1 < SCREEN_WIDTH && y + 1 < SCREEN_HEIGHT)
                {
                    if (grid[x+1, y+1].isAlive)
                    {
                        numNeighbors++;
                    }
                }
                //-NothWest
                if (x - 1 >= 0 && y + 1 < SCREEN_HEIGHT)
                {
                    if (grid[x-1,y+1].isAlive)
                    {
                        numNeighbors++;
                    }
                }
                //-SouthEast
                if (x + 1 < SCREEN_WIDTH && y - 1 >= 0)
                {
                    if (grid[x+1,y-1].isAlive)
                    {
                        numNeighbors++;
                    }
                }
                //-SouthWest
                if (x - 1 >= 0 && y - 1 >= 0)
                {
                    if (grid[x-1, y-1].isAlive)
                    {
                        numNeighbors++;
                    }
                }
                grid[x, y].numNeighbors = numNeighbors;

            }
        }
    }

    void PopulationControl ()//-15:30 into part 3 vid
    {
        for (int y = 0; y < SCREEN_HEIGHT; y++)
        {
            for (int x = 0; x < SCREEN_WIDTH; x++)
            {
                //-rules
                //-Any live cell with 2 of 3 lives neighbors survives
                //-Any dead cell with 3 live neigbors become a live cell
                //-All other live cells die in next Gen and all other dead cells stay dead

                if (grid[x, y].isAlive)
                {

                    //-cell is Alive
                    if (grid[x,y].numNeighbors != 2 && grid[x, y].numNeighbors != 3)
                    {
                        grid[x, y].SetAlive(false);

                        grid[x, y].GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Prefabs/Yellow");
                        grid[x, y].GetComponent<SpriteRenderer>().enabled = true;
                        yellowCells.Add(grid[x, y]);
                        //print(grid[x, y].GetComponent<SpriteRenderer>().sprite.name);

                        //GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Prefabs/Yellow");

                        //grid[x, y].SetDead(true);

                        //8iplace(Cell, DeadCell);


                        //this.gameObject.GetComponent<SpriteRenderer>().sprite = yellowSprite;

                    } else
                    {
                        grid[x, y].GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Prefabs/Red");
                    }
                    
                }
                else
                {
                    
                    //-cell is dead
                    if (grid[x, y].numNeighbors == 3)
                    {
                        grid[x, y].SetAlive(true);
                        grid[x, y].GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Prefabs/Red");

                    }
                }
            }
        }
    }

     bool RandomAliveCell()
    {
        
        int rand = UnityEngine.Random.Range(0, 100);

        if (rand > 100)
        {
            return true;
        }
        return false; 
    }


    private void RemoveYellowCells()
    {
        for(int i = 0; i < yellowCells.Count; i++)
        {
            yellowCells[i].GetComponent<SpriteRenderer>().enabled = false;
        }
        //print("loop done");
        yellowCells.Clear();
    }
   
}

