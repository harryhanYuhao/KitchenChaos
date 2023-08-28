using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseUI : MonoBehaviour
{
    [SerializeField] private Button resumeButton;
    [SerializeField] private Button mainMenuButton;

    private void Awake()
    {
        resumeButton.onClick.AddListener(() =>
        {
            GameSceneHandler.Instance.TogglePauseGame();
        });
        mainMenuButton.onClick.AddListener(() =>
        {
            Loader.LoadScene(Loader.Scene.MainManuScene);
        });
    }

    void Start()
    {
        GameSceneHandler.Instance.onGamePaused += GameHandler_onGamePaused;
        GameSceneHandler.Instance.onGameUnpaused += GameHandler_onGameUnpaused;
        Hide();   
    }

    private void Show()
    {
        gameObject.SetActive(true);
    }
    
    private void Hide()
    {
        gameObject.SetActive(false);
    }
    
    private void GameHandler_onGamePaused(object sender, EventArgs e)
    {
        Show();
    }
    
    private void GameHandler_onGameUnpaused(object sender, EventArgs e)
    {
        Hide();
    }
}
