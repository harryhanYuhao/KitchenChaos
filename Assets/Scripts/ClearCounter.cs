using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearCounter : BaseCounter {
    [SerializeField]
    private KitchenObjectSO kitchenObjectSO;

    private void Awake() { this.kitchenObject = null; }

    public override void Interact(Player player) {
        // Debug.Log("ClearCounter Interact");
        if (player.HasKitchenObject()) {
            this.SetKitchenObject(player.GetKitchenObject());
            kitchenObject.SetKitchenObjectParent(this);
        } else if (this.HasKitchenObject())
        {
            kitchenObject.SetKitchenObjectParent(player); 
        }
    }
}
