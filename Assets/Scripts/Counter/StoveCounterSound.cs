using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoveCounterSound : MonoBehaviour
{
    private AudioSource audioSource;
    [SerializeField] private StoveCounter stoveCounter;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        stoveCounter.FryingStarted += Sizzling_AudioStart;
        stoveCounter.FryingStopped += Sizzling_AudioEnd;
    }
    
    private void Sizzling_AudioStart(object sender, System.EventArgs e)
    {
        audioSource.Play();
    }
    
    private void Sizzling_AudioEnd(object sender, System.EventArgs e)
    {
        audioSource.Stop();
    }
}
