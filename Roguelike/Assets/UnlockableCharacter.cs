using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnlockableCharacter : NPC
{
    public event EventHandler OnIsaUnlockedVisual;

    public void InvokeIsaUnlockedVisual()
    {
        OnIsaUnlockedVisual?.Invoke(this, EventArgs.Empty);
    }
}
