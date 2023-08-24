using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour, IKitchenObjectParent
{
    public static Player Instance { get; private set; }
    public event EventHandler<OnSelectedCounterChangedEventArgs> OnSelectedCounterChanged;
    public class OnSelectedCounterChangedEventArgs : EventArgs
    {
        public ClearCounter selectedCounter;
    }

    // Serialized fields are visible in the inspector
    [SerializeField]
    private float moveSpeed = 5f;
    [SerializeField]
    private LayerMask counterLayerMask;
    [SerializeField]
    private GameInput gameInput;
    [SerializeField]
    private Transform playerHoldPoint;

    public bool isWalking;
    private Vector3 lastInteractDir;
    private ClearCounter selectedCounter;
    private KitchenObject kitchenObject;

    private void Start()
    {
        gameInput.OnInteractAction += GameInput_OnInteractAction;
    }

    private void Awake()
    {
        lastInteractDir = Vector3.zero;
        selectedCounter = null;
        if (Instance != null)
        {
            Debug.LogWarning("There is more than one instance of Player in the scene");
        }
        Instance = this; // a singleton pattern
    }

    private void Update()
    {
        Vector2 inputVector = gameInput.GetMovementVectorNormalized();

        Vector3 moveDir = new Vector3(inputVector.x, 0, inputVector.y);

        if (moveDir != Vector3.zero)
        {
            lastInteractDir = moveDir;
        }

        // Handles the movement
        float moveDistance = Time.deltaTime * moveSpeed;
        float playerHeight = 2f;
        float playerRadius = 0.7f;

        bool canMoveX = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, new Vector3(moveDir.x, 0, 0), moveDistance);
        bool canMoveZ = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, new Vector3(0, 0, moveDir.z), moveDistance);

        if (canMoveX)
        {
            Vector3 direction = new Vector3(moveDir.x, 0, 0);
            transform.position += direction * Time.deltaTime * moveSpeed;
        }
        if (canMoveZ)
        {
            Vector3 direction = new Vector3(0, 0, moveDir.z);
            transform.position += direction * Time.deltaTime * moveSpeed;
        }

        isWalking = (canMoveX || canMoveZ) && moveDir != Vector3.zero;

        float RotationSpeed = 10f;
        transform.forward = Vector3.Slerp(transform.forward, moveDir, Time.deltaTime * RotationSpeed);

        // Handles the interaction
        float interactionDistance = 2f;
        if (Physics.Raycast(transform.position, lastInteractDir, out RaycastHit raycastHit, interactionDistance, counterLayerMask))
        {
            // if the ClearCounter component exists on the object we hit, call the Interact method
            if (raycastHit.transform.TryGetComponent(out ClearCounter clearCounter))
            {

                SetSelectedCounter(clearCounter);
            }
            else
            {
                SetSelectedCounter(null);
            }
        }
        else
        {
            SetSelectedCounter(null);
        }
    }

    private void GameInput_OnInteractAction(object sender, System.EventArgs e)
    {
        if (selectedCounter != null)
            selectedCounter.Interact(this);
    }

    private void SetSelectedCounter(ClearCounter selectedCounter)
    {
        this.selectedCounter = selectedCounter;
        OnSelectedCounterChanged?.Invoke(this, new OnSelectedCounterChangedEventArgs { selectedCounter = selectedCounter });
        // Set_OnSelectedCounterChanged(new OnSelectedCounterChangedEventArgs{selectedCounter = selectedCounter});
    }

    protected virtual void Set_OnSelectedCounterChanged(OnSelectedCounterChangedEventArgs e)
    {
        OnSelectedCounterChanged?.Invoke(this, e);
    }

    public bool IsWalking()
    {
        // Debug. (isWalking);
        return isWalking;
    }

    public bool HasKitchenObject()
    {
        return kitchenObject != null;
    }

    public void ClearKitchenObject()
    {
        kitchenObject = null;
    }

    public void SetKitchenObject(KitchenObject kitchenObject)
    {
        this.kitchenObject = kitchenObject;
    }

    public KitchenObject GetKitchenObject()
    {
        return kitchenObject;
    }

    public Transform GetKitchenObjectFollowTransform()
    {
        return playerHoldPoint;
    }
}
