using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSceneHandler : MonoBehaviour
{
    public static GameSceneHandler Instance { get; private set; }

    public event EventHandler countDownStarted;
    public event EventHandler gameStarted;
    public event EventHandler gameOver;
    public event EventHandler onGamePaused;
    public event EventHandler onGameUnpaused;
    
    private enum State
    {
        WaitingToStart,
        CountDown,
        GamePlaying,
        GameOver,
    }

    public int difficulty = 0;

    private State state;

    private float timer;

    private readonly float waitingToStartTime = 0.2f;
    private const int countDownTime = 3;
    private const int gameTime = 10;
    private bool gamePaused = false;
    
    // Start is called before the first frame update
    void Awake()
    {
        Instance = this;
        StartGameSession();
    }

    // Start called after Awake
    private void Start()
    {
        GameInput.Instance.OnPauseAction += GameInput_OnPauseAction;
    }

    // Update is called once per frame
    void Update()
    {
        switch (state)
        {
            case State.WaitingToStart:
                timer += Time.deltaTime;
                if (timer >= waitingToStartTime)
                {
                    state = State.CountDown;
                    countDownStarted?.Invoke(this, EventArgs.Empty);
                    timer = 0;
                }
                break;
            case State.CountDown:
                timer += Time.deltaTime;
                if (timer >= countDownTime)
                {
                    state = State.GamePlaying;
                    gameStarted?.Invoke(this, EventArgs.Empty);
                    timer = 0;
                }
                break;
            case State.GamePlaying:
                timer += Time.deltaTime;
                if (timer >= gameTime)
                {
                    state = State.GameOver;
                    gameOver?.Invoke(this, EventArgs.Empty);
                    timer = 0;
                }
                break;
        }    
    }

    public bool IsPlaying()
    {
        return state == State.GamePlaying;
    }

    // return -1 if the game is not playing
    public float GetGameProgressNormalized()
    {
        if (state == State.GamePlaying)
        {
            return timer / gameTime;
        }
        else
        {
            return -1;
        }
    }
    public void StartGameSession()
    {
        state = State.WaitingToStart;
        timer = 0;
    }
    
    private void GameInput_OnPauseAction(object sender, EventArgs e)
    {
        TogglePauseGame();
    }

    public void TogglePauseGame()
    {
        gamePaused = !gamePaused;
        if (gamePaused) {
            Time.timeScale = 0f;  // pause all 
            onGamePaused!.Invoke(this, EventArgs.Empty);
        }
        else
        {
            Time.timeScale = 1f;
            onGameUnpaused!.Invoke(this, EventArgs.Empty);
        }
    }
}
