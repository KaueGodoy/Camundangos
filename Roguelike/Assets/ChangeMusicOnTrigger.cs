using UnityEngine;

public class ChangeMusicOnTrigger : MonoBehaviour
{
    private AudioManager _audioManager;

    private void Awake()
    {
        _audioManager = FindObjectOfType<AudioManager>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Rigidbody2D playerRigidbody = collision.GetComponent<Rigidbody2D>();

            if (playerRigidbody.velocity.x > 0)
            {
                _audioManager.StopSound("Theme");
                _audioManager.PlaySound("BGM_Cave");
            }
            else
            {
                _audioManager.StopSound("BGM_Cave");
                _audioManager.PlaySound("Theme");
            }
        }
    }
}
