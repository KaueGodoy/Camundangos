using UnityEngine;

public class MouseManager : MonoBehaviour
{
    public static MouseManager Instance { get; private set; }

    private bool _isCursorShowing = false;

    private void Awake()
    {
        Instance = this;
    }
    void Start()
    {
        GameInput.Instance.OnShowCursorPressed += GameInput_OnShowCursorPressed;

        HideCursor();
    }

    private void GameInput_OnShowCursorPressed(object sender, System.EventArgs e)
    {
        TriggerCursorState();
    }

    public void HideCursor()
    {
        //Cursor.lockState = CursorLockMode.Locked;
        //Cursor.visible = false;
    }

    public void ShowCursor()
    {
        //Cursor.lockState = CursorLockMode.None;
        //Cursor.visible = true;
    }

    public void TriggerCursorState()
    {
        if (_isCursorShowing)
        {
            HideCursor();
        }
        else
        {
            ShowCursor();
        }

        _isCursorShowing = !_isCursorShowing;
    }

    private void OnDisable()
    {
        GameInput.Instance.OnShowCursorPressed -= GameInput_OnShowCursorPressed;
    }
}
