using UnityEngine;

public class HandleAnimationStates : AnimationController
{
    [Header("Animations")]
    [SerializeField] private string _idle;
    [SerializeField] private string _walk;

    private void Start()
    {
        //IdleState = true;
        WalkingState = true;
    }

    private void Update()
    {
        UpdateTimer();
        UpdateAnimationState();
    }

    private void UpdateAnimationState()
    {
        if (WalkingState)
        {
            ChangeAnimationState(_walk);
        }
        else if (IdleState)
        {
            ChangeAnimationState(_idle);
        }
    }

    private void UpdateTimer()
    {

    }
}

public enum AnimationState
{
    Idle, Walking
}
