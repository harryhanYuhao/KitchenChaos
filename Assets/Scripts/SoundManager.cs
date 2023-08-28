// this is the script controlling various sound effects.
// the sound effect for stove sizzling is controled by a separate file
// the background music is controled by the music manage (with no script)
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    private void Start()
    {
        TrashCounter.onTrash += TrashCounter_onTrash;
        CuttingCounter.OnAnyCut += CuttingCounter_OnAnyCut;
        Player.OnPickSomething += Play_ObjectPickup;
        Player.OnDropSomething += Play_ObjectDrop;
        PlateObject.OnAddedToPlate += Play_ObjectPickup;
        PlateObject.OnPoppedFromPlate += Play_ObjectDrop;
    }
    public void PlaySound(AudioClip clip, Vector3 position, float volume = 1f)
    {
        AudioSource.PlayClipAtPoint(clip, position, volume);
    }

    public void PlaySound(AudioClip[] array, Vector3 position, float volume = 1f)
    {
        PlaySound(array[Random.Range(0, array.Length)], position, volume);
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
}
