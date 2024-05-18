using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slime : BaseEnemy
{
    public bool IsDamaged()
    {
        return CurrentHealth < MaxHealth;
    }
}
