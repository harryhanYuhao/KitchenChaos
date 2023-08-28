using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashCounter : BaseCounter
{
    // Suscribed to this event: Sound Manager
    public static event EventHandler onTrash;
    public override void Interact(Player player) {
        if (player.HasKitchenObject()) {
            onTrash?.Invoke(this, EventArgs.Empty);
            if (player.GetKitchenObject() is PlateObject)
                // if holding plate: first emptying the plate, then destroy the plate if the plate holds nothing
            {
                var tmp = player.GetKitchenObject() as PlateObject;
                if (tmp.Loaded())
                {
                    tmp.DestroyOnPlate();
                }
                else
                {
                    tmp.DestroySelf();
                }
            }
            else
            {
                player.GetKitchenObject().DestroySelf();
            }
        }
    }
}
