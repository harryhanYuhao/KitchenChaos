using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearCounter : BaseCounter {
    public override void Interact(Player player) {
        // Debug.Log("ClearCounter Interact");
        if (!player.HasKitchenObject() && this.HasKitchenObject())
        {
            kitchenObject.SetKitchenObjectParent(player); 
        }
        else if (player.HasKitchenObject() && !this.HasKitchenObject())
        {
            player.GetKitchenObject().SetKitchenObjectParent(this);
        } 
    }
}
