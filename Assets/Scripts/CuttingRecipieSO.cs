using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class CuttingRecipieSO : ScriptableObject
{
    public KitchenObjectSO ingredients;
    public KitchenObjectSO result;
    public int cutRequired;
}
