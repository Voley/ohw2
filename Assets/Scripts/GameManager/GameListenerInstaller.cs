using ShootEmUp;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameListenerInstaller : MonoBehaviour
{
    [SerializeField] MonoBehaviour[] _gameListeners;

    public void InstallListeners(GameManager manager)
    {
        foreach (MonoBehaviour item in _gameListeners) {
            if (item != null && item is IGameListener listener) {
                manager.AddListener(listener);
            }
        }
    }
}
