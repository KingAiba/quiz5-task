using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameGrid : Matrix
{
    public Node[,] NodeMatrix;

    public delegate void StatusChangeDelegate(int Row, int Col, NodeStatus Status);
    public StatusChangeDelegate StatusChange;


    public GameGrid(int Rows, int Cols) : base(Rows, Cols)
    {
        InitializeNodeMatrix();
    }

    public GameGrid(int[,] Arr) : base(Arr)
    {
        InitializeNodeMatrix();
    }

    public void InitializeNodeMatrix()
    {
        NodeMatrix = new Node[rows, cols];
        for(int i = 0; i<rows; i++)
        {
            for(int j=0; j<cols;j++)
            {
                NodeMatrix[i, j] = new Node(i, j);
            }
        }
    }

    public void ChangeStatus(int Row, int Col, NodeStatus Status)
    {
        if(mat[Row, Col] != (int)NodeStatus.Safe)
        {
            NodeMatrix[Row, Col].SetStatus(Status);
            mat[Row, Col] = (int)Status;
            StatusChange?.Invoke(Row, Col, Status);
        }
    }

    public List<List<Node>> FindAllEnclosedRegion()
    {
        List<List<Node>> output = new List<List<Node>>();
        int[,] copyOfMat = (int[,])mat.Clone();

        for(int i = 0; i<rows; i++)
        {
            for(int j = 0; j<cols; j++)
            {
                if(copyOfMat[i, j] == 0)
                {
                    output.Add(GetCurrentEnclosedRegion(NodeMatrix[i, j], ref copyOfMat));
                }
            }
        }

        return output;
    }

    public List<Node> GetCurrentEnclosedRegion(Node node, ref int[,] copyOfMat)
    {
        List<Node> output = new List<Node>();
        Queue<Node> nodeQueue = new Queue<Node>();
        nodeQueue.Enqueue(node);
        while(nodeQueue.Count != 0)
        {
            Node tempNode = nodeQueue.Dequeue();
            copyOfMat[tempNode.row, tempNode.col] = -1;

            output.Add(tempNode);

            if(tempNode.row != 0)
            {
                if(copyOfMat[tempNode.row - 1, tempNode.col] == 0)
                {
                    nodeQueue.Enqueue(NodeMatrix[tempNode.row - 1, tempNode.col]);
                }                
            }
            if(tempNode.row != rows - 1)
            {
                if(copyOfMat[tempNode.row + 1, tempNode.col] == 0)
                {
                    nodeQueue.Enqueue(NodeMatrix[tempNode.row + 1, tempNode.col]);
                }              
            }
            if(tempNode.col != 0)
            {
                if (copyOfMat[tempNode.row, tempNode.col - 1] == 0)
                {
                    nodeQueue.Enqueue(NodeMatrix[tempNode.row, tempNode.col - 1]);
                }
            }
            if(tempNode.col != cols - 1)
            {
                if (copyOfMat[tempNode.row, tempNode.col + 1] == 0)
                {
                    nodeQueue.Enqueue(NodeMatrix[tempNode.row, tempNode.col + 1]);
                }
            }
        }

        return output;
    }
}
