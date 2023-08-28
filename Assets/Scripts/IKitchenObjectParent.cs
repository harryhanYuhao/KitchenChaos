using UnityEngine;

public interface IKitchenObjectParent {
    public bool HasKitchenObject();
    public bool HasPlate();

    public void ClearKitchenObject();

    public void SetKitchenObject(KitchenObject kitchenObject);

    public KitchenObject GetKitchenObject();

    public Transform GetKitchenObjectFollowTransform();
}
