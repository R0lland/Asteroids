using ServiceLocatorAsteroid.Service;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IStateService : IGameService
{
    public void ChangeState(StateService.GameState newGameState);

    public void Initialize();
}
