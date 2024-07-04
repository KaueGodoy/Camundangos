using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

public class PlayerNewCooldown : MonoBehaviour
{
    public static PlayerNewCooldown Instance { get; private set; }  

    [SerializeField] private PlayerAttack _playerAttack;
    private PlayerControls _playerControls;

    private float _fillAmountFull = 1f;

    private Dictionary<string, CooldownData> _skillCooldowns;
    private Dictionary<string, CooldownData> _ultCooldowns;

    private string _currentCharacter;

    #region Skill

    [Header("Skill")]
    [SerializeField] private TextMeshProUGUI cooldownText;
    [SerializeField] private Image foregroundImage;
    [SerializeField] private Image iconImage;

    private Color cooldownColor = new Color32(164, 164, 164, 255);
    private Color defaultColor = new Color32(255, 255, 255, 168);

    #endregion

    #region Ult

    [Header("Ult")]
    [SerializeField] private TextMeshProUGUI ultCooldownText;
    [SerializeField] private Image ultForegroundImage;
    [SerializeField] private Image ultIconImage;

    private Color _ultCooldownColor = new Color32(164, 164, 164, 255);
    private Color _ultDefaultColor = new Color32(255, 150, 150, 168);

    #endregion

    private void Awake()
    {
        Instance = this;

        _playerControls = new PlayerControls();

        _skillCooldowns = new Dictionary<string, CooldownData>();
        _ultCooldowns = new Dictionary<string, CooldownData>();

        InitializeCooldowns();
    }

    private void InitializeCooldowns()
    {
        // Initialize cooldowns for each character
        _skillCooldowns["MarceloVisual"] = new CooldownData(5.0f);
        _skillCooldowns["MatiasVisual"] = new CooldownData(5.0f);
        _skillCooldowns["IsaVisual"] = new CooldownData(5.0f);
        _skillCooldowns["LeoVisual"] = new CooldownData(5.0f);

        _ultCooldowns["MarceloVisual"] = new CooldownData(10.0f);
        _ultCooldowns["MatiasVisual"] = new CooldownData(10.0f);
        _ultCooldowns["IsaVisual"] = new CooldownData(10.0f);
        _ultCooldowns["LeoVisual"] = new CooldownData(10.0f);
    }

    private void Start()
    {
        _currentCharacter = "MarceloVisual"; // Default character
        ResetCooldown();

        GameInput.Instance.OnPlayerSkill += GameInput_OnPlayerSkill;
        GameInput.Instance.OnPlayerUlt += GameInput_OnPlayerUlt;
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
        TriggerCooldown(_skillCooldowns[_currentCharacter], cooldownText, foregroundImage, iconImage, cooldownColor, defaultColor);
        TriggerCooldown(_ultCooldowns[_currentCharacter], ultCooldownText, ultForegroundImage, ultIconImage, _ultCooldownColor, _ultDefaultColor);
    }

    private void GameInput_OnPlayerSkill(object sender, System.EventArgs e)
    {
        _skillCooldowns[_currentCharacter].TriggerCooldown = true;
    }

    private void GameInput_OnPlayerUlt(object sender, System.EventArgs e)
    {
        _ultCooldowns[_currentCharacter].TriggerCooldown = true;
    }

    public void UpdateCurrentCharacter(string characterName)
    {
        _currentCharacter = characterName;
        ResetCooldown();
    }

    private void TriggerCooldown(CooldownData cooldownData, TextMeshProUGUI text, Image foreground, Image icon, Color cooldownColor, Color defaultColor)
    {
        if (cooldownData.TriggerCooldown)
        {
            cooldownData.OffCooldown = false;
            cooldownData.CooldownTimer -= Time.deltaTime;

            int numberOfDecimals = 1;
            float convertedCooldownTimer = Mathf.Round(cooldownData.CooldownTimer * Mathf.Pow(10, numberOfDecimals)) / Mathf.Pow(10, numberOfDecimals);

            icon.color = cooldownColor;
            foreground.fillAmount = cooldownData.CooldownTimer / cooldownData.CooldownValue;
            text.gameObject.SetActive(true);
            text.text = convertedCooldownTimer + "";

            if (cooldownData.CooldownTimer <= 0)
            {
                cooldownData.OffCooldown = true;
                ResetCooldown(cooldownData, text, foreground, icon, defaultColor);
            }
        }
    }

    private void ResetCooldown()
    {
        ResetCooldown(_skillCooldowns[_currentCharacter], cooldownText, foregroundImage, iconImage, defaultColor);
        ResetCooldown(_ultCooldowns[_currentCharacter], ultCooldownText, ultForegroundImage, ultIconImage, _ultDefaultColor);
    }

    private void ResetCooldown(CooldownData cooldownData, TextMeshProUGUI text, Image foreground, Image icon, Color defaultColor)
    {
        icon.color = defaultColor;
        icon.gameObject.SetActive(true);
        cooldownData.TriggerCooldown = false;
        cooldownData.CooldownTimer = cooldownData.CooldownValue;
        text.gameObject.SetActive(false);
        foreground.fillAmount = _fillAmountFull;
        foreground.gameObject.SetActive(true);
    }

    private void OnDestroy()
    {
        GameInput.Instance.OnPlayerSkill -= GameInput_OnPlayerSkill;
        GameInput.Instance.OnPlayerUlt -= GameInput_OnPlayerUlt;
    }
}

public class CooldownData
{
    public float CooldownValue { get; private set; }
    public float CooldownTimer { get; set; }
    public bool TriggerCooldown { get; set; }
    public bool OffCooldown { get; set; }

    public CooldownData(float cooldownValue)
    {
        CooldownValue = cooldownValue;
        CooldownTimer = cooldownValue;
        TriggerCooldown = false;
        OffCooldown = true;
    }
}
