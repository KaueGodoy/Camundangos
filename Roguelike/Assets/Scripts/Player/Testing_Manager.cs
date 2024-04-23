using UnityEngine;

public class Testing_Manager : MonoBehaviour
{
    [SerializeField] private Testing_PlayerMovement _testingPlayerMovementPanel;

    private bool _isActive;

    private void Start()
    {
        _isActive = false;
        _testingPlayerMovementPanel.gameObject.SetActive(_isActive);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Y))
        {
            _isActive = !_isActive;
            _testingPlayerMovementPanel.gameObject.SetActive(_isActive);
        }
    }
}
