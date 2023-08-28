using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CountDownUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI countDownText;

    private int countDownTime = 3;
    
    private IEnumerator StartCountDown()
    {
        while (countDownTime > 0)
        {
            countDownText.text = countDownTime.ToString();
            countDownTime--;
            yield return new WaitForSeconds(1);
        }
    }

    private IEnumerator ShowStartThenHide()
    {
        countDownText.text = "START!";
        yield return new WaitForSeconds(1f);
        Hide();
    }
    private void Start()
    {
        GameSceneHandler.Instance.countDownStarted += GameHandler_CountDownStarted;
        GameSceneHandler.Instance.gameStarted += GameHandler_GameStarted;
        Hide();
    }
    
    private void GameHandler_CountDownStarted(object sender, System.EventArgs e)
    {
        Show();
        StartCoroutine(StartCountDown());
        
    }
    
    private void GameHandler_GameStarted(object sender, System.EventArgs e)
    {
        StartCoroutine(ShowStartThenHide());
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
