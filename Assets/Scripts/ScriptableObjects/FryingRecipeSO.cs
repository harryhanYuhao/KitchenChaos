using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class FryingRecipeSO : ScriptableObject {
    public KitchenObjectSO ingredients;
    public KitchenObjectSO result;
    public int fryingTimeMax;
}
