using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class PlateObject : KitchenObject, IKitchenObjectParent
{
    // set the parent of the transform, conforming to the interface IKitchenObjectParent
    [SerializeField] private Transform topPoint;
    private List<KitchenObject> kitchenObjectList;
    private const int maxObjects = 6;
    // private int objectsHeld;

    // Visual and UIs
    [FormerlySerializedAs("PlateVisual")] [SerializeField] private GameObject PlateIconUI; // in charge of all visual
    [SerializeField] private Transform IconTemplate; // in charge of all visual
    
    // public static events
    public static event EventHandler OnAddedToPlate;
    public static event EventHandler OnPoppedFromPlate;

    void Start()
    {
        kitchenObjectList = new List<KitchenObject>();
        PlateIconUI.SetActive(false);
    }

    public bool TryTransferToPlate(KitchenObject ko)
    {
        if (kitchenObjectList.Count < maxObjects && Compatible(ko))
        {
            // TODO HAS BUG
            if (ko.GetKitchenObjectParent() != null) ko.GetKitchenObjectParent().ClearKitchenObject();
            ko.SetKitchenObjectParent(this);
            ko.transform.parent = topPoint;
            ko.transform.localPosition = new Vector3(0, 0.1f* kitchenObjectList.Count, 0);
            kitchenObjectList.Add(ko);
            VisualUpdate();
            OnAddedToPlate?.Invoke(this, EventArgs.Empty);
            return true;
        }
        return false;
    }

    private void VisualUpdate()
    {
        // quick and dirty: requires refactoring
        foreach (Transform tmp in PlateIconUI.transform)
        {
            if (tmp != IconTemplate)
            {
                Destroy(tmp.GameObject());
            } 
        }

        foreach (var VARIABLE in kitchenObjectList)
        {
            PlateIconUI.SetActive(true);
            var tmp = Instantiate(IconTemplate, PlateIconUI.transform);
            tmp.gameObject.SetActive(true);
            Image sprite = tmp.transform.Find("Sprite").GetComponent<Image>();
            sprite.sprite = VARIABLE.GetKitchenObjectSO().sprite;
        }
    }

    public KitchenObject GetTopKitchenObject()
    {
        if (kitchenObjectList.Any()) return null;
        return kitchenObjectList.Last();
    }
    private bool Compatible(KitchenObject ko)
    {
        if (ko is PlateObject)
        {
            return false;
        }
        return true;
    }
    public bool Loaded()
    {
        return kitchenObjectList.Count > 0;
    }
    public void DestroyOnPlate()
    {
        foreach (var tmp in kitchenObjectList) tmp.DestroySelf();
        kitchenObjectList.Clear();
        VisualUpdate();
    }
    
    public override void DestroySelf()
    {
        DestroyOnPlate();
        base.DestroySelf();
    }

    public bool TryPopFromPlate()
    {
        if (!kitchenObjectList.Any()) return false;
        kitchenObjectList.RemoveAt(kitchenObjectList.Count - 1);
        VisualUpdate();
        OnPoppedFromPlate?.Invoke(this, EventArgs.Empty);
        return true;
    }


    public bool TryMoveLastObjectFromPlateTo(IKitchenObjectParent parent)
    {
        if (!kitchenObjectList.Any()) return false;
        KitchenObject tmp = kitchenObjectList.Last();
        tmp.SetKitchenObjectParent(parent);
        this.TryPopFromPlate();
        VisualUpdate();
        return true;
    }
    
    // the interface may be removed
    public bool HasKitchenObject() { return kitchenObjectList.Count > 0; }

        public bool HasPlate() { return false; }

        // very prone to bugs
        public void ClearKitchenObject()
        {
            // foreach (var tmp in kitchenObjectList) tmp.DestroySelf();
            // kitchenObjectList.Clear();
        }
    
        public void SetKitchenObject(KitchenObject kitchenObject) { }

        public KitchenObject GetKitchenObject() { return null; }


        // TODO: needs to be properly implemented
        public Transform GetKitchenObjectFollowTransform() { return topPoint; }
}
