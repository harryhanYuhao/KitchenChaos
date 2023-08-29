using UnityEngine;
using UnityEngine.UI;

public class TimerUI : MonoBehaviour
{
    [SerializeField] private Image TimerImage;

    private void Start()
    {
        GameSceneHandler.Instance.gameStarted += TimerUI_GameStarted;
        GameSceneHandler.Instance.gameOver += TimerUI_GameOver;
        Hide();    
    }

    private void Update()
    {
        TimerImage.fillAmount = 1 - GameSceneHandler.Instance.GetGameProgressNormalized();
    }
    
    private void TimerUI_GameStarted(object sender, System.EventArgs e)
    {
        Show();
    }
    
    private void TimerUI_GameOver(object sender, System.EventArgs e)
    {
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
}
