using System;
using UnityEngine;

public class OnCharacterUnlocked : MonoBehaviour
{
    public static OnCharacterUnlocked Instance { get; private set; }

    public event EventHandler OnMarceloUnlocked;
    public event EventHandler OnIsaUnlocked;

    public bool IsMarceloLocked { get; set; }
    public bool IsIsaLocked { get; set; }

    private void Awake()
    {
        Instance = this;    
    }

    public void UnlockIsa()
    {
        OnIsaUnlocked?.Invoke(this, EventArgs.Empty);
    }

    public bool GetMarceloLockedState()
    {
        return IsMarceloLocked;
    }
}
