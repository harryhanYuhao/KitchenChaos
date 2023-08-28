using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameInput : MonoBehaviour {
    public static GameInput Instance { get; private set; }
    public event EventHandler OnInteractAction;
    public event EventHandler OnInteractAlternateAction;
    public event EventHandler OnPauseAction;
    private PlayerInputActions playerInputActions;

    private void Awake()
    {
        Instance = this;
        // This is not destroyed automatically on scene change
        // must be destroyed manually
        playerInputActions = new PlayerInputActions();
        playerInputActions.Player.Enable();

        playerInputActions.Player.Interact.performed += Interact_performed;
        playerInputActions.Player.InteractAlternate.performed += InteractAlternate_performed;
        playerInputActions.Player.Pause.performed += Pasue_performed;
    }

    private void OnDestroy()
    {
        playerInputActions.Player.Interact.performed -= Interact_performed;
        playerInputActions.Player.InteractAlternate.performed -= InteractAlternate_performed;
        playerInputActions.Player.Pause.performed -= Pasue_performed;
        
        playerInputActions.Dispose();
    }

    private void Interact_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj) {
        // ignore if it is null event
        OnInteractAction?.Invoke(this, EventArgs.Empty);
    }

    private void 
        InteractAlternate_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj) {
        // ignore if it is null event
        OnInteractAlternateAction?.Invoke(this, EventArgs.Empty);
    }

    private void Pasue_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj) {
        OnPauseAction?.Invoke(this, EventArgs.Empty);
    }
    
    public Vector2 GetMovementVectorNormalized() {
        Vector2 inputVector = playerInputActions.Player.MOVE.ReadValue<Vector2>();
        inputVector = inputVector.normalized;
        // Debug.Log(inputVector);
        return inputVector;
    }
}
