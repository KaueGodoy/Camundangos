using UnityEngine;

public class PlantAnimationController : MonoBehaviour
{
    [SerializeField] private Plant _plant;

    private Animator _animator;

    private string _attackAnimation = "Plant_Attack";

    private void Awake()
    {
        _animator = GetComponent<Animator>();   
    }

    private void Start()
    {
        _plant.OnPlantAttackAnimation += _plant_OnAttackAnimation;
    }

    private void _plant_OnAttackAnimation(object sender, System.EventArgs e)
    {
        _animator.Play(_attackAnimation);
    }

    private void OnDestroy()
    {
        _plant.OnPlantAttackAnimation -= _plant_OnAttackAnimation;
    }

    public void EnableAttack()
    {
        _plant.AttackPlayer();
    }
}
