// this is the script controlling various sound effects.
// the sound effect for stove sizzling is controled by a separate file
// the background music is controled by the music manage (with no script)

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class SoundManager : MonoBehaviour
{
    [SerializeField] private AudioClip[] chop;
    [SerializeField] private AudioClip[] deliverFailed;
    [SerializeField] private AudioClip[] deliverSuccess;
    [SerializeField] private AudioClip[] footStep;
    [SerializeField] private AudioClip[] objectDrop;
    [SerializeField] private AudioClip[] objectPickup;
    [SerializeField] private AudioClip[] panSizzle;
    [SerializeField] private AudioClip[] trash;
    [SerializeField] private AudioClip[] warning;

    public static SoundManager Instance { get; private set; }
    
    private float soundEffectVolumeMultiplier = 1f;

    private void Awake()
    {
        Instance = this;   
    }

    private void Start()
    {
        TrashCounter.onTrash += TrashCounter_onTrash;
        CuttingCounter.OnAnyCut += CuttingCounter_OnAnyCut;
        Player.OnPickSomething += Play_ObjectPickup;
        Player.OnDropSomething += Play_ObjectDrop;
        PlateObject.OnAddedToPlate += Play_ObjectPickup;
        PlateObject.OnPoppedFromPlate += Play_ObjectDrop;
        SettingsUI.Instance.onSoundEffectVolumeBottonClicked += OnSettingUISoundEffectVolumeBottonClicked;
    }
    public void PlaySound(AudioClip clip, Vector3 position, float volume = 1f)
    {
        AudioSource.PlayClipAtPoint(clip, position, volume*soundEffectVolumeMultiplier);
    }

    public void PlaySound(AudioClip[] array, Vector3 position, float volume = 1f)
    {
        AudioSource.PlayClipAtPoint(array[Random.Range(0, array.Length)], position, volume*soundEffectVolumeMultiplier);
    } 
    
    private void TrashCounter_onTrash(object sender, System.EventArgs e)
    {
        var tmp = sender as TrashCounter;
        PlaySound(trash, Camera.main.transform.position, 0.1f);
    }
    
    private void CuttingCounter_OnAnyCut(object sender, System.EventArgs e)
    {
        var tmp = sender as CuttingCounter;
        PlaySound(chop, Camera.main.transform.position, 0.12f);
    }
    
    private void Play_ObjectPickup(object sender, System.EventArgs e)
    {
        var tmp = sender as Player;
        PlaySound(objectPickup, Camera.main.transform.position, 0.12f);
    }
    private void Play_ObjectDrop(object sender, System.EventArgs e)
    {
        var tmp = sender as Player;
        PlaySound(objectDrop, Camera.main.transform.position, 0.12f);
    }
    
    public void PlayPlayerFootStep(Vector3 position)
    {
        PlaySound(footStep, Camera.main.transform.position, 0.2f);
    }
    
    public float GetSoundEffectVolumeMultiplier()
    {
        return soundEffectVolumeMultiplier;
    }
    
    public void SetSoundEffectVolumeMultiplier(float value)
    {
        soundEffectVolumeMultiplier = value;
    }

    public void LoopSoundEffectVolume()
    {
        soundEffectVolumeMultiplier += 0.1f;
        if (soundEffectVolumeMultiplier > 1f)
        {
            soundEffectVolumeMultiplier = 0f;
        }
    }
    
    private void OnSettingUISoundEffectVolumeBottonClicked(object sender, EventArgs e)
    {
        LoopSoundEffectVolume();
    }
}
