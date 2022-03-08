using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameGridView : MonoBehaviour
{

    public int gridRows = 2;
    public int gridCols = 2;

    public float nodeSpacing = 1.0f;
    public Vector3 nodeSize = new Vector3(1.0f, 1.0f, 1.0f);

    public GameObject nodePrefab;

    GameGrid myGameGrid;



    public void InitializeGridView()
    {
        myGameGrid = new GameGrid(gridRows, gridCols);
    }

    public void CreateGameNode(int row, int col)
    {
        GameObject newNode = Instantiate(nodePrefab, new Vector3(row * nodeSpacing, 0, col * nodeSpacing), nodePrefab.transform.rotation);
        newNode.transform.localScale = nodeSize;

        GameNode gameNode = newNode.GetComponent<GameNode>();
        gameNode.InitGameNode(row, col);
    }

    public void DrawGrid()
    {
        for(int i= 0; i<gridRows;i++)
        {
            for(int j=0; j<gridCols;j++)
            {
                CreateGameNode(i, j);
            }
        }

    }


    void Start()
    {
        InitializeGridView();
        DrawGrid();
    }

    
    void Update()
    {
        
    }
}
