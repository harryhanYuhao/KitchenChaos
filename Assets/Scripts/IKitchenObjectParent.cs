using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IKitchenObjectParent 
{
    public bool HasKitchenObject();

    public void ClearKitchenObject();

    public void SetKitchenObject(KitchenObject kitchenObject);

    public KitchenObject GetKitchenObject();

    public Transform GetKitchenObjectFollowTransform();
}
