using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameView : View
{
    [SerializeField] private GameObject _life;
    [SerializeField] private TextMeshProUGUI _scoreText;
    [SerializeField] private Transform _livesContainer;
    [SerializeField] private GameObject _playerInfo;
    [SerializeField] private GameObject _loseScreen;
    [SerializeField] private TextMeshProUGUI _scoreLoseScreen;
    [SerializeField] private GameObject _youWin;

    private List<GameObject> _livesList = new List<GameObject>();

    public void Initialize(int maxLives)
    {
        _loseScreen.SetActive(false);
        _playerInfo.SetActive(true);
        for (int i = 0; i < maxLives; i++)
        {
            GameObject life = Instantiate(_life, _livesContainer);
            _livesList.Add(life);
        }
    }

    public void UpdateUI(int currentLives, int currentScore)
    {
        for (int i = 0; i < _livesList.Count; i++)
        {
            bool isLive = currentLives > i;
            _livesList[i].SetActive(isLive);
        }
        _scoreText.text = currentScore.ToString();
    }

    public void ShowFinalScoreScreen(int currentScore, bool win)
    {
        _loseScreen.SetActive(true);
        _playerInfo.SetActive(false);
        _youWin.SetActive(win);
        _scoreLoseScreen.text = currentScore.ToString();
    }
}
