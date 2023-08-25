using System.Collections;
using System.Collections.Generic;
using UnityEditor.UI;
using UnityEngine;

public class ContainerCounter : BaseCounter{
    [SerializeField]
    private KitchenObjectSO kitchenObjectSO;

    private Animator animator;

    private void Awake()
    {
        this.kitchenObject = null;
        // find the visual game object
        GameObject visual = gameObject.transform.Find("ContainerCounter_Visual").gameObject;
        animator = visual.GetComponent<Animator>();
        Debug.Log(visual);
        Debug.Log(animator);
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
