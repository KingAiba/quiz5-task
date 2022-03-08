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
    public Vector3 nodePosition;
    public NodeStatus nodeStatus = NodeStatus.None;

    public Node(Vector3 pos)
    {
        nodePosition = pos;
    }
}
