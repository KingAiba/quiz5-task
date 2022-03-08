using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    public Transform target;
    public Vector3 offset;
    void Start()
    {
        offset = transform.position;
    }
  
    void Update()
    {
        
    }

    private void LateUpdate()
    {
        transform.position = target.position + offset;
    }
}
