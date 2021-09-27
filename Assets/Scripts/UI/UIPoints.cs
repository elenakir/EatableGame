using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIPoints : MonoBehaviour
{
    [SerializeField] private GameData _gameData;
    [SerializeField] private TextMeshProUGUI _pointsCountText;
    [SerializeField] private TextMeshProUGUI _pointsFailPanelText;
    [SerializeField] private GameObject _endPanel;
    [SerializeField] private Button _buttonRestart;

    private int _points;

    private void Start()
    {
        _points = 0;
        _pointsCountText.text = _points.ToString();

        EventBus.onRightAnswer += IncreasePoint;
        EventBus.onEnd += ShowEndPanel;
        EventBus.onRestart += Reset;

        _buttonRestart.onClick.AddListener(() => 
        {
            EventBus.RestartGame();
            _endPanel.SetActive(false);
        });
    }

    private void OnDestroy()
    {
        EventBus.onRightAnswer -= IncreasePoint;
        EventBus.onEnd -= ShowEndPanel;
        EventBus.onRestart -= Reset;
    }

    private void IncreasePoint()
    {
        _points++;
        _pointsCountText.text = _points.ToString();

        if (_gameData.maxCombo < _points)
        {
            _gameData.maxCombo = _points;
        }
    }

    private void ShowEndPanel()
    {
        _endPanel.SetActive(true);
        _pointsFailPanelText.text = _pointsCountText.text;
    }

    private void Reset()
    {
        _points = 0;
        _pointsCountText.text = _points.ToString();
    }
}
