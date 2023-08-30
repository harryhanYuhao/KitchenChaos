using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SettingsUI : UI
{
    public static SettingsUI Instance { get; private set; }
    [SerializeField] private Button MusicVolumeButton;
    [SerializeField] private Button SoundEffectVolumnButton;

    public event EventHandler onSoundEffectVolumeBottonClicked;
    public event EventHandler onMusicVolumeBottonClicked;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        SoundEffectVolumnButton.onClick.AddListener(() =>
        {  
            onSoundEffectVolumeBottonClicked?.Invoke(this, EventArgs.Empty);
            UpdateVisual();
        });
        MusicVolumeButton.onClick.AddListener(() =>
        {
            onMusicVolumeBottonClicked?.Invoke(this, EventArgs.Empty);
            UpdateVisual();
        });
        
        PauseUI.Instance.onSettingButtonClicked += PauseUI_onSettingButtonClicked;
        Hide();
    }


    private void UpdateVisual()
    {
        float soundEffectVolume = SoundManager.Instance.GetSoundEffectVolumeMultiplier();
        TextMeshProUGUI tmp = SoundEffectVolumnButton.GetComponentInChildren<TextMeshProUGUI>();
        tmp.text = "Sound Effect Volume: " + (soundEffectVolume*10f).ToString("0");
        float BackGroundMusicVolume = BackgroundMusic.Instance.GetVolume();
        tmp = MusicVolumeButton.GetComponentInChildren<TextMeshProUGUI>();
        tmp.text = "Music Volume: " + (BackGroundMusicVolume*10f).ToString("0");
    }
    
    private void PauseUI_onSettingButtonClicked(object sender, System.EventArgs e)
    {
        Show();
        UpdateVisual();
    }
}
