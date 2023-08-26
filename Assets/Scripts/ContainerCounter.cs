using System.Collections;
using System.Collections.Generic;
using UnityEditor.UI;
using UnityEngine;
using UnityEngine.InputSystem.LowLevel;

public class ContainerCounter : BaseCounter{
    [SerializeField]
    private KitchenObjectSO kitchenObjectSO;

    private Animator openCloseAnimator;
    // private GameObject objectSprite;

    protected override void Awake()
    {
        this.CounterInit();
        if (kitchenObjectSO == null) Debug.LogError("kitchenObjectSO not found");
        // find the visual game object
        GameObject visual = gameObject.transform.Find("ContainerCounter_Visual").gameObject;
        if (visual == null) Debug.LogError("ContainerCounter_Visual not found");
        
        openCloseAnimator = visual.GetComponent<Animator>();
        if (openCloseAnimator == null) Debug.LogError("animator not found"); 
        
        // Automatically set the sprite based on the kitchenObjectSO
        GameObject tmp = visual.transform.Find("Single door").gameObject;
        if (tmp == null) Debug.LogError("Single door not found");
            
        var objectSprite = tmp.transform.Find("ObjectSprite").gameObject;
        if (objectSprite == null) Debug.LogError("ObjectSprite not found");
        
        var SpriteRenderer = objectSprite.GetComponent<SpriteRenderer>();
        if (SpriteRenderer == null) Debug.LogError("SpriteRenderer not found");

        if (kitchenObjectSO.sprite == null) Debug.LogError("kitchenObjectSO.sprite not found");
        if (SpriteRenderer.sprite == null) Debug.LogError("SpriteRenderer.sprite not found");
        SpriteRenderer.sprite = kitchenObjectSO.sprite;
        
        // in Interact the kitchonObject so must have a prefab attached
        if (kitchenObjectSO.prefab == null) Debug.LogError("kitchenObjectSO.prefab not found");
    }
    public override void Interact(Player player) {
        // Debug.Log("Container Interact");
        if (!player.HasKitchenObject()) {
            KitchenObject.SpawnKitchenObject(kitchenObjectSO, player);
            // visual
            openCloseAnimator.SetTrigger("OpenClose");
        } 
    }
}
