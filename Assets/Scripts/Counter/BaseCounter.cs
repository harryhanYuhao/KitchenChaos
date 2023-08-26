using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseCounter : MonoBehaviour, IKitchenObjectParent {
    protected KitchenObject kitchenObject;

    [SerializeField]
    protected Transform counterTopPoint;

    // CounterInit shall be called for all objects inheriting from BaseCounter
    // Place it in Awake()
    protected void CounterInit() {
        Debug.Log(name + "init");
        this.kitchenObject = null;
        // automatically set counter top point
        counterTopPoint = gameObject.transform.Find("CounterTopPoint");
        if (counterTopPoint == null) {
            Debug.LogError("CounterTopPoint not found");
        }
    }

    protected virtual void Awake() { this.CounterInit(); }

    public virtual void Interact(Player player) { Debug.LogWarning("BaseCounter Interact"); }
    public virtual void InteractAlternate(Player player) {
        Debug.Log("BaseCounter Interact Alternate");
    }
    public Transform GetKitchenObjectFollowTransform() { return counterTopPoint; }
    public bool HasKitchenObject() { return kitchenObject != null; }
    public void ClearKitchenObject() { kitchenObject = null; }
    public void SetKitchenObject(KitchenObject kitchenObject) {
        if (kitchenObject != null) this.kitchenObject = kitchenObject;
    }
    public KitchenObject GetKitchenObject() { return kitchenObject; }
}
