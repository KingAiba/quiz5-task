using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public Vector3 currentPosition;
    public Vector3 targetPos;
    public Vector3 moveDirection;

    public float moveSpeed = 10f;

    public bool isMoving = false;

    public LayerMask nodeLayer;

    public GameNode targetNode = null;
    public GameNode prevNode = null;

    public delegate void OnEnterVisitedDelegate();
    public OnEnterVisitedDelegate OnEnterVisited;

    void Start()
    {
        PickDirection();
    }

    
    void Update()
    {
        MovePosition();
    }

    public void PickDirection()
    {
        moveDirection = new Vector3(Random.Range(-1, 2), 0, Random.Range(-1, 2));
    }
    public void RayCastInDirection()
    {
        float rayDistance = 2f;
        Vector3 offsetPos = new Vector3(0, -0.5f, 0);
        Vector3 rayDir = moveDirection;
        RaycastHit hit;

        if (!isMoving && Physics.Raycast(transform.position + offsetPos, direction: rayDir, out hit, maxDistance: rayDistance, layerMask: nodeLayer))
        {
            prevNode = targetNode;
            targetNode = hit.collider.GetComponent<GameNode>();
            if (targetNode == null || targetNode.nodeStatus == NodeStatus.None || targetNode.nodeStatus == NodeStatus.Safe)
            {
                PickDirection();
            }
            else
            {
                targetPos = new Vector3(targetNode.transform.position.x, 1, targetNode.transform.position.z);
                if(targetNode.nodeStatus == NodeStatus.Visited)
                {
                    OnEnterVisited?.Invoke();
                }
            }
            //Debug.Log(targetPos);
            Debug.DrawRay(transform.position + offsetPos, rayDir * rayDistance, Color.cyan);

        }
        else
        {
            Debug.DrawRay(transform.position + offsetPos, rayDir * rayDistance, Color.yellow);
        }
    }

    public void MovePosition()
    {
        if(targetNode != null && targetNode.nodeStatus == NodeStatus.Safe)
        {
            PickDirection();
        }
        if (!isMoving)
        {
            RayCastInDirection();
            StartCoroutine(MovePlayer());
        }
    }

    private IEnumerator MovePlayer()
    {
        isMoving = true;
        while (transform.position != targetPos)
        {
            transform.position = Vector3.Lerp(transform.position, targetPos, moveSpeed * Time.deltaTime);
            yield return null;
        }
        isMoving = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            other.gameObject.GetComponent<PlayerController>().TakeDamage(1);
        }
    }
}
