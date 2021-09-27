public static class EventBus
{
    public delegate void OnSwipe(SwipeData data);
    public static OnSwipe onSwipe;

    public delegate void OnRightAnswer();
    public static OnRightAnswer onRightAnswer;

    public delegate void OnWrongAnswer();
    public static OnWrongAnswer onWrongAnswer;

    public delegate void OnEnd();
    public static OnEnd onEnd;

    public delegate void OnRestart();
    public static OnRestart onRestart;

    public delegate void OnTimerOut();
    public static OnTimerOut onTimerOut;

    public static void Swipe(SwipeData data)
    {
        onSwipe?.Invoke(data);
    }

    public static void RightAnswer()
    {
        onRightAnswer?.Invoke();
    }

    public static void WrongAnswer()
    {
        onWrongAnswer?.Invoke();
    }

    public static void EndGame()
    {
        onEnd?.Invoke();
    }

    public static void RestartGame()
    {
        onRestart?.Invoke();
    }

    public static void TimerOut()
    {
        onTimerOut?.Invoke();
    }
}
