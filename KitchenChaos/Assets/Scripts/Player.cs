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
        
        //use Time.deltaTime as unity will calculate using fps, higher fps will cause higher movement rate
        //so Time.deltaTime will tackle this issue and everyone will move at same rate
        
        float moveDistance = moveSpeed * Time.deltaTime;
        float playerSize = .7f;
        float playerHeight = 2f;
        
        //bool canMove = !Physics.Raycast(transform.position, moveDir, playerSize);
        //raycast, like laser but player visual cam boleh interpolate dengan wall

        bool canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerSize, moveDir, moveDistance);

        if (!canMove)
        {
            //Cannot Move towards moveDir
            Vector3 moveDirX = new Vector3(moveDir.x, 0, 0);
            canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerSize, moveDirX, moveDistance);

            if (canMove)
            {
                moveDir = moveDirX;
            }else
            {
                Vector3 moveDirZ = new Vector3(0, 0, moveDir.z);
                canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerSize, moveDirZ, moveDistance);
                
                if (canMove)
                {
                    moveDir = moveDirZ;
                }
                else
                {
                    //Cannot MOVE ALL DIRECTION
                }
            }
        }
        if (canMove)
        {
             transform.position += moveDir  * moveDistance;
        }

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
