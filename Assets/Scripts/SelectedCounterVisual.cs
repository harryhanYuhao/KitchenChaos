using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectedCounterVisual : MonoBehaviour
{
    [SerializeField] private ClearCounter clearCounter;
    [SerializeField] private GameObject visualGameObject;
    // Start is called before the first frame update

    private void Start()
    {
        Player.Instance.OnSelectedCounterChanged += Player_OnSelectedCounterChanged;
    }

    private void Player_OnSelectedCounterChanged(object sender, Player.OnSelectedCounterChangedEventArgs e){
        if (e.selectedCounter == clearCounter){
            show();
        } else {
            hide();
        }
    }

    // Update is called once per frame
    void Update()
    {
    }
    private void show(){
        visualGameObject.SetActive(true);
    }
    private void hide(){
        visualGameObject.SetActive(false);
    }
}
