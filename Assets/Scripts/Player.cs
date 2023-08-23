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
        transform.position += moveDir * Time.deltaTime * moveSpeed;

        isWalking =  moveDir != Vector3.zero;
        float RotationSpeed = 10f;
        transform.forward = Vector3.Slerp(transform.forward, moveDir, Time.deltaTime * RotationSpeed);
    }

    public bool IsWalking(){
       // Debug.Log(isWalking);
        return isWalking;
    }
}
