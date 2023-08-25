using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CuttingCounter : BaseCounter
{
    [SerializeField]
    private KitchenObjectSO kitchenObjectSO;

    private void Awake() { this.kitchenObject = null; }

    public override void Interact(Player player) {
    }
}
