using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class UITimer : MonoBehaviour
{
    [SerializeField] private GameData _gameData;
    [SerializeField] private Image _timerImage;
    [SerializeField] private Image _countdownImage;
    [SerializeField] private Sprite[] _numbers;

    private Animator _animator;
    private bool _stopped;

    private void Start()
    {
        _animator = _countdownImage.GetComponent<Animator>();

        StartCoroutine(Timer());
        EventBus.onTimerOut += RestartTimer;
        EventBus.onSwipe += RestartTimer;
        EventBus.onRestart += Unstopped;
        EventBus.onEnd += Stop;
    }

    private void OnDestroy()
    {
        EventBus.onTimerOut -= RestartTimer;
        EventBus.onSwipe -= RestartTimer;
        EventBus.onRestart -= Unstopped;
        EventBus.onEnd -= Stop;
    }

    private void RestartTimer()
    {
        if (!_stopped)
        {
            StopAllCoroutines();
            StartCoroutine(Timer());
        }
    }

    private void RestartTimer(SwipeData data)
    {
        if (!_stopped)
        {
            StopAllCoroutines();
            StartCoroutine(Timer()); 
        }
    }

    private void Unstopped()
    {
        _stopped = false;
        StartCoroutine(Timer());
    }

    private void Stop()
    {
        _stopped = true;
        StopAllCoroutines();
        _animator.speed = 0;
    }



    IEnumerator Timer()
    {
        _animator.speed = 1;
        _animator.Play("Timer", -1, 0f);
        StartCoroutine(CountdownImage());

        float time = 1;
        while (time >= 0f)
        {
            _timerImage.fillAmount = time;
            time -= Time.deltaTime / _gameData.secondsCount;
            yield return null;
        }
        EventBus.WrongAnswer();
        EventBus.TimerOut();
    }

    IEnumerator CountdownImage()
    {
        int count = _gameData.secondsCount;
        while (count > 0)
        {
            _countdownImage.sprite = _numbers[count-1];
            count--;
            yield return new WaitForSeconds(1);
        }
    }
}
