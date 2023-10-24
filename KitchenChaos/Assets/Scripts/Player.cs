using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{   
    [SerializeField] private float moveSpeed = 7f;
    [SerializeField] private GameInput gameInput;
    private bool isWalking;
    
    private void Update()
    {
        Vector2 inputVector = gameInput.GetMovementVectorNormalized();

        Vector3 moveDir = new Vector3(inputVector.x, 0f, inputVector.y);
        transform.position += moveDir  * moveSpeed * Time.deltaTime;
        //use Time.deltaTime as unity will calculate using fps, higher fps will cause higher movement rate
        //so Time.deltaTime will tackle this issue and everyone will move at same rate

        isWalking = moveDir != Vector3.zero;
        
        float rotationSpeed = 10f;
        transform.forward = Vector3.Slerp(transform.forward, moveDir, Time.deltaTime * rotationSpeed);
        //make rotation of character smooth

        
    }

    public bool IsWalking()
    {
        return isWalking;
    }
}
