using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerCooldowns : MonoBehaviour
{
    [SerializeField] private PlayerAttack _playerAttack;
    private PlayerControls _playerControls;

    private float _fillAmountFull = 1f;

    #region Skill

    [Header("Skill")]
    [SerializeField] private TextMeshProUGUI cooldownText;
    [SerializeField] private Image foregroundImage;
    [SerializeField] private Image iconImage;

    public float cooldownTimer;
    public float cooldownValue = 5.00f;
    public bool triggerCooldown;
    public bool offCooldown = true;

    public float target;
    public float reduceSpeed = 2f;

    Color cooldownColor = new Color32(164, 164, 164, 255);
    Color defaultColor = new Color32(255, 255, 255, 168);

    private Color _ultCooldownColor = new Color32(164, 164, 164, 255);
    private Color _ultDefaultColor = new Color32(255, 150, 150, 168);

    private void Awake()
    {
        _playerControls = new PlayerControls();
        offCooldown = true;
    }

    private void Start()
    {
        ResetCooldown();
        UltResetCooldown();

        GameInput.Instance.OnPlayerSkill += GameInput_OnPlayerSkill;
    }

    private void GameInput_OnPlayerSkill(object sender, System.EventArgs e)
    {
        triggerCooldown = true;
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

        if (_playerControls.Player.Ult.triggered)
        {
            ultTriggerCooldown = true;
        }

    }

    private void FixedUpdate()
    {
        TriggerCooldown();
        UltTriggerCooldown();
    }

    public void TriggerCooldown()
    {

        if (triggerCooldown)
        {
            offCooldown = false;

            cooldownTimer -= Time.deltaTime;

            int numberOfDecimals = 1;

            float convertedCooldownTimer = Mathf.Round(cooldownTimer * Mathf.Pow(10, numberOfDecimals)) / Mathf.Pow(10, numberOfDecimals);

            iconImage.color = cooldownColor;

            //iconImage.gameObject.SetActive(false);
            //foregroundImage.gameObject.SetActive(false);
            foregroundImage.fillAmount = cooldownTimer / cooldownValue;

            cooldownText.gameObject.SetActive(true);
            cooldownText.text = convertedCooldownTimer + "";

            _playerAttack.attackRequest = false;

            if (cooldownTimer <= 0)
            {
                offCooldown = true;
                ResetCooldown();
            }

        }

    }

    public void ResetCooldown()
    {
        iconImage.color = defaultColor;
        iconImage.gameObject.SetActive(true);
        triggerCooldown = false;
        cooldownTimer = cooldownValue;
        target = cooldownTimer / cooldownValue;
        cooldownText.gameObject.SetActive(false);
        foregroundImage.fillAmount = _fillAmountFull;
        foregroundImage.gameObject.SetActive(true);
    }

    #endregion

    #region Ult

    [Header("Ult")]
    [SerializeField] private TextMeshProUGUI ultCooldownText;
    [SerializeField] private Image ultForegroundImage;
    [SerializeField] private Image ultIconImage;

    public float ultCooldownTimer;
    public float ultCooldownValue = 10.0f;
    public bool ultTriggerCooldown;
    public bool ultOffCooldown = true;

    public float ultTarget;
    public float ultReduceSpeed = 2f;

    public void UltTriggerCooldown()
    {
        if (ultTriggerCooldown)
        {
            ultOffCooldown = false;

            ultCooldownTimer -= Time.deltaTime;

            int numberOfDecimals = 1;

            float convertedCooldownTimer = Mathf.Round(ultCooldownTimer * Mathf.Pow(10, numberOfDecimals)) / Mathf.Pow(10, numberOfDecimals);

            ultIconImage.color = _ultCooldownColor;

            //iconImage.gameObject.SetActive(false);
            //foregroundImage.gameObject.SetActive(false);
            ultForegroundImage.fillAmount = ultCooldownTimer / ultCooldownValue;

            ultCooldownText.gameObject.SetActive(true);
            ultCooldownText.text = convertedCooldownTimer + "";

            if (ultCooldownTimer <= 0)
            {
                ultOffCooldown = true;
                UltResetCooldown();
            }
        }
    }

    public void UltResetCooldown()
    {
        ultIconImage.color = _ultDefaultColor;
        ultIconImage.gameObject.SetActive(true);
        ultTriggerCooldown = false;
        ultCooldownTimer = ultCooldownValue;
        ultTarget = ultCooldownTimer / ultCooldownValue;
        ultCooldownText.gameObject.SetActive(false);
        ultForegroundImage.fillAmount = _fillAmountFull;
        ultForegroundImage.gameObject.SetActive(true);
    }

    #endregion
}
