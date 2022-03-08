using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public PlayerController playerController;
    public GameGridView gameGridView;
    public bool levelLoaded = false;

    public List<Node> playerPath = new List<Node>();

    public float percentComplete = 0;
    public float winThreshold = 0.7f;

    public List<EnemyController> ghostList =  new List<EnemyController>();

    public bool isWon = false;

    public delegate void OnWinDelegate();
    public OnWinDelegate OnWin;

    public void GetRequiredObjects()
    {

        playerController = GameObject.Find("Player").GetComponent<PlayerController>();
        playerController.OnPositionChange += ChangedPlayerPosition;
        playerController.OnSafeRegionReached += FindRegionToFill;
        gameGridView = GameObject.Find("GridView").GetComponent<GameGridView>();

        GameObject[] temp = GameObject.FindGameObjectsWithTag("Ghost");
        foreach(GameObject ghost in temp)
        {
            ghostList.Add(ghost.GetComponent<EnemyController>());
        }
        //InitLevel();

    }

    public void ChangedPlayerPosition(int Row, int Col, NodeStatus Status)
    {
        //Debug.Log("r:" + Row + "c:" + Col + "s" + Status);
        playerPath.Add(gameGridView.GetNodeFromGameGrid(Row, Col));
        gameGridView.ChangeNodeStatus(Row, Col, Status);
    }

    public void UpdateFillPercent()
    {
        float counter = 0;
        float totalNodes = gameGridView.gridRows * gameGridView.gridCols;
        List<GameNode> temp = gameGridView.GetListOfNodes();
        foreach(GameNode node in temp)
        {
            if(node.nodeStatus == NodeStatus.Safe)
            {
                counter++;
            }
        }

        percentComplete = counter/totalNodes;
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
        UpdateFillPercent();
    }

    public void CheckWin()
    {
        if(percentComplete > winThreshold)
        {
            OnWin?.Invoke();
        }
    }

    public void FindRegionToFill()
    {
        //Debug.Log(gameGridView.PrintMat());
        List<List<Node>> regions = gameGridView.GetListOfRegions();
        List<Node> smallestRegion = null;
        int minCount = int.MaxValue;
        foreach(List<Node> region in regions)
        {
            bool isViableRegion = true;

            foreach(EnemyController ghost in ghostList)
            {
                Node check = gameGridView.GetNodeFromGameGrid(ghost.targetNode.row, ghost.targetNode.col);
                if(region.Contains(check) || check == null)
                {
                    isViableRegion = false;
                    break;
                    
                }
            }

            if(isViableRegion && region.Count < minCount)
            {
                minCount = region.Count;
                smallestRegion = region;
            }
        }

        gameGridView.FillRegion(smallestRegion);
        gameGridView.FillRegion(playerPath);

        playerPath.Clear();
        UpdateFillPercent();
    }

    void Start()
    {
        GetRequiredObjects();      
    }
    
    void Update()
    {
        CheckWin();
        if(levelLoaded == false)
        {
            InitLevel();
        }

        
    }


}
