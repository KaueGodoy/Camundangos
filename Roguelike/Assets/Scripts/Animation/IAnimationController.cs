using UnityEngine;

public interface IAnimationController
{
    public Animator Animator { get; set; }
    public string CurrentAnimation { get; set; }
    public void ChangeAnimationState(string newAnimation);
}
