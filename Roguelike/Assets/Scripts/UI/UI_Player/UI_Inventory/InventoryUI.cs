using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    [Header("Inventory")]
    [SerializeField] private RectTransform _inventoryPanel;

    [Header("Sections")]
    [SerializeField] private RectTransform _sectionPanelWeapon;
    [SerializeField] private RectTransform _sectionPanelConsumable;

    [Header("Content")]
    [SerializeField] private RectTransform _weaponScrollViewContent;
    [SerializeField] private RectTransform _consumableScrollViewContent;

    public InventoryUIItem ItemContainer { get; set; }
    public Item CurrentSelectedItem { get; set; }
    public bool MenuIsActive { get; set; }

    private AudioManager _audioManager;

    private void Awake()
    {
        ItemContainer = Resources.Load<InventoryUIItem>("UI/Item_Container");
        _audioManager = FindObjectOfType<AudioManager>();
        UIEventHandler.OnItemAddedToInventory += ItemAdded;
    }

    private void Start()
    {
        _inventoryPanel.gameObject.SetActive(false);
        _sectionPanelWeapon.gameObject.SetActive(true);
        _sectionPanelConsumable.gameObject.SetActive(false);

        GameInput.Instance.OnInventoryPressed += GameInput_OnInventoryPressed;
    }

    private void GameInput_OnInventoryPressed(object sender, System.EventArgs e)
    {
        ActivateMenu();
    }

    private void OnDisable()
    {
        UIEventHandler.OnItemAddedToInventory -= ItemAdded;
    }

    public void ActivateMenu()
    {
        MenuIsActive = !MenuIsActive;
        _inventoryPanel.gameObject.SetActive(MenuIsActive);
        OpenMenu(MenuIsActive);
        CloseMenu(!MenuIsActive);
    }

    private bool OpenMenu(bool menuOpened)
    {
        if (menuOpened)
            _audioManager.PlaySound("OnInventoryOpened");

        return menuOpened;
    }

    private bool CloseMenu(bool menuClosed)
    {
        if (menuClosed)
            _audioManager.PlaySound("OnInventoryClosed");

        return menuClosed;
    }

    private void EnableSection()
    {
        _audioManager.PlaySound("OnInventorySectionSelected");
    }

    public void EnableSectionWeapon()
    {
        _sectionPanelWeapon.gameObject.SetActive(true);
        _sectionPanelConsumable.gameObject.SetActive(false);
        EnableSection();
    }

    public void EnableSectionConsumable()
    {
        _sectionPanelWeapon.gameObject.SetActive(false);
        _sectionPanelConsumable.gameObject.SetActive(true);
        EnableSection();
    }

    public void ItemAdded(Item item)
    {
        InventoryUIItem emptyItem = Instantiate(ItemContainer);
        emptyItem.SetItem(item);

        if (item.ItemType == Item.ItemTypes.Weapon)
        {
            emptyItem.transform.SetParent(_weaponScrollViewContent);
        }
        else if (item.ItemType == Item.ItemTypes.Consumable)
        {
            emptyItem.transform.SetParent(_consumableScrollViewContent);
        }
        else
        {
            emptyItem.transform.SetParent(_weaponScrollViewContent);
            Debug.LogWarning($"{emptyItem.gameObject.name} does not belong to defined types of item");
        }
    }
}
