using UnityEngine;

public class UIEffects : MonoBehaviour
{
    [SerializeField] private Transform _effectsParent;
    [SerializeField] private GameObject _addPointEffect;
    [SerializeField] private GameObject _removePointEffect;

    private void Start()
    {
        EventBus.onRightAnswer += AddPoint;
        EventBus.onWrongAnswer += RemovePoint;
    }

    private void OnDestroy()
    {
        EventBus.onRightAnswer -= AddPoint;
        EventBus.onWrongAnswer -= RemovePoint;
    }

    private void AddPoint()
    {
        var effect = Instantiate(_addPointEffect, _effectsParent);
        Destroy(effect, 2f);
    }

    private void RemovePoint()
    {
        var effect = Instantiate(_removePointEffect, _effectsParent);
        Destroy(effect, 2f);
    }
}
