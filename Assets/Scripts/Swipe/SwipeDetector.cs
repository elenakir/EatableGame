using UnityEngine;
using UnityEngine.EventSystems;

public class SwipeDetector : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [SerializeField] private bool _detectSwipeOnlyAfterRelease = false;
    [SerializeField] private float _minDistanceForSwipe = 20f;

    private Vector2 _downPosition;
    private Vector2 _upPosition;
    private bool _stopped;

    private void Start()
    {
        EventBus.onEnd += StopSwipe;
        EventBus.onRestart += RestartSwipe;
    }

    private void OnDestroy()
    {
        EventBus.onEnd -= StopSwipe;
        EventBus.onRestart -= RestartSwipe;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        _upPosition = eventData.pointerCurrentRaycast.screenPosition;
        _downPosition = eventData.pointerCurrentRaycast.screenPosition;
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (!_detectSwipeOnlyAfterRelease)
        {
            _downPosition = eventData.position;
            DetectSwipe();
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        _downPosition = eventData.position;
        DetectSwipe();
    }

    private void DetectSwipe()
    {
        if (SwipeDistanceCheckMet())
        {
            if (IsVerticalSwipe())
            {
                var direction = _downPosition.y - _upPosition.y > 0 ? SwipeDirection.Up : SwipeDirection.Down;
                SendSwipe(direction);
            }
            else
            {
                var direction = _downPosition.x - _upPosition.x > 0 ? SwipeDirection.Right : SwipeDirection.Left;
                SendSwipe(direction);
            }
            _upPosition = _downPosition;
        }
    }

    private bool IsVerticalSwipe()
    {
        return VerticalMovementDistance() > HorizontalMovementDistance();
    }

    private bool SwipeDistanceCheckMet()
    {
        return VerticalMovementDistance() > _minDistanceForSwipe || HorizontalMovementDistance() > _minDistanceForSwipe;
    }

    private float VerticalMovementDistance()
    {
        return Mathf.Abs(_downPosition.y - _upPosition.y);
    }

    private float HorizontalMovementDistance()
    {
        return Mathf.Abs(_downPosition.x - _upPosition.x);
    }

    private void SendSwipe(SwipeDirection direction)
    {
        SwipeData swipeData = new SwipeData()
        {
            Direction = direction,
            StartPosition = _downPosition,
            EndPosition = _upPosition
        };

        if (!_stopped)
        {
            EventBus.Swipe(swipeData);
        }
    }

    private void StopSwipe()
    {
        _stopped = true;
    }

    private void RestartSwipe()
    {
        _stopped = false;
    }
}