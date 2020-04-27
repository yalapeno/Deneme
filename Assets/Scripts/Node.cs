using System.Collections;
using System.Collections.Generic;


public class Node 
{
    /* Node for pathfinding */
    public int xPos;
    public int yPos;
    bool wall = false; // assuming nothing is in the scene at Start
    Node parentNode;
    int h; // from start to here
    int g = int.MaxValue; // from here to target
    int f { get { return h + g; } }

    public Node(int xPos, int yPos)
    {
        this.xPos = xPos;
        this.yPos = yPos;
    }
    public bool GetWall()
    {
        return wall;
    }

    public void SetWall(bool wall)
    {
        this.wall = wall;
    }

}
