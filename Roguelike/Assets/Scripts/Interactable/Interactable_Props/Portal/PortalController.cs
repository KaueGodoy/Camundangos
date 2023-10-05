using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PortalController : MonoBehaviour
{
    [SerializeField]
    private Button button;
    private Portal[] portal;
    private PlayerController player;
    private GameObject panel;
    [SerializeField]
    private RectTransform faderTeleport;

    private void Start()
    {
        player = FindObjectOfType<PlayerController>();
        panel = transform.Find("Panel_Portals").gameObject;
    }

    public void ActivatePortal(Portal[] portals)
    {
        for (int i = 0; i < portals.Length; i++)
        {
            Button portalButton = Instantiate(button, panel.transform);
            portalButton.GetComponentInChildren<TMPro.TextMeshProUGUI>().text = portals[i].name;
            int x = i;
            portalButton.onClick.AddListener(delegate { OnPortalButtonClick(x, portals[x]); });
        }

        panel.SetActive(true);
    }

    public void DeactivatePortal()
    {
        foreach (Button button in GetComponentsInChildren<Button>())
        {
            Destroy(button.gameObject);
        }

        panel.SetActive(false);
    }

    void OnPortalButtonClick(int portalIndex, Portal portal)
    {
        //Scale
        faderTeleport.gameObject.SetActive(true);
        LeanTween.scale(faderTeleport, Vector3.zero, 0.3f);
        LeanTween.scale(faderTeleport, new Vector3(1, 1, 1), 0.8f).setEase(LeanTweenType.easeInOutQuad).setOnComplete(() =>
        {
            faderTeleport.gameObject.SetActive(false);
        });

        player.transform.position = portal.TeleportLocation;

        foreach (Button button in GetComponentsInChildren<Button>())
        {
            Destroy(button.gameObject);
        }

        panel.SetActive(false);
    }


}
