using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSound : MonoBehaviour
{
    private float footStepTimer;
    private float footStepTimeMax;
    // this field shall be set to the gameSoundManager in root tree
    // [SerializeField] private SoundManager soundManager;
    
    private void Start()
    {
        footStepTimeMax = 0.2f;
        footStepTimer = 0;
    }
    
    private void Update()
    {
        if (Player.Instance.IsWalking())
        {
            footStepTimer += Time.deltaTime;
            if (footStepTimer >= footStepTimeMax)
            {
                footStepTimer += footStepTimeMax;
                SoundManager.Instance.PlayPlayerFootStep(this.transform.position);
                footStepTimer = 0;
            }
        }
    }
}
