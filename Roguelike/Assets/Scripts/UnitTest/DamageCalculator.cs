using System;

public class DamageCalculator
{
    
    public static int CalculateDamage(int amount, float mitigationPercent)
    {
        float multiplier = 1f - mitigationPercent;
            
        return Convert.ToInt32((amount * multiplier));
    }

    public static int CalculateDamageMyWay(int amount, float mitigationPercent)
    {
        int originalDamage = Convert.ToInt32((amount * mitigationPercent));

        int finalDamage = amount - originalDamage;


        return finalDamage;
    }
}
