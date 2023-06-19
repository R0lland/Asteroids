using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : IUIManager
{
    [SerializeField] private UIGame _uiGamePrefab;

    public UIManager(UIGame _uiGame)
    {
        _uiGamePrefab = _uiGame;
    }  
}
