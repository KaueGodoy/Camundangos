using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InventoryUI : MonoBehaviour
{
    public RectTransform inventoryPanel;
    public RectTransform scrollViewContent;
    InventoryUIItem ItemContainer { get; set; }
    bool MenuIsActive { get; set; }
    Item CurrentSelectedItem { get; set; }

    private PlayerControls playerControls;

    private AudioManager _audioManager;


    private void Awake()
    {
        playerControls = new PlayerControls();

        ItemContainer = Resources.Load<InventoryUIItem>("UI/Item_Container");

        _audioManager = FindObjectOfType<AudioManager>();


        UIEventHandler.OnItemAddedToInventory += ItemAdded;
    }
    private void OnEnable()
    {
        playerControls.Enable();
    }

    private void OnDisable()
    {
        playerControls.Disable();
    }

    private void Start()
    {
        inventoryPanel.gameObject.SetActive(false);
    }

    private void Update()
    {
        if (playerControls.UI.Inventory.triggered)
        {
            ActivateMenu();
        }
    }

    private void ActivateMenu()
    {
        MenuIsActive = !MenuIsActive;
        inventoryPanel.gameObject.SetActive(MenuIsActive);
        OpenMenu(MenuIsActive);
    }

    private bool OpenMenu(bool menuOpened)
    {
        if (menuOpened)
            _audioManager.PlaySound("OnInventoryOpened");

        return menuOpened;
    }

    public void ItemAdded(Item item)
    {
        InventoryUIItem emptyItem = Instantiate(ItemContainer);
        emptyItem.SetItem(item);
        emptyItem.transform.SetParent(scrollViewContent);
    }
}
