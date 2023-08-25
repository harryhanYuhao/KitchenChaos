using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContainerCounter : MonoBehaviour, IKitchenObjectParent {
    [SerializeField]
    private KitchenObjectSO kitchenObjectSO;
    [SerializeField]
    private Transform counterTopPoint;

    private KitchenObject kitchenObject;
    // Start is called before the first frame update
    void Start() {}

    // Update is called once per frame
    void Update() {}
    public void Interact(Player player) {
        // Debug.Log("ClearCounter Interact");
        if (kitchenObject == null) {
            Transform kitchenObjectTransform = Instantiate(kitchenObjectSO.prefab, counterTopPoint);
            kitchenObject = kitchenObjectTransform.GetComponent<KitchenObject>();
            kitchenObject.SetKitchenObjectParent(this);
        } else {
            // give object to player
            if (!player.HasKitchenObject()) {
                player.SetKitchenObject(kitchenObject);
                kitchenObject.SetKitchenObjectParent(player);
            }
        }
    }
    public Transform GetKitchenObjectFollowTransform() { return counterTopPoint; }

    public bool HasKitchenObject() { return kitchenObject != null; }

    public void ClearKitchenObject() { kitchenObject = null; }

    public void SetKitchenObject(KitchenObject kitchenObject) {
        this.kitchenObject = kitchenObject;
    }

    public KitchenObject GetKitchenObject() { return kitchenObject; }
}
