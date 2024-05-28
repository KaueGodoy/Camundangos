using UnityEngine;

public class SkeletonV3AnimationController : MonoBehaviour
{
    [SerializeField] private SkeletonV3_Attack _skeletonV3Attack;
    [SerializeField] private SkeletonV3 _baseSkeleton;

    private Animator _animator;

    private string _attackAnimation = "Skeleton_V3_Attack";

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    private void Start()
    {
        _skeletonV3Attack.OnSkeletonAttacAnimation += _skeletonV3_OnSkeletonAttacAnimation;
    }

    private void Update()
    {
        UpdateAnimationState();
    }

    private void _skeletonV3_OnSkeletonAttacAnimation(object sender, System.EventArgs e)
    {
        //_animator.Play(_attackAnimation);
    }

    private void OnDestroy()
    {
        _skeletonV3Attack.OnSkeletonAttacAnimation -= _skeletonV3_OnSkeletonAttacAnimation;
    }

    private string currentAnimation;

    private const string SkeletonIdle = "Skeleton_V3_Idle";
    private const string SkeletonWalk = "Skeleton_V3_Walk";
    private const string SkeletonAttack = "Skeleton_V3_Attack";
    private const string SkeletonHit = "Skeleton_V3_Hit";
    private const string SkeletonDeath = "Skeleton_V3_Death";

    public void ChangeAnimationState(string newAnimation)
    {
        if (currentAnimation == newAnimation) return;

        _animator.Play(newAnimation);
        currentAnimation = newAnimation;
    }

    private void UpdateAnimationState()
    {
        if (_baseSkeleton.isDead())
        {
            ChangeAnimationState(SkeletonDeath);
        }
        else if (_baseSkeleton.IsHit)
        {
            ChangeAnimationState(SkeletonHit);
        }
        else if (_skeletonV3Attack.CanAttack)
        {
            ChangeAnimationState(SkeletonAttack);
        }
        else if (_baseSkeleton.RigidBody.velocity.x > 0 || _baseSkeleton.RigidBody.velocity.x < 0)
        {
            ChangeAnimationState(SkeletonWalk);
        }
        else
        {
            ChangeAnimationState(SkeletonIdle);
        }
    }
}
