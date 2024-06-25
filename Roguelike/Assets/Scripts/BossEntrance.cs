using System;
using UnityEngine;

public class BossEntrance : MonoBehaviour
{
    public static BossEntrance Instance { get; private set; }

    public event EventHandler OnBossEntranceFailed;

    private BoxCollider2D _boxCollider;

    private void Awake()
    {
        Instance = this;
        _boxCollider = GetComponent<BoxCollider2D>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        NewPlayerController player = collision.gameObject.GetComponent<NewPlayerController>();

        if (player != null)
        {

            if (OnCharacterUnlocked.Instance.IsIsaUnlocked &&
                OnCharacterUnlocked.Instance.IsMatiasUnlocked &&
                OnCharacterUnlocked.Instance.IsLeoUnlocked)
            {
                _boxCollider.enabled = false;
                Debug.Log("All characters unlocked");
            }
            else
            {
                OnBossEntranceFailed?.Invoke(this, EventArgs.Empty);
                Debug.Log("Unlock all characters");
            }
        }
    }
}
