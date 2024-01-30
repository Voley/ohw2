using System.Collections.Generic;
using UnityEngine;

namespace ShootEmUp
{
    public enum GameState
    {
        None,
        Starting,
        Playing,
        Paused
    }

    public sealed class GameManager : MonoBehaviour
    {
        [SerializeField] private GameListenerInstaller _installer;

        public GameState GameState { get; private set; }
        
        private List<IGameListener> _listeners = new();
        private List<IGameUpdateListener> _updateListeners = new();
        private List<IGameFixedUpdateListener> _fixedUpdatesListeners = new();

        private void Start()
        {
            _installer.InstallListeners(this);
            
            GameState = GameState.None;

            PrepareGame();
        }

        public void LaunchGame()
        {
            StartGame();
        }

        public void TogglePause()
        {
            if (GameState == GameState.Paused) { 
                ResumeGame();
            } else {
                PauseGame();
            }
        }

        public void AddListener(IGameListener listener)
        {
            _listeners.Add(listener);

            if (listener is IGameUpdateListener updater) {
                _updateListeners.Add(updater);
            }

            if (listener is IGameFixedUpdateListener fixedUpdater) {
                _fixedUpdatesListeners.Add(fixedUpdater);
            }
        }

        public void RemoveListener(IGameListener listener)
        {
            _listeners.Remove(listener);
        }

        public void FinishGame()
        {
            Debug.Log("Game over!");
            Time.timeScale = 0;
        }

        private void PrepareGame()
        {
            // notify listeners

            for (int i = 0; i < _listeners.Count; i++) {
                if (_listeners[i] is IGamePrepareListener listener) {
                    listener.PrepareGame();
                }
            }

            // logic here

            GameState = GameState.Starting;
        }

        private void StartGame()
        {
            // notify listeners

            for (int i = 0; i < _listeners.Count; i++) {
                if (_listeners[i] is IGameStartListener listener) {
                    listener.StartGame();
                } 
            }

            // logic here
            GameState = GameState.Playing;
        }

        private void PauseGame()
        {
            // notify listeners

            for (int i = 0; i < _listeners.Count; i++) {
                if (_listeners[i] is IGamePauseListener listener) {
                    listener.PauseGame();
                }
            }

            // logic here
            GameState = GameState.Paused;

            Time.timeScale = 0;
        }

        private void ResumeGame()
        {
            // notify listeners

            for (int i = 0; i < _listeners.Count; i++) {
                if (_listeners[i] is IGameResumeListener listener) {
                    listener.ResumeGame();
                }
            }

            // logic here
            GameState = GameState.Playing;
            Time.timeScale = 1;
        }

        private void UpdateGame()
        {
            // notify listeners

            foreach (var listener in _updateListeners) {
                listener.UpdateGame();
            }

            // logic here
        }

        private void FixedUpdateGame()
        {
            // notify listeners

            foreach (var listener in _fixedUpdatesListeners) {
                listener.FixedUpdateGame();
            }

            // logic here
        }

        private void Update()
        {
            if (GameState == GameState.Playing) {
                UpdateGame();
            }
        }

        private void FixedUpdate()
        {
            if (GameState == GameState.Playing) {
                FixedUpdateGame();
            }
        }
    }
}