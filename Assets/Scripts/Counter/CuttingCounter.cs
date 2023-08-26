using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CuttingCounter : BaseCounter {
    public event EventHandler OnCut;
    public event EventHandler<OnProgressChangedEventArgs> OnProgressChanged;
    public class OnProgressChangedEventArgs : EventArgs {
        public float progressNormalized;
    }

    [SerializeField]
    private CuttingRecipieSO[] CuttingRecipieSOArray;

    private int cutTimes;

    protected override void Awake() {
        CounterInit();
        cutTimes = 0;
    }
    public override void Interact(Player player) {
        if (!player.HasKitchenObject() && this.HasKitchenObject()) {
            kitchenObject.SetKitchenObjectParent(player);
            cutTimes = 0;
            // fire the event to notify the progress bar
            OnProgressChanged?.Invoke(this,
                                      new OnProgressChangedEventArgs { progressNormalized = 0f });
        } else if (player.HasKitchenObject() && !this.HasKitchenObject()) {
            player.GetKitchenObject().SetKitchenObjectParent(this);
        }
    }

    public override void InteractAlternate(Player player) {
        if (this.HasKitchenObject() && HasCuttingRecipeSO(GetKitchenObject())) {
            cutTimes++;
            int cutRequired = GetCuttingRecipeSO(kitchenObject).cutRequired;
            // fire event to notify the progress bar
            OnProgressChanged?.Invoke(
                this, new OnProgressChangedEventArgs { progressNormalized =
                                                           (float)cutTimes / cutRequired });
            // fire event to notify animation
            OnCut?.Invoke(this, EventArgs.Empty);
            if (cutTimes >= cutRequired) {
                KitchenObjectSO tmp = GetCutted(GetKitchenObject());
                GetKitchenObject().DestroySelf();
                KitchenObject.SpawnKitchenObject(tmp, this);
            }
        }
    }

    private KitchenObjectSO GetCutted(KitchenObject input) {
        // tmp returns null if not recipe found
        foreach (CuttingRecipieSO tmp in CuttingRecipieSOArray) {
            if (tmp.ingredients == input.GetKitchenObjectSO()) {
                return tmp.result;
            }
        }
        Debug.LogWarning("No CuttingRecipieSO found for" + input.name + "in CuttingCounter" +
                         this.name);
        return null;
    }

    private CuttingRecipieSO GetCuttingRecipeSO(KitchenObject input) {
        foreach (var VARIABLE in CuttingRecipieSOArray) {
            if (VARIABLE.ingredients == input.GetKitchenObjectSO()) {
                return VARIABLE;
            }
        }
        return null;
    }

    private bool HasCuttingRecipeSO(KitchenObject input) {
        foreach (var VARIABLE in CuttingRecipieSOArray) {
            if (VARIABLE.ingredients == input.GetKitchenObjectSO()) {
                return true;
            }
        }
        return false;
    }
}
