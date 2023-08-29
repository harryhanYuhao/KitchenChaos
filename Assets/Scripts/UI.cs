// as a parent class for various UIs
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI : MonoBehaviour
{
    protected void Show()
    {
        gameObject.SetActive(true);
    }
    
    protected void Hide()
    {
        gameObject.SetActive(false);
    }
}
