using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public PlayerController playerController;
    public GameGridView gameGridView;
    public bool levelLoaded = false;

    public void GetRequiredObjects()
    {
        playerController = GameObject.Find("Player").GetComponent<PlayerController>();
        playerController.OnPositionChange += ChangedPlayerPosition;

        gameGridView = GameObject.Find("GridView").GetComponent<GameGridView>();
        //InitLevel();

    }

    public void ChangedPlayerPosition(int Row, int Col, NodeStatus Status)
    {
        //Debug.Log("r:" + Row + "c:" + Col + "s" + Status);
        gameGridView.ChangeNodeStatus(Row, Col, Status);
    }

    public void InitLevel()
    {
        for(int i=0; i<gameGridView.gridRows; i++)
        {
            for(int j=0; j<gameGridView.gridCols; j++)
            {
                if(i == 0 || j == 0 || i == gameGridView.gridRows-1 || j == gameGridView.gridCols-1)
                {
                    gameGridView.ChangeNodeStatus(i, j, NodeStatus.Safe);
                    //Debug.Log(gameGridView.GetNodeFromGameGrid(i, j).nodeStatus);
                }
            }
        }

        levelLoaded = true;

    }

    public void FindLargestEnclosedRegion()
    {

    }

    private void Awake()
    {
        
    }

    void Start()
    {
        GetRequiredObjects();
        
    }

    
    void Update()
    {
        if(levelLoaded == false)
        {
            InitLevel();
        }
    }


}
