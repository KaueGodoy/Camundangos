using UnityEngine;

public class PlayerBaseStats : MonoBehaviour
{
    public CharacterStats CharacterStats { get; set; }

    [Header("Base Stats")]
    public float baseHealth = 22000f;
    public float baseAttack = 10;
    public float baseAttackPercent = 0;
    public float baseAttackFlat = 0;
    public float baseDamageBonus = 0;
    public float baseCritRate = 5;
    public float baseCritDamage = 50;
    public float baseDefense = 15;
    public float baseAttackSpeed = 5;

    private void Awake()
    {
        CharacterStats = new CharacterStats(baseHealth, baseAttack, baseAttackPercent, baseAttackFlat,
            baseDamageBonus, baseCritRate, baseCritDamage, baseDefense, baseAttackSpeed);
    }
}
