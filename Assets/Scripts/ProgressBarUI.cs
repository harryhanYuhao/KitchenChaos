// This script shall be attached to the CuttingCounter_Visual, which is the child of Cutting Counter

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class ProgressBarUI : MonoBehaviour {
    [SerializeField] private Image barImage;
    // shall be the parent cutting counter
    [SerializeField] private GameObject hasProgressGameObject;

    private IHasProgress iHasProgress;
    private void Start() {
        iHasProgress = hasProgressGameObject.GetComponent<IHasProgress>();
        if (iHasProgress == null) Debug.LogError("IHasProgress not found");
        iHasProgress.OnProgressChanged += IHasProgress_OnProgressChanged;
        barImage.fillAmount = 0f;
        Hide();
    }

    private void IHasProgress_OnProgressChanged(object sender,
                                                  IHasProgress.OnProgressChangedEventArgs e) {
        barImage.fillAmount = e.progressNormalized;
        if (e.progressNormalized == 0f || e.progressNormalized == 1f) {
            Hide();
        } else {
            Show();
        }
    }

    private void Show() { gameObject.SetActive(true); }

    private void Hide() { gameObject.SetActive(false); }
}
