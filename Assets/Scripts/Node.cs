using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum NodeStatus
{
    Empty,
    Safe,   
    Visited,
    Destroy,
    None,
}

public class Node
{
    public int row;
    public int col;

    public NodeStatus nodeStatus = NodeStatus.Empty;

    public delegate void OnStatusChangeDelegate(NodeStatus Status);
    public OnStatusChangeDelegate OnStatusChange;

    public Node(int Row, int Col)
    {
        row = Row;
        col = Col;
    }

    public void SetStatus(NodeStatus Status)
    {
        nodeStatus = Status;
        OnStatusChange?.Invoke(Status);
    }


}
