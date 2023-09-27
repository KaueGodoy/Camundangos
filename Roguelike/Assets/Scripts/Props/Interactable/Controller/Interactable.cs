using UnityEngine;

public abstract class Interactable : MonoBehaviour, IInteractable
{
    private InteractableController _interactableController;
    private PlayerControls _playerControls;

    public string Name { get; set; }

    private bool _canInteract;
    public bool CanInteract { get { return _canInteract; } set { } }

    public virtual void Awake()
    {
        _interactableController = FindObjectOfType<InteractableController>();
        _playerControls = new PlayerControls();

        SetName("Default name");
    }

    private void OnEnable()
    {
        _playerControls.Enable();
    }

    private void OnDisable()
    {
        _playerControls.Disable();
    }

    private void Update()
    {
        if (_canInteract) EnableInteraction();
    }

    public virtual void InitiateInteraction()
    {
        Debug.Log($"Interacting with {gameObject.name}");
        _interactableController.ShowInteractionUI(Name);
        _canInteract = true;
    }

    public virtual void DisableInteraction()
    {
        Debug.Log("Disabling interaction");
        _interactableController.HideInteractionUI();
        _canInteract = false;
    }

    public virtual void EnableInteraction()
    {
        if (_playerControls.UI.Interact.triggered)
        {
            ExecuteInteraction();
        }
    }

    public virtual void ExecuteInteraction()
    {
        Debug.Log("Interaction executed");
        _canInteract = false;
    }

    public virtual void GiveItemToPlayer(string itemName)
    {
        InventoryController.Instance.GiveItem(itemName);
    }

    public virtual void DestroyGameObject()
    {
        Destroy(gameObject);
    }

    public string SetName(string name)
    {
        return Name = name;
    }

    public virtual void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerController player = collision.GetComponent<PlayerController>();

        if (player != null)
        {
            InitiateInteraction();
        }
    }

    public virtual void OnTriggerExit2D(Collider2D collision)
    {
        PlayerController player = collision.GetComponent<PlayerController>();

        if (player != null)
        {
            DisableInteraction();
        }
    }
}
