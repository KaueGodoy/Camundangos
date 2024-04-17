using System;
using UnityEngine;

public class UnlockableCharacter : NPC
{
    public event EventHandler OnIsaUnlockedVisual;
    public event EventHandler OnMatiasUnlockedVisual;
    public event EventHandler OnLeoUnlockedVisual;

    [SerializeField] private float _timeToDestroy = 0.3f;
    public float TimeToDestroySelf { get { return _timeToDestroy; } set { _timeToDestroy = value; } }

    public void InvokeIsaUnlockedVisual()
    {
        OnIsaUnlockedVisual?.Invoke(this, EventArgs.Empty);
    }

    public void InvokeMatiasUnlockedVisual()
    {
        OnMatiasUnlockedVisual?.Invoke(this, EventArgs.Empty);
    }

    public void InvokeLeoUnlockedVisual()
    {
        OnLeoUnlockedVisual?.Invoke(this, EventArgs.Empty);
    }
}
