using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Localization.Settings;

public class Chest : Interactable
{
    public event EventHandler OnChestOpenedParticles;

    [SerializeField] private List<string> drops = new List<string>();

    //private string _chestKey = "ChestKey";

    public override void Awake()
    {
        base.Awake();

        //LocalizationSettings.StringDatabase.GetLocalizedStringAsync(_chestKey).Completed += handle =>
        //{
        //    string localizedMessage = handle.Result;

        //    SetName(localizedMessage);
        //};
    }

    public List<string> Drops
    {
        get { return drops; }
        set { drops = value; }
    }

    public override void ExecuteInteraction()
    {
        base.ExecuteInteraction();
        OpenChest();
        DestroyGameObjectWithDelay(0.4f);
    }

    public virtual void OpenChest()
    {
        if (Drops == null) return;

        foreach (var item in Drops)
        {
            GiveItemToPlayer(item);
        }

        AudioManager.Instance.PlaySound("OnChestOpen");
        OnChestOpenedParticles?.Invoke(this, EventArgs.Empty);
    }
}
