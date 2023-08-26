using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This is a class holding the proper prefabs for the kitchen object
[CreateAssetMenu()]
public class KitchenObjectSO : ScriptableObject {
    // contains the original prefab
    public GameObject prefab;
    public Sprite sprite;
    public string objectName;
}
