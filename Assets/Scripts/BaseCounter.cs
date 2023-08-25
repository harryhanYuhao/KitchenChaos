using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseCounter : MonoBehaviour, IKitchenObjectParent{
    protected KitchenObject kitchenObject;

    [SerializeField] protected Transform counterTopPoint;
    public virtual void Interact(Player player) {
        Debug.LogWarning("BaseCounter Interact");
    }
    public Transform GetKitchenObjectFollowTransform() { return counterTopPoint; }
    public bool HasKitchenObject() { return kitchenObject != null; }
    public void ClearKitchenObject() { kitchenObject = null; }
    public void SetKitchenObject(KitchenObject kitchenObject) {
        this.kitchenObject = kitchenObject;
    }
    public KitchenObject GetKitchenObject() { return kitchenObject; }
}
