using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameNode : MonoBehaviour
{
    public int row;
    public int col;

    public NodeStatus nodeStatus = NodeStatus.Empty;

    public List<GameObject> childObjects;

    public void InitGameNode(int Row, int Col)
    {
        row = Row;
        col = Col;

        GetAllChildren();
        NodeStatusUpdate();
    }


    private void GetAllChildren()
    {
        foreach (Transform child in transform)
        {
            childObjects.Add(child.gameObject);
        }
    }

    public void SetStatus(NodeStatus Status)
    {
        nodeStatus = Status;
        NodeStatusUpdate();
    }

    public void NodeStatusUpdate()
    {
        for (int i = 0; i < childObjects.Count; i++)
        {
            if (i == (int)nodeStatus)
            {
                childObjects[i].SetActive(true);
            }
            else
            {
                childObjects[i].SetActive(false);
            }
        }
    }

    void Start()
    {
        
    }

    
    void Update()
    {
        
    }


}
