using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SignPost : ActionItem
{
    public override void InitiateInteraction()
    {
        base.InitiateInteraction();

        Debug.Log("Interacting with sign post");
    }


}
