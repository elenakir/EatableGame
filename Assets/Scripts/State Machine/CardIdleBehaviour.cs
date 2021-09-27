using UnityEngine;

public class CardIdleBehaviour : StateMachineBehaviour
{
    private SwipeData swipeData;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        EventBus.onSwipe += Test; 
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (swipeData.Direction == SwipeDirection.Right)
        {
            animator.SetFloat("direction", 1);
        }
        else if (swipeData.Direction == SwipeDirection.Left)
        {
            animator.SetFloat("direction", -1);
        }
        else
        {
            animator.SetFloat("direction", 0);
        }
    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        EventBus.onSwipe -= Test;
    }

    private void Test(SwipeData data)
    {
        swipeData = data;
    }
}
