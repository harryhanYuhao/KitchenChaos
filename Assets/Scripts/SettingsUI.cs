using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SettingsUI : UI
{
    [SerializeField] private Button MusicVolumeButton;
    [SerializeField] private Button SoundEffectVolumnButton;

    private void Start()
    {
        SoundEffectVolumnButton.onClick.AddListener(() =>
        {   AdjustSoundEffectVolume();
            UpdateVisual();
        });
        
        PauseUI.Instance.onSettingButtonClicked += PauseUI_onSettingButtonClicked;
        Hide();
    }

    
    private void AdjustSoundEffectVolume()
    {
        SoundManager.Instance.LoopSoundEffectVolume();
    }

    private void UpdateVisual()
    {
        float soundEffectVolume = SoundManager.Instance.GetSoundEffectVolumeMultiplier();
        TextMeshProUGUI tmp = SoundEffectVolumnButton.GetComponentInChildren<TextMeshProUGUI>();
        tmp.text = "Sound Effect Volume: " + (soundEffectVolume*10f).ToString("0");
    }
    
    private void PauseUI_onSettingButtonClicked(object sender, System.EventArgs e)
    {
        Show();
        UpdateVisual();
    }
}
