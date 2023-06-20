using ServiceLocatorAsteroid.Service;
using UnityEngine;

public interface IUiService : IGameService
{
    public UIGame GetGameUI();

    public void CreateGameUI();

    public void RemoveCurrentUI();
    public void CreateMenuUI();
}