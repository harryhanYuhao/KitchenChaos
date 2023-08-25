using System.Collections;
using System.Collections.Generic;
using UnityEditor.UI;
using UnityEngine;

public class ContainerCounter : BaseCounter{
    [SerializeField]
    private KitchenObjectSO kitchenObjectSO;

    private Animator animator;
    private GameObject objectSprite;

    private void Awake()
    {
        this.kitchenObject = null;
        // find the visual game object
        GameObject visual = gameObject.transform.Find("ContainerCounter_Visual").gameObject;
        animator = visual.GetComponent<Animator>();
        Debug.Log(visual);
        Debug.Log(animator);
        // Automatically set the sprite based on the kitchenObjectSO
        GameObject tmp = visual.transform.Find("Single door").gameObject;
        objectSprite = tmp.transform.Find("ObjectSprite").gameObject;
        var SpriteRenderer = objectSprite.GetComponent<SpriteRenderer>();
        SpriteRenderer.sprite = kitchenObjectSO.sprite;
        Debug.Log(SpriteRenderer);
    }
    public override void Interact(Player player) {
        // Debug.Log("Container Interact");
        if (!player.HasKitchenObject()) {
            Transform kitchenObjectTransform = Instantiate(kitchenObjectSO.prefab, counterTopPoint);
            kitchenObject = kitchenObjectTransform.GetComponent<KitchenObject>();
            kitchenObject.SetKitchenObjectParent(player);
            
            // visual
            animator.SetTrigger("OpenClose");
        } 
    }
}
