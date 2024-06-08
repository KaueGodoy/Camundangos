using UnityEngine;
using UnityEngine.Localization.Settings;

public class Collectible : Interactable
{
    [SerializeField] private ParticleSystem _collectibleParticle;
    [SerializeField] private string _collectibleKey = "CollectibleKey";

    public string CollectibleNameKey { get { return _collectibleKey; } set { _collectibleKey = value; } }
    
    public bool HasBeenCollected { get; set; }

    public override void Awake()
    {
        base.Awake();

        LocalizationSettings.StringDatabase.GetLocalizedStringAsync(CollectibleNameKey).Completed += handle =>
        {
            string localizedMessage = handle.Result;

            SetName(localizedMessage);
        };

        HasBeenCollected = false;
    }

    public override void ExecuteInteraction()
    {
        base.ExecuteInteraction();
        HasBeenCollected = true;
        Debug.Log(HasBeenCollected);

        AudioManager.Instance.PlaySound("OnChestOpen");
        ParticleManager.Instance.InstantiateParticle(_collectibleParticle, transform.position);
        DestroyGameObjectWithDelay(0.4f);
    }
}
