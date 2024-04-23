using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Testing_PlayerMovement : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _moveSpeedText;
    [SerializeField] private TMP_InputField _moveSpeedInputField;
    [SerializeField] private Button _moveSpeedButtonPlus;
    [SerializeField] private Button _moveSpeedButtonMinus;
    [Space]
    [SerializeField] private TextMeshProUGUI _dashSpeedText;
    [SerializeField] private TMP_InputField _dashSpeedInputField;
    [SerializeField] private Button _dashSpeedButtonPlus;
    [SerializeField] private Button _dashSpeedButtonMinus;
    [Space]
    [SerializeField] private TextMeshProUGUI _jumpForceText;
    [SerializeField] private TMP_InputField _jumpForceInputField;
    [SerializeField] private Button _JumpForceButtonPlus;
    [SerializeField] private Button _JumpForceButtonMinus;

    private void Awake()
    {
        SetInitialInputFieldValues();

        _moveSpeedButtonPlus.onClick.AddListener(() =>
        {
            float.TryParse(_moveSpeedInputField.text, out float inputSpeed);

            NewPlayerMovement.Instance.MoveSpeed += inputSpeed;
        });
        _moveSpeedButtonMinus.onClick.AddListener(() =>
        {
            float.TryParse(_moveSpeedInputField.text, out float inputSpeed);

            NewPlayerMovement.Instance.MoveSpeed -= inputSpeed;
        });

        _dashSpeedButtonPlus.onClick.AddListener(() =>
        {
            float.TryParse(_dashSpeedInputField.text, out float inputSpeed);

            NewPlayerMovement.Instance.DashSpeed += inputSpeed;
        });
        _dashSpeedButtonMinus.onClick.AddListener(() =>
        {
            float.TryParse(_dashSpeedInputField.text, out float inputSpeed);

            NewPlayerMovement.Instance.DashSpeed -= inputSpeed;
        });

        _JumpForceButtonPlus.onClick.AddListener(() =>
        {
            float.TryParse(_jumpForceInputField.text, out float inputSpeed);

            NewPlayerMovement.Instance.JumpMultiplier += inputSpeed;
        });
        _JumpForceButtonMinus.onClick.AddListener(() =>
        {
            float.TryParse(_jumpForceInputField.text, out float inputSpeed);

            NewPlayerMovement.Instance.JumpMultiplier -= inputSpeed;
        });
    }

    private void Start()
    {
        gameObject.SetActive(false);
    }

    private void Update()
    {
        _moveSpeedText.text = "Move speed: " + NewPlayerMovement.Instance.MoveSpeed;
        _jumpForceText.text = "Jump force: " + NewPlayerMovement.Instance.JumpMultiplier;
        _dashSpeedText.text = "Dash speed: " + NewPlayerMovement.Instance.DashSpeed;   
    }

    private void SetInitialInputFieldValues()
    {
        _moveSpeedInputField.text = "0.5";
        _dashSpeedInputField.text = "1";
        _jumpForceInputField.text = "0.5";
    }
}
