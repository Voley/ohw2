using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IGameListener { }

public interface IGamePrepareListener : IGameListener {
    public void PrepareGame();
}

public interface IGameStartListener : IGameListener {
    public void StartGame();
}

public interface IGameFinishListener : IGameListener {
    public void FinishGame();
}

public interface IGamePauseListener : IGameListener {
    public void PauseGame();
}

public interface IGameResumeListener : IGameListener {
    public void ResumeGame();
}

public interface IGameUpdateListener: IGameListener {
    public void UpdateGame();
}

public interface IGameFixedUpdateListener: IGameListener {
    public void FixedUpdateGame();
}
