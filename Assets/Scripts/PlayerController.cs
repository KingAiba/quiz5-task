using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public Vector3 gridPos = new Vector3(0, 0, 0);

    public Vector3 prevPos = new Vector3(0, 0, 0);
    public Vector3 targetPos = new Vector3(0, 1, 0);

    public float moveSpeed = 10;

    public InputManager playerInput;
    public bool isMoving = false;

    //public PlayerGrid gridManager;

    public LayerMask nodeLayer;

    void Start()
    {
        playerInput = GetComponent<InputManager>();
        //gridManager = GameObject.Find("GridObject").GetComponent<PlayerGrid>();
    }

    
    void Update()
    {
        MovePos();
        RotateTowardsTarget();
        //MovePlayer();
    }

    private void FixedUpdate()
    {
        //ShootRayOnInput(playerInput.inputVector);
    }

/*    private void ShootRayOnInput(Vector3 pInput)
    {
        Vector3 offsetPos = new Vector3(0, -0.5f, 0);
        Vector3 rayDir = pInput;
        RaycastHit hit;

        if (!isMoving && Physics.Raycast(transform.position + offsetPos, direction: rayDir, out hit, maxDistance: 2f, layerMask: nodeLayer))
        {
            Node targetNode = hit.collider.GetComponent<Node>();
            if (targetNode != null)
            {
                targetPos = new Vector3(targetNode.worldPosition.x, 1, targetNode.worldPosition.z);
            }
            //Debug.Log(hit.collider.GetComponent<Node>().nodePosition);
            Debug.DrawRay(transform.position + offsetPos, rayDir * 2f, Color.red);

        }
        else
        {
            Debug.DrawRay(transform.position + offsetPos, rayDir * 2f, Color.yellow);          
        }
    }*/

    private void MovePos()
    {
/*        if((gridPos.x + playerInput.inputVector.x < gridManager.gridSize.x && gridPos.z + playerInput.inputVector.z < gridManager.gridSize.y)
            && (gridPos.x + playerInput.inputVector.x >= 0 && gridPos.z + playerInput.inputVector.z >= 0)
            && !isMoving)
        {
            gridPos += playerInput.inputVector;
            targetPos = gridManager.GetNodeWorldPos(gridPos);
            targetPos = new Vector3(targetPos.x, 1, targetPos.z);
            StartCoroutine(MovePlayer());
            
        }*/
        if(!isMoving)
        {
            StartCoroutine(MovePlayer());
        }
    }

    private void RotateTowardsTarget()
    {
        transform.LookAt(targetPos, Vector3.up);
    }

    private IEnumerator MovePlayer()
    {
        isMoving = true;
        prevPos = transform.position;
        while(transform.position != targetPos)
        {
            transform.position = Vector3.Lerp(transform.position, targetPos, moveSpeed * Time.deltaTime);
            yield return null;
        }
        isMoving = false;
    }


}
