using System;
using UnityEngine;

public class OnCharacterUnlocked : MonoBehaviour
{
    public static OnCharacterUnlocked Instance { get; private set; }

    public event EventHandler OnIsaUnlocked;

    public bool IsIsaUnlocked { get; set; }

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

}
