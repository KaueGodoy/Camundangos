using UnityEngine;

public static class AnimationHandler
{
    private static Animator animator;
    private static string currentAnimation;

    public static void Initialize(Animator targetAnimator)
    {
        animator = targetAnimator;
    }

    public static void ChangeAnimationState(string newAnimation)
    {
        if (currentAnimation == newAnimation) return;

        animator.Play(newAnimation);
        currentAnimation = newAnimation;
    }

    public static void PlayAnimation(string animationState)
    {
        ChangeAnimationState(animationState);
    }
}
