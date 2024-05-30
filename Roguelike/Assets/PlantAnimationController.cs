using UnityEngine;

public class PlantAnimationController : MonoBehaviour
{
    [SerializeField] private Plant_Attack _plant;
    [SerializeField] private Plant _plantEnemy;

    private Animator _animator;

    private string _attackAnimation = "Plant_Attack";
    private string _deathAnimation = "Plant_Death";

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    private void Start()
    {
        _plant.OnPlantAttackAnimation += _plant_OnAttackAnimation;
    }

    private void Update()
    {
        UpdateAnimationState(); 
    }

    private void _plant_OnAttackAnimation(object sender, System.EventArgs e)
    {
        //_animator.Play(_attackAnimation);
    }

    private void OnDestroy()
    {
        _plant.OnPlantAttackAnimation -= _plant_OnAttackAnimation;
    }

    public void EnableAttack()
    {
        _plant.AttackPlayer();
    }

    private string currentAnimation;

    private const string PlantIdle = "Plant_Idle";
    private const string PlantAttack = "Plant_Attack";
    private const string PlantHit = "Plant_Hit";
    private const string PlantDeath = "Plant_Death";

    public void ChangeAnimationState(string newAnimation)
    {
        if (currentAnimation == newAnimation) return;

        _animator.Play(newAnimation);
        currentAnimation = newAnimation;
    }


    private void UpdateAnimationState()
    {
        if (_plantEnemy.isDead())
        {
            ChangeAnimationState(PlantDeath);
        }
        else if (_plantEnemy.IsHit)
        {
            ChangeAnimationState(PlantHit);
        }
        else if (_plant.CanAttack)
        {
            ChangeAnimationState(PlantAttack);
        }
        else
        {
            ChangeAnimationState(PlantIdle);
        }
    }
}
