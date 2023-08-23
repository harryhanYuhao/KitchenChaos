using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // Serialized fields are visible in the inspector
    [SerializeField] private float moveSpeed = 5f;

    private GameInput gameInput;

    private bool isWalking;

    private void Awake(){
        gameInput = GameObject.Find("GameInput").GetComponent<GameInput>();
    }

    private void Update(){
        Vector2 inputVector = gameInput.GetMovementVectorNormalized();

        Vector3 moveDir = new Vector3(inputVector.x, 0, inputVector.y);

        float moveDistance = Time.deltaTime * moveSpeed; 
        float playerHeight = 2f;
        float playerRadius = 0.7f;

        bool canMoveX = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, new Vector3(moveDir.x, 0 ,0), moveDistance);
        bool canMoveZ = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, new Vector3(0, 0 ,moveDir.z), moveDistance);
        
        if (canMoveX){
            Vector3 direction = new Vector3(moveDir.x, 0 ,0);
            transform.position += direction * Time.deltaTime * moveSpeed; 
        }      
        if (canMoveZ){
            Vector3 direction = new Vector3(0, 0 ,moveDir.z);
            transform.position += direction * Time.deltaTime * moveSpeed; 
        }

        isWalking = (canMoveX || canMoveZ) && moveDir != Vector3.zero;

        float RotationSpeed = 10f;
        transform.forward = Vector3.Slerp(transform.forward, moveDir, Time.deltaTime * RotationSpeed);
    }

    public bool IsWalking(){
       // Debug.Log(isWalking);
        return isWalking;
    }
}
