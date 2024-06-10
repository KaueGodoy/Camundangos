using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectible_Album : Collectible
{
    public override void CollectItem()
    {
        base.CollectItem();

        Debug.Log("Album collected");
        // Increase ult and skill scaling 

        PlayerSkill.Instance.SkillMultiplier += 0.20f;
    }
}
