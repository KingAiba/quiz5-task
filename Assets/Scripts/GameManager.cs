using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public PlayerController playerController;
    public GameGridView gameGridView;
    public bool levelLoaded = false;

    public List<Node> playerPath = new List<Node>();

    public void GetRequiredObjects()
    {

        playerController = GameObject.Find("Player").GetComponent<PlayerController>();
        playerController.OnPositionChange += ChangedPlayerPosition;
        playerController.OnSafeRegionReached += FindRegionToFill;
        gameGridView = GameObject.Find("GridView").GetComponent<GameGridView>();
        //InitLevel();

    }

    public void ChangedPlayerPosition(int Row, int Col, NodeStatus Status)
    {
        //Debug.Log("r:" + Row + "c:" + Col + "s" + Status);
        playerPath.Add(gameGridView.GetNodeFromGameGrid(Row, Col));
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

    public void FindRegionToFill()
    {
        Debug.Log(gameGridView.PrintMat());
        List<List<Node>> regions = gameGridView.GetListOfRegions();
        List<Node> smallestRegion = null;
        int minCount = int.MaxValue;
        foreach(List<Node> region in regions)
        {
            if(region.Count < minCount)
            {
                minCount = region.Count;
                smallestRegion = region;
            }
        }

        gameGridView.FillRegion(smallestRegion);
        gameGridView.FillRegion(playerPath);

        playerPath.Clear();
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
