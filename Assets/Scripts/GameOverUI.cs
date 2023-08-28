using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameOverUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI RecipesReceivedNumber;
    [SerializeField] private Button PlayAgainButton;

    private void Start()
    {
        PlayAgainButton.onClick.AddListener(() =>
        {
            Loader.LoadScene(Loader.Scene.GameScene);
        });
        GameSceneHandler.Instance.gameOver += GameHandler_GameOver;
        Hide();
    }
    
    private void GameHandler_GameOver(object sender, System.EventArgs e)
    {
        // TODO: set the number of recipes received successfully
        Show();
    }

    private void Show()
    {
        gameObject.SetActive(true);
    }
    
    private void Hide()
    {
        gameObject.SetActive(false);
    }
}
