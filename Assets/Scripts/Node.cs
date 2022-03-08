using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum NodeStatus
{
    None,
    Safe,
    Empty,
    Visited,
}

public class Node : MonoBehaviour
{
    public int row;
    public int col;
    public NodeStatus nodeStatus = NodeStatus.None;

    public Node(int Row, int Col)
    {
        row = Row;
        col = Col;
    }
}
