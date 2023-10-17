
using UnityEngine;

public interface IAnimationController
{
    public void ChangeAnimationState(string newAnimation);
    public string CurrentAnimation { get; set; }
    public Animator Animator { get; set; }
    public bool IdleState { get; set; }
    public bool WalkingState { get; set; }
}
