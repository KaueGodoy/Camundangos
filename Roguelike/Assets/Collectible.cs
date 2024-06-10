using UnityEngine;
using UnityEngine.Localization.Settings;

[RequireComponent(typeof(BoxCollider2D))]
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
        CollectItem();
        DestroyGameObjectWithDelay(0.4f);
    }

    public virtual void CollectItem()
    {
        HasBeenCollected = true;
        Debug.Log(HasBeenCollected);

        AudioManager.Instance.PlaySound("OnChestOpen");
        ParticleManager.Instance.InstantiateParticle(_collectibleParticle, transform.position);
    }
}
