using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;

public class PauseUI : MonoBehaviour
{
    public static PauseUI Instance { get; private set; }
    [SerializeField] private Button resumeButton;
    [SerializeField] private Button mainMenuButton;
    [SerializeField] private Button settingButton;
    
    public event EventHandler onSettingButtonClicked;

    private void Awake()
    {
        Instance = this;
        resumeButton.onClick.AddListener(() =>
        {
            // there is only one gamescene handler, so we may use the singleton pattern
            GameSceneHandler.Instance.TogglePauseGame();
        });
        mainMenuButton.onClick.AddListener(() =>
        {
            Loader.LoadScene(Loader.Scene.MainManuScene);
        });
        settingButton.onClick.AddListener(() =>
        {
            onSettingButtonClicked?.Invoke(this, EventArgs.Empty);
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
