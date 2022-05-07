using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Game : MonoBehaviour
{

    private static int SCREEN_WIDTH = 64; //- 1024 pixels
    private static int SCREEN_HEIGHT = 48; //- 768 pixels


    private float speed = 0.1f;

    private float timer = 0;

    public bool simulationEnabled = false;

    [SerializeField] Slider speedSlider;

    public void ChangeSpeed(float newSpeed)
    {
        speed = newSpeed;
    }



    List<Cell> yellowCells = new List<Cell>();

    Cell[,] grid = new Cell[SCREEN_WIDTH, SCREEN_HEIGHT];

    // Start is called before the first frame update
    void Start()
    {
        for (int y = 0; y < SCREEN_HEIGHT; y++)
        {
            for (int x = 0; x < SCREEN_WIDTH; x++)
            {
                Cell cell = Instantiate(Resources.Load("Prefabs/Cell", typeof(Cell)), new Vector2(x, y), Quaternion.identity) as Cell;
                grid[x, y] = cell;
                grid[x, y].SetAlive(false);
            }
        }
        PlaceCells(1);
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

    //The clicking method I need help with but the reset, Pause and start I did myself.
    void UserInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 mousePoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            int x = Mathf.RoundToInt(mousePoint.x);
            int y = Mathf.RoundToInt(mousePoint.y);

            if (x >= 0 && y >= 0 && x < SCREEN_WIDTH && y < SCREEN_HEIGHT)
            {
                //-- we are in bounds
                grid[x, y].SetAlive(!grid[x, y].isAlive);

                if (grid[x, y].GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Prefabs/Yellow"))
                {
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
        //Reset
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
    //I got help to do 1 but once I knew how to do it I was able to do the rest of the presets.
    public void PlaceCells(int type)
    {
        yellowCells.Clear();
        //Nothing
        if (type == 1)
        {
            for (int y = 0; y < SCREEN_HEIGHT; y++)
            {
                for (int x = 0; x < SCREEN_WIDTH; x++)
                {
                    grid[x, y].SetAlive(false);
                }
            }
        }
        //Random
        if (type == 2)
        {
            for (int y = 0; y < SCREEN_HEIGHT; y++)
            {
                for (int x = 0; x < SCREEN_WIDTH; x++)
                {

                    int rand = UnityEngine.Random.Range(0, 100);

                    if (rand > 75)
                    {
                        grid[x, y].SetAlive(true);
                    }

                }
            }
        }
        //Kite
        if (type == 3)
        {

            for (int y = 21; y < 24; y++)
            {
                for (int x = 30; x < 33; x++)
                {

                    if (x != 33)
                    {
                        if (y == 21 || y == 23 && ((x == 31)) || y == 22 && (x == 32))
                        {
                            grid[x, y].SetAlive(true);
                        }

                    }

                }
            }

        }

        //Blinker
        if (type == 4)
        {

            for (int y = 21; y < 22; y++)
            {
                for (int x = 30; x < 33; x++)
                {
                    if (x != 33)
                    {
                        if (y == 21)
                        {
                            grid[x, y].SetAlive(true);
                        }
                    }
                }
            }

        }
        //Toad
        if (type == 5)
        {

            for (int y = 21; y < 23; y++)
            {
                for (int x = 32; x < 36; x++)
                {

                    if (y == 21 && (x != 35) || y == 22 && (x != 32))
                    {
                        grid[x, y].SetAlive(true);
                    }
                }
            }

        }
        //Space Ship
        if (type == 6)
        {

            for (int y = 21; y < 25; y++)
            {
                for (int x = 30; x < 35; x++)
                {

                    if (y == 21 && (x != 34) || y == 22 && (x != 31 && x != 32 && x != 33) || x == 30 && (y != 24) || y == 24 && (x != 30 && x != 32 && x != 33))
                    {
                        grid[x, y].SetAlive(true);
                    }

                }
            }

        }
        if (type == 7)
        {

            for (int y = 0; y < SCREEN_HEIGHT; y++)
            {
                for (int x = 0; x < SCREEN_WIDTH; x++)
                    {

                    if (y == 0 || y == 47 || x == 0 || x == 63)
                    {
                        grid[x, y].SetAlive(true);
                    }

                }
            }

        }
    }

    //Making my code more efficient (also my code)
    public void UserInputPreset()
    {
        for (int y = 0; y < SCREEN_HEIGHT; y++)
        {
            for (int x = 0; x < SCREEN_WIDTH; x++)
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

    //my code to create a dropdown menu.
    public void PresetInputData(int val)
    {

        if (val == 1)
        {
            UserInputPreset();
            PlaceCells(2);

        }

        if (val == 2)
        {
            UserInputPreset();
            PlaceCells(3);

        }

        if (val == 3)
        {
            UserInputPreset();
            PlaceCells(4);
        }
        if (val == 4)
        {
            UserInputPreset();
            PlaceCells(5);
        }
        if (val == 5)
        {
            UserInputPreset();
            PlaceCells(6);
        }
        if (val == 6)
        {
            UserInputPreset();
            PlaceCells(7);
        }
    }
    //This code was from a video
    void CountNeighbors()
    {
        for (int y = 0; y < SCREEN_HEIGHT; y++)
        {
            for (int x = 0; x < SCREEN_WIDTH; x++)
            {
                int numNeighbors = 0;

                //- North
                if (y + 1 < SCREEN_HEIGHT)
                {
                    if (grid[x, y + 1].isAlive)
                    {
                        numNeighbors++;
                    }
                }
                //-East
                if (x + 1 < SCREEN_WIDTH)
                {
                    if (grid[x + 1, y].isAlive)
                    {
                        numNeighbors++;
                    }
                }
                //-South
                if (y - 1 >= 0)
                {
                    if (grid[x, y - 1].isAlive)
                    {
                        numNeighbors++;
                    }
                }
                //-West
                if (x - 1 >= 0)
                {
                    if (grid[x - 1, y].isAlive)
                    {
                        numNeighbors++;
                    }
                }
                //-NothEast
                if (x + 1 < SCREEN_WIDTH && y + 1 < SCREEN_HEIGHT)
                {
                    if (grid[x + 1, y + 1].isAlive)
                    {
                        numNeighbors++;
                    }
                }
                //-NothWest
                if (x - 1 >= 0 && y + 1 < SCREEN_HEIGHT)
                {
                    if (grid[x - 1, y + 1].isAlive)
                    {
                        numNeighbors++;
                    }
                }
                //-SouthEast
                if (x + 1 < SCREEN_WIDTH && y - 1 >= 0)
                {
                    if (grid[x + 1, y - 1].isAlive)
                    {
                        numNeighbors++;
                    }
                }
                //-SouthWest
                if (x - 1 >= 0 && y - 1 >= 0)
                {
                    if (grid[x - 1, y - 1].isAlive)
                    {
                        numNeighbors++;
                    }
                }
                grid[x, y].numNeighbors = numNeighbors;

            }
        }
    }
    //most of this code was from a video
    void PopulationControl()
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
                    if (grid[x, y].numNeighbors != 2 && grid[x, y].numNeighbors != 3)
                    {
                        grid[x, y].SetAlive(false);

                        yellowCells.Add(grid[x, y]);
                        TrailDelay trail = new TrailDelay(x, y, grid);
                        StartCoroutine(trail.TrailFade());

                    }
                    else
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
    //my code to create trail (help from alistair)
    private void RemoveYellowCells()
    {
        for (int i = 0; i < yellowCells.Count; i++)
        {
            yellowCells[i].GetComponent<SpriteRenderer>().enabled = false;
        }
        yellowCells.Clear();
    }

    class TrailDelay
    {
        private float delay = 0.2f;
                Cell[,] grid;
        int x;
        int y;
        public TrailDelay(int x, int y, Cell[,] Grid)
        {
            this.x = x;
            this.y = y;
            grid = Grid;
        }
        public IEnumerator TrailFade()
        {
            grid[x, y].GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Prefabs/Yellow");
            grid[x, y].GetComponent<SpriteRenderer>().enabled = true;

            yield
            return new WaitForSeconds(delay);

            grid[x, y].GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Prefabs/Yellow25");
            grid[x, y].GetComponent<SpriteRenderer>().enabled = true;

            yield
            return new WaitForSeconds(delay);

            grid[x, y].GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Prefabs/Yellow50");
            grid[x, y].GetComponent<SpriteRenderer>().enabled = true;

            yield
            return new WaitForSeconds(delay);

            grid[x, y].GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Prefabs/Yellow75");
            grid[x, y].GetComponent<SpriteRenderer>().enabled = true;

            yield
            return new WaitForSeconds(delay);

            if (grid[x, y].isAlive)
            {
                grid[x, y].GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Prefabs/Red");
                grid[x, y].GetComponent<SpriteRenderer>().enabled = true;
            }
            else
            {
                grid[x, y].GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Prefabs/Red");
                grid[x, y].GetComponent<SpriteRenderer>().enabled = false;
            }
        }
    }
}