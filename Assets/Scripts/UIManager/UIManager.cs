using ShootEmUp;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour, IGamePrepareListener, IGameStartListener, IGamePauseListener, IGameResumeListener
{
    [SerializeField] private GameManager _gameManager;
    [SerializeField] Button _startGameButton;
    [SerializeField] Button _pauseButton;
    [SerializeField] TMP_Text _pauseButtonText;
    [SerializeField] private Countdown _countdown;

    public void PrepareGame()
    {
        _startGameButton.onClick.AddListener(StartGameButtonPressed);
        _startGameButton.gameObject.SetActive(true);
        _countdown.OnCountdownEnded += StartGameplay;
        _pauseButton.gameObject.SetActive(false);
        _pauseButton.onClick.AddListener(PauseButtonPressed);
    }

    public void StartGame()
    {
        _startGameButton.gameObject.SetActive(false);
        _pauseButton.gameObject.SetActive(true);
    }

    public void StartGameButtonPressed()
    {
        _countdown.StartCountdown();
        _startGameButton.interactable = false;
    }

    public void PauseButtonPressed()
    {
        _gameManager.TogglePause();
    }

    public void PauseGame()
    {
        _pauseButtonText.text = "Resume";
    }

    public void ResumeGame()
    {
        _pauseButtonText.text = "Pause";
    }

    private void StartGameplay()
    {
        _gameManager.LaunchGame();
    }
}
