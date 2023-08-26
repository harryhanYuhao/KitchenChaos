// stove counter must have a child transform called StoveCounter_Visual
// which has two child transforms called SizzlingParticles and StoveOnVisual

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoveCounter : BaseCounter
{
    [SerializeField] private FryingRecipeSO[] fryingRecipeSOArray;
    // Start is called before the first frame update

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
        CookingVisualOff();
        fryingTimer = 0f;
    }

    private void Update()
    {
        if (HasKitchenObject() && HasFryingRecipeSO(GetKitchenObject()))
        {
            CookingVisualOn();
            fryingTimer += Time.deltaTime;
            FryingRecipeSO tmp = GetFryingRecipeSO(GetKitchenObject());
            if (fryingTimer >= tmp.fryingTimeMax)
            {
                // fried
                fryingTimer = 0f;
                GetKitchenObject().DestroySelf();
                KitchenObject.SpawnKitchenObject(tmp.result, this);
                CookingVisualOff();
            }
        }
    }

    public override void Interact(Player player) {
        if (!player.HasKitchenObject() && this.HasKitchenObject()) {
            kitchenObject.SetKitchenObjectParent(player);
            CookingVisualOff();
        } else if (player.HasKitchenObject() && !this.HasKitchenObject())
        {
            fryingTimer = 0f;
            player.GetKitchenObject().SetKitchenObjectParent(this);
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
    private void CookingVisualOn() {
        foreach (var VARIABLE in AnimateEffect)
        {
            VARIABLE.SetActive(true);
        }
    }
    
    private void CookingVisualOff() {
        foreach (var VARIABLE in AnimateEffect)
        {
            VARIABLE.SetActive(false);
        }
    }
}
