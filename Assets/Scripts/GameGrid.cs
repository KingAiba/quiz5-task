using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameGrid : Matrix
{
    public Node[,] NodeMatrix;

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
}
