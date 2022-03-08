using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public Vector3 inputVector;

    void Start()
    {
        
    }

    
    void Update()
    {
        GetInput();
    }

    public void GetInput()
    {
        inputVector = new Vector3(0, 0, 0);
        if(Input.GetKey(KeyCode.D))
        {
            inputVector.x = 1;
        }
        else if(Input.GetKey(KeyCode.W))
        {
            inputVector.z = 1;
        }
        else if(Input.GetKey(KeyCode.S))
        {
            inputVector.z = -1;
        }
        else if(Input.GetKey(KeyCode.A))
        {
            inputVector.x = -1;
        }
    }
}
