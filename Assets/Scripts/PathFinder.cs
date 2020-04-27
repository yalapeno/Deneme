using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathFinder : MonoBehaviour
{
    /* Implements A* search algorithm for pathfinding */

    Node[,] grid;
    [SerializeField] Vector2Int gridDimentions = new Vector2Int(12, 10); //TODO: custom grid size?



    public Vector2Int GetGridDimentions()
    {
        return gridDimentions;
    }

    private void Start()
    {
        InitilizeGrid();

    }
    private void Update()
    {
        /*
         * For Debugging
         if (Input.GetMouseButtonDown(0)) 
        {

            //GetNeighbours(GetNodeUnderMouse());
            CalculateH(grid[3,3], GetNodeUnderMouse());
        }*/
    }

    public void AStar(Node from, Node to) // A* search logic
    {
        List<Node> openSet = new List<Node>();
        // TODO: Implement A* search
    }

    public static int CalculateH(Node from, Node to)
    {
        /* Heuristic for calculating the H value between two nodes. (Simple block walking). */
        return Mathf.Abs(to.xPos - from.xPos) + Mathf.Abs(to.yPos - from.xPos);
    }

    public List<Node> GetNeighbours(Node node)
    {
        /*Get walkable neighbouring nodes for a giving node*/

        List<Node> neighbours = new List<Node>();
        int xPos;
        int yPos;

        for (int i = -1; i <= 1; i++)
        {
            for (int j = -1; j <= 1; j++)
            {
                xPos = i + node.xPos;
                yPos = j + node.yPos;
                if (Mathf.Abs(i) == Mathf.Abs(j) // no diagonal movement
                    || xPos < 0 || yPos < 0 // check if pos outside grid
                    || xPos >= gridDimentions.x || yPos >= gridDimentions.y
                    || grid[xPos, yPos].GetWall()) // check if walled
                    continue;
                neighbours.Add(grid[xPos, yPos]);
            }
        }
        
        /* For debugging
         
         foreach(Node no in neighbours)
        {
            Debug.Log("x: "+ no.xPos+ " y: "+ no.yPos);
        }*/
        
        return neighbours;

    }

    public void PrintWalls() // For debugging
    {
        for (int i = 0; i < gridDimentions.x; i++)
        {
            for (int j = 0; j < gridDimentions.y; j++)
            {
                Debug.Log(i.ToString() + "  " + j.ToString() + "   " + grid[i, j].GetWall().ToString());
            }
        }
    }

    public void SetWall(Vector2 pos, Vector2 size)
    {
        /*Set tiles as wall when a building is placed*/
        for (int i = 0; i< Mathf.RoundToInt(size.x); i++)
        {
            for (int j = 0; j < Mathf.RoundToInt(size.y); j++)
            {
                grid[Mathf.RoundToInt(pos.x) + i, Mathf.RoundToInt(pos.y) + j].SetWall(true);
            }
        }
    }

    private void InitilizeGrid()
    {
        /* Init Nodes for tiles in the beginning of the game */
        // TODO: It assumes the grid is empty in the beginning. What if there are buildings in the scene in the beginning?
        grid = new Node[gridDimentions.x, gridDimentions.y];
        for (int x = 0; x < gridDimentions.x; x++)
        {
            for (int y = 0; y < gridDimentions.y; y++)
            {
                grid[x, y] = new Node(x, y);
            }
        }
    }
    private Node GetNodeUnderMouse()
    {
        // Get Node for the clicked tile
        Vector2 tileCoord = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        return grid[Mathf.FloorToInt(tileCoord.x), Mathf.FloorToInt(tileCoord.y)];
    }
}
