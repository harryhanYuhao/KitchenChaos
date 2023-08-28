// stove counter must have a child transform called StoveCounter_Visual
// which has two child transforms called SizzlingParticles and StoveOnVisual

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class StoveCounter : BaseCounter, IHasProgress
{
    [SerializeField] private FryingRecipeSO[] fryingRecipeSOArray;
    // conforming to IHasProgress, which is related to UI bar
    public event EventHandler<IHasProgress.OnProgressChangedEventArgs> OnProgressChanged;

    public event EventHandler FryingStarted;
    public event EventHandler FryingStopped;
    
    private bool isFrying = false;
    
    private float fryingTimer;
    
    private Animator cookingAnimator;
    private GameObject[] AnimateEffect;

    private void Awake()
    {
        CounterInit();
        Array.Resize(ref AnimateEffect, 2);
        var counterVisual = gameObject.transform.Find("StoveCounter_Visual");
        AnimateEffect[0] = counterVisual.Find("SizzlingParticles").gameObject;
        AnimateEffect[1] = counterVisual.Find("StoveOnVisual").gameObject;
        
        foreach (var VARIABLE in AnimateEffect)
        {
            if (VARIABLE ==  null) Debug.LogError("AnimateEffect"+VARIABLE.name+" not found in " + this.name);
        }
        CookingVisualOff(this, System.EventArgs.Empty);
        fryingTimer = 0f;
        FryingStarted += CookingVisualOn;
        FryingStarted += SetIsFryingTrue;
        FryingStopped += CookingVisualOff;
        FryingStopped += SetIsFryingFalse;
    }

    private void Update()
    {
        if (HasKitchenObject() && HasFryingRecipeSO(GetKitchenObject()))
        {
            if (isFrying == false)
            {
                FryingStarted?.Invoke(this, EventArgs.Empty);
            }
            FryingRecipeSO tmp = GetFryingRecipeSO(GetKitchenObject());
            fryingTimer += Time.deltaTime;
            OnProgressChanged?.Invoke(
                this, new IHasProgress.OnProgressChangedEventArgs { progressNormalized =
                    (float)fryingTimer / tmp.fryingTimeMax});
            if (fryingTimer >= tmp.fryingTimeMax)
            {
                // fried
                fryingTimer = 0f;
                GetKitchenObject().DestroySelf();
                KitchenObject.SpawnKitchenObject(tmp.result, this);
                if (isFrying == true)
                {
                    FryingStopped?.Invoke(this, EventArgs.Empty);
                }
            }
        }
    }

    public override void Interact(Player player) {
        if (!player.HasKitchenObject() && this.HasKitchenObject()) {
            kitchenObject.SetKitchenObjectParent(player);
            FryingStopped?.Invoke(this, System.EventArgs.Empty);
            OnProgressChanged?.Invoke(
                this, new IHasProgress.OnProgressChangedEventArgs { progressNormalized = 0});
            // player shall not put plate on stove
        } else if (player.HasKitchenObject() && !this.HasKitchenObject())
        {
            if (player.HasPlate()) return;
            fryingTimer = 0f;
            player.GetKitchenObject().SetKitchenObjectParent(this);
        } else if (player.HasKitchenObject() && this.HasKitchenObject())
        {
            if (player.GetKitchenObject() is not PlateObject)
            {
                return;
            }
            else // player has plate 
            {
                GetKitchenObject().TryTransferOnToPlate(player.GetPlate());
                OnProgressChanged?.Invoke(
                    this, new IHasProgress.OnProgressChangedEventArgs { progressNormalized = 0});
                FryingStopped?.Invoke(this, System.EventArgs.Empty);
            }
        }
    }
    
    private KitchenObjectSO GetFried(KitchenObject input) {
        // tmp returns null if not recipe found
        foreach (FryingRecipeSO tmp in fryingRecipeSOArray) {
            if (tmp.ingredients == input.GetKitchenObjectSO()) {
                return tmp.result;
            }
        }
        Debug.LogWarning("No Frying Recipe found for" + input.name + "in CuttingCounter" +
                         this.name);
        return null;
    }
    
    private FryingRecipeSO GetFryingRecipeSO(KitchenObject input) {
        foreach (var VARIABLE in fryingRecipeSOArray) {
            if (VARIABLE.ingredients == input.GetKitchenObjectSO()) {
                return VARIABLE;
            }
        }
        return null;
    }
    
    private bool HasFryingRecipeSO(KitchenObject input) {
        foreach (var VARIABLE in fryingRecipeSOArray) {
            if (VARIABLE.ingredients == input.GetKitchenObjectSO()) {
                return true;
            }
        }
        return false;
    }
    private void CookingVisualOn(object sender, System.EventArgs e) {
        foreach (var VARIABLE in AnimateEffect)
        {
            VARIABLE.SetActive(true);
        }
    }
    
    private void CookingVisualOff(object sender, System.EventArgs e) {
        foreach (var VARIABLE in AnimateEffect)
        {
            VARIABLE.SetActive(false);
        }
    }
    
    private void SetIsFryingTrue(object sender, System.EventArgs e) {
        isFrying = true;
    }
    
    private void SetIsFryingFalse(object sender, System.EventArgs e) {
        isFrying = false;
    }
}
