using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class BackgroundMusic : MonoBehaviour
{
    public static BackgroundMusic Instance {get ; private set;}
    private AudioSource audioSource;
    private float volume;

    private void Awake()
    {
        Instance = this;
        audioSource = this.GetComponent<AudioSource>();
    }

    private void Start()
    {
        volume = audioSource.volume;
        SettingsUI.Instance.onMusicVolumeBottonClicked += SettingsUI_onMusicVolumeBottonClicked;
    }
    
    public void LoopBackgroundMusiclume()
    {
        volume += 0.1f;
        if (volume > 1.09f)  // compensating floating point inaccuracy
        {
            volume = 0f;
        }
    }
    
    private void SettingsUI_onMusicVolumeBottonClicked(object sender, System.EventArgs e)
    {
        LoopBackgroundMusiclume();
        audioSource.volume = volume;
    }

    public float GetVolume()
    {
        return volume;
    }
}
