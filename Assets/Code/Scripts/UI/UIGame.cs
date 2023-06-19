using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIGame : MonoBehaviour
{
    [SerializeField] private GameObject _life;

    [SerializeField] private TextMeshProUGUI _scoreText;
    [SerializeField] private Transform _livesContainer;

    private List<GameObject> _livesList = new List<GameObject>();
    private RectTransform _rectTransform;

    private void Start()
    {
        Canvas canvas = FindObjectOfType<Canvas>();
        if (canvas)
        {
            transform.SetParent(canvas.transform, false);
            _rectTransform = GetComponent<RectTransform>();
            _rectTransform.anchoredPosition = Vector3.zero;
            _rectTransform.localScale = Vector3.one;
        }
    }

    public void Initialize(int maxLives)
    {
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
}
