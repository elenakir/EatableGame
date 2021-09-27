using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIHealth : MonoBehaviour
{
    [SerializeField] private GameData _gameData;
    [SerializeField] private GameObject _prefab;
    [SerializeField] private Transform _parent;

    private List<GameObject> _healthList;
    private int counter;

    private void Start()
    {
        counter = _gameData.healthCount - 1;
        _healthList = new List<GameObject>();

        for (int i = 0; i < _gameData.healthCount; i++)
        {
            _healthList.Add(Instantiate(_prefab, _parent));
        }

        EventBus.onWrongAnswer += LoseHealth;
        EventBus.onRestart += RestoreHealth;
    }

    private void OnDestroy()
    {
        EventBus.onWrongAnswer -= LoseHealth;
        EventBus.onRestart -= RestoreHealth;
    }

    private void LoseHealth()
    {
        if (counter >= 0)
        {
            ChangeAlpha(_healthList[counter].GetComponent<Image>(), 0.3f);
            counter--;
            if (counter < 0)
            {
                EventBus.EndGame();
            }
        }
    }

    private void RestoreHealth()
    {
        counter = _gameData.healthCount - 1;
        foreach (var item in _healthList)
        {
            ChangeAlpha(item.GetComponent<Image>(), 1);
        }
    }

    private void ChangeAlpha(Image image, float alpha)
    {
        var tempColor = image.color;
        tempColor.a = alpha;
        image.color = tempColor;
    }
}
