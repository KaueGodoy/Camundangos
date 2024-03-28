using UnityEngine;

public abstract class AnimationController : MonoBehaviour, IAnimationController
{
    public string CurrentAnimation { get; set; }
    public Animator Animator { get; set; }

    public virtual void Awake()
    {
        Animator = GetComponent<Animator>();
    }

    public void ChangeAnimationState(string newAnimation)
    {
        if (newAnimation == null) return;

        //if (CurrentAnimation == newAnimation) return;

        Animator.Play(newAnimation);
        CurrentAnimation = newAnimation;
    }

}
