using ServiceLocatorAsteroid.Service;
using UnityEngine;

public interface IUIManager : IGameService
{
    public UIGame GetGameUI();

    public void CreateGameUI();
}