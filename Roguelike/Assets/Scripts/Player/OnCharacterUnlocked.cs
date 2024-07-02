using System;
using UnityEngine;

public class OnCharacterUnlocked : MonoBehaviour
{
    public static OnCharacterUnlocked Instance { get; private set; }

    public event EventHandler OnIsaUnlocked;
    public event EventHandler OnMatiasUnlocked;
    public event EventHandler OnLeoUnlocked;

    public event EventHandler OnCharacterUnlockedVisual;

    public bool IsIsaUnlocked { get; set; }
    public bool IsMatiasUnlocked { get; set; }
    public bool IsLeoUnlocked { get; set; }

    private void Awake()
    {
        Instance = this;    
    }

    private void Start()
    {
        IsIsaUnlocked = false;
    }

    public void UnlockIsaInvokingEvent()
    {
        OnIsaUnlocked?.Invoke(this, EventArgs.Empty);
    }

    public bool UnlockIsaUpdatingState()
    {
        return IsIsaUnlocked = true;
    }

    public void UnlockMatiasInvokingEvent()
    {
        OnMatiasUnlocked?.Invoke(this, EventArgs.Empty);
    }

    public bool UnlockMatiasUpdatingState()
    {
        return IsMatiasUnlocked = true;
    }

    public void UnlockLeoInvokingEvent()
    {
        OnLeoUnlocked?.Invoke(this, EventArgs.Empty);
    }

    public bool UnlockLeoUpdatingState()
    {
        return IsLeoUnlocked = true;
    }

    public void InvokeOnCharacterUnlockedVisualUpdate()
    {
        OnCharacterUnlockedVisual?.Invoke(this, EventArgs.Empty);
    }
}
