using UnityEngine;

public abstract class AnimationController : MonoBehaviour, IAnimationController
{
    public string CurrentAnimation { get; set; }
    public Animator Animator { get; set; }
    public bool IdleState { get; set; }
    public bool WalkingState { get; set; }

    private void Awake()
    {
        Animator = GetComponent<Animator>();
    }

    public void ChangeAnimationState(string newAnimation)
    {
        if (CurrentAnimation == newAnimation) return;

        Animator.Play(newAnimation);
        CurrentAnimation = newAnimation;
    }

}
