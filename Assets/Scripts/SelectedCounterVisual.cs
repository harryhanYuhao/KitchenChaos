using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectedCounterVisual : MonoBehaviour {
    [SerializeField]
    private BaseCounter baseCounter;
    [SerializeField]
    private GameObject [] visualGameObjectArray;
    // Start is called before the first frame update

    private void Start() {
        Player.Instance.OnSelectedCounterChanged += Player_OnSelectedCounterChanged;
    }

    private void Player_OnSelectedCounterChanged(object sender,
                                                 Player.OnSelectedCounterChangedEventArgs e) {
        if (e.selectedCounter == baseCounter) {
            show();
        } else {
            hide();
        }
    }

    // Update is called once per frame
    void Update() {}
    private void show() { 
        foreach (GameObject visualGameObject in visualGameObjectArray)
            visualGameObject.SetActive(true); 
    }

    private void hide() { 
        foreach (GameObject visualGameObject in visualGameObjectArray)
            visualGameObject.SetActive(false); 
    }
}
