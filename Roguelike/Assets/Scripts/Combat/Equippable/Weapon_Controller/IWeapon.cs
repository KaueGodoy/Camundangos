using System.Collections.Generic;

public interface IWeapon
{
    List<BaseStat> Stats { get; set; }
    float CurrentDamage { get; set; }
    void PerformAttack(float damage);
    void PerformSkillAttack();
    void PerformUltAttack();
}
