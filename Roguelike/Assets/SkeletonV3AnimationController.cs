using UnityEngine;

public class SkeletonV3AnimationController : MonoBehaviour
{
    [SerializeField] private SkeletonV3_Attack _skeletonV3;

    private Animator _animator;

    private string _attackAnimation = "Skeleton_V3_Attack";

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    private void Start()
    {
        _skeletonV3.OnSkeletonAttacAnimation += _skeletonV3_OnSkeletonAttacAnimation;
    }

    private void _skeletonV3_OnSkeletonAttacAnimation(object sender, System.EventArgs e)
    {
        _animator.Play(_attackAnimation);
    }

    private void OnDestroy()
    {
        _skeletonV3.OnSkeletonAttacAnimation -= _skeletonV3_OnSkeletonAttacAnimation;
    }
}
