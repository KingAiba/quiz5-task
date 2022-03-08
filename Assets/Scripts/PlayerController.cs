using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Vector3 gridPos = new Vector3(0, 0, 0);

   
    public Vector3 targetPos = new Vector3(0, 1, 0);

    public float moveSpeed = 10;

    public InputManager playerInput;
    public bool isMoving = false;

    public GameGridView gridView;

    public LayerMask nodeLayer;

    public GameNode targetNode = null;
    public GameNode prevNode = null;

    public delegate void OnPositionChangeDelegate(int Row, int Col, NodeStatus Status);
    public OnPositionChangeDelegate OnPositionChange;

    public delegate void OnSafeRegionReachedDelegate();
    public OnSafeRegionReachedDelegate OnSafeRegionReached;

    void Start()
    {
        playerInput = GetComponent<InputManager>();
        
    }

    
    void Update()
    {
        RotateTowardsTarget();
        MovePosition();
    }

    private void FixedUpdate()
    {
        ShootRayOnInput(playerInput.inputVector);
    }

    private void ShootRayOnInput(Vector3 pInput)
    {
        float rayDistance = 2f;
        Vector3 offsetPos = new Vector3(0, -0.5f, 0);
        Vector3 rayDir = pInput;
        RaycastHit hit;

        if (Physics.Raycast(transform.position + offsetPos, direction: rayDir, out hit, maxDistance: rayDistance, layerMask: nodeLayer))
        {
            prevNode = targetNode;
            targetNode = hit.collider.GetComponent<GameNode>();
            if (targetNode != null || targetNode.nodeStatus != NodeStatus.None)
            {
                targetPos = new Vector3(targetNode.transform.position.x, 1, targetNode.transform.position.z);
                OnPositionChange?.Invoke(targetNode.row, targetNode.col, NodeStatus.Visited);

                if((prevNode != null) && (prevNode.nodeStatus == NodeStatus.Visited && targetNode.nodeStatus == NodeStatus.Safe))
                {
                    OnSafeRegionReached?.Invoke();
                }
            }
            //Debug.Log(targetPos);
            Debug.DrawRay(transform.position + offsetPos, rayDir * rayDistance, Color.red);

        }
        else
        {
            Debug.DrawRay(transform.position + offsetPos, rayDir * rayDistance, Color.yellow);
        }
    }

    private void RotateTowardsTarget()
    {
        transform.LookAt(targetPos, Vector3.up);
    }

    public void MovePosition()
    {
        if(!isMoving)
        {
            StartCoroutine(MovePlayer());
        }
    }

    private IEnumerator MovePlayer()
    {
        isMoving = true; 
        while(transform.position != targetPos)
        {
            transform.position = Vector3.Lerp(transform.position, targetPos, moveSpeed * Time.deltaTime);
            yield return null;
        }
        isMoving = false;
    }


}
