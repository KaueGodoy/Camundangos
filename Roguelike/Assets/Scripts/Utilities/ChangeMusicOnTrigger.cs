using UnityEngine;

public class ChangeMusicOnTrigger : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Rigidbody2D playerRigidbody = collision.GetComponent<Rigidbody2D>();

            if (playerRigidbody.velocity.x > 0)
            {
                AudioManager.Instance.StopSound("Theme");
                AudioManager.Instance.PlaySound("BGM_Cave");
            }
            else
            {
                AudioManager.Instance.StopSound("BGM_Cave");
                AudioManager.Instance.PlaySound("Theme");
            }
        }
    }
}
