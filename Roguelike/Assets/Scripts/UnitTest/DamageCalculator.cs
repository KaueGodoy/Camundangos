using System;

public class DamageCalculator
{
    public static int CalculateDamage(int amount, float mitigationPercet)
    {
        return Convert.ToInt32((amount * mitigationPercet));
    }

}
