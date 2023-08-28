using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KitchenObject : MonoBehaviour {
    [SerializeField]
    private KitchenObjectSO kitchenObjectSO;
    private IKitchenObjectParent kitchenObjectParent;
    private int cutTimes = 0;
    private int cookedTimes = 0;

    private void Awake() {
        this.kitchenObjectParent = null;
        Debug.Log("KitchenObject Awake");
    }

    public void SetKitchenObjectParent(IKitchenObjectParent kitchenObjectParent) {
        if (this.kitchenObjectParent != null) {
            this.kitchenObjectParent.ClearKitchenObject();
        }
        this.kitchenObjectParent = kitchenObjectParent;
        kitchenObjectParent.SetKitchenObject(this);
        transform.parent = kitchenObjectParent.GetKitchenObjectFollowTransform();
        transform.localPosition = Vector3.zero;
    }

    public virtual void DestroySelf() {
        if (kitchenObjectParent != null && (kitchenObjectParent is not PlateObject))
        {
            kitchenObjectParent.ClearKitchenObject();
        }
        Destroy(gameObject);
    }

    public IKitchenObjectParent GetKitchenObjectParent() { return kitchenObjectParent; }
    public KitchenObjectSO GetKitchenObjectSO() { return kitchenObjectSO; }

    public static GameObject SpawnKitchenObject(KitchenObjectSO inSO, IKitchenObjectParent parent) {
        GameObject kitchenObjectGO =
            Instantiate(inSO.prefab, parent.GetKitchenObjectFollowTransform());
        var kitchenObject = kitchenObjectGO.GetComponent<KitchenObject>();
        kitchenObject.SetKitchenObjectParent(parent);
        kitchenObject.kitchenObjectSO = inSO;
        return kitchenObjectGO;
    }

    public static KitchenObject TrySpawnOnPlate(KitchenObjectSO inSO, PlateObject plate)
    {
        GameObject go = Instantiate(inSO.prefab);
        KitchenObject ko = go.GetComponent<KitchenObject>();
        if (plate.TryTransferToPlate(ko))
        {
            return ko;
        }
        ko.DestroySelf();
        return null;
    }
    
    
    public KitchenObject TryTransferOnToPlate(PlateObject plate)
    {
        KitchenObject ko = this.GetComponent<KitchenObject>();
        if (plate.TryTransferToPlate(ko))
        {
            return ko;
        }
        ko.DestroySelf();
        return null;
    }

}