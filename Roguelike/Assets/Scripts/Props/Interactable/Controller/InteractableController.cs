using TMPro;
using UnityEngine;

public class InteractableController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _itemNameText;

    private GameObject _panel_Interactible;

    private void Awake()
    {
        _panel_Interactible = transform.Find("Panel_Interactible").gameObject;
    }

    private void Start()
    {
        SetPanelVisibility(false);
    }

    public virtual void ShowInteractionUI(string interactionText)
    {
        SetPanelVisibility(true);
        SetText(interactionText);
    }

    public virtual void HideInteractionUI()
    {
        SetPanelVisibility(false);
        SetText(" ");
    }

    private string SetText(string text)
    {
        return _itemNameText.text = text;
    }

    private void SetPanelVisibility(bool visible)
    {
       _panel_Interactible.gameObject.SetActive(visible);
    }
}
