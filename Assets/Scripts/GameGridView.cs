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

    List<GameNode> gameNodeList = new List<GameNode>();

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

        Node matNode = myGameGrid.NodeMatrix[row, col];

        matNode.OnStatusChange += gameNode.SetStatus;


        gameNodeList.Add(gameNode);
    }

    public void DestroyGameGridView()
    {
        foreach(GameNode gn in gameNodeList)
        {
            Node matNode = myGameGrid.NodeMatrix[gn.row, gn.col];
            matNode.OnStatusChange -= gn.SetStatus;
        }
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

    public void ChangeNodeStatus(int Row, int Col, NodeStatus nodeStatus)
    {
        //Debug.Log("r:" + Row + "c:" + Col + "s" + nodeStatus);

        myGameGrid.ChangeStatus(Row, Col, nodeStatus);
        //Debug.Log(myGameGrid.PrintMat());
    }

    public Node GetNodeFromGameGrid(int Row, int Col)
    {
        return myGameGrid.NodeMatrix[Row, Col];
    }

    private void Awake()
    {
        InitializeGridView();
    }

    void Start()
    {
        
        DrawGrid();
    }

    
    void Update()
    {
        
    }

    private void OnDestroy()
    {
        DestroyGameGridView();
    }
}
