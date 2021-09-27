using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class GameSystem : MonoBehaviour
{
    [SerializeField] private List<CardBundleData> _bundles;
    [SerializeField] private CardFactory _factory;
    [SerializeField] private Transform _cardParent;

    private List<CardData> _allCards;
    private bool _stopped;
    private int _taskIndex;

    private void Start()
    {
        _allCards = new List<CardData>();

        foreach (var bundle in _bundles)
        {
            foreach (var item in bundle.CardData)
            {
                _allCards.Add(item);
            }
        }

        EventBus.onRestart += Restart;
        EventBus.onSwipe += NextTask;
        EventBus.onEnd += StopGame;
        EventBus.onTimerOut += AutoNext;

        Restart();
    }

    private void OnDestroy()
    {
        EventBus.onRestart -= Restart;
        EventBus.onSwipe -= NextTask;
        EventBus.onEnd -= StopGame;
        EventBus.onTimerOut -= AutoNext;
    }

    private void NextTask(SwipeData data)
    {
        if ((data.Direction == SwipeDirection.Right && _allCards[_taskIndex].IsEatable)
            || (data.Direction == SwipeDirection.Left && !_allCards[_taskIndex].IsEatable))
        {
            EventBus.RightAnswer();
        }
        else EventBus.WrongAnswer();

        _taskIndex++;
        SetCurrentCard();
    }

    private void AutoNext()
    {
        _taskIndex++;
        SetCurrentCard();
    }

    private async void SetCurrentCard()
    {
        Card prev = new Card();

        if (!_stopped)
        {
            if (_taskIndex < _allCards.Count)
            {
                Card card = _factory.CreateCard(_cardParent);

                var handle = _allCards[_taskIndex].Icon.LoadAssetAsync<Sprite>();
                await handle.Task;
                if (handle.Status == AsyncOperationStatus.Succeeded)
                {
                    Sprite sprite = handle.Result;
                    card.Icon.sprite = sprite;
                    Addressables.Release(handle);
                }
                card.Name = _allCards[_taskIndex].Name;
            }
            else EventBus.EndGame();
        }
    }

    private void StopGame()
    {
        _stopped = true;
    }

    private void Restart()
    {
        _stopped = false;
        _taskIndex = 0;
        _allCards = _allCards.OrderBy(x => Random.value).ToList();
        SetCurrentCard();
    }
}
