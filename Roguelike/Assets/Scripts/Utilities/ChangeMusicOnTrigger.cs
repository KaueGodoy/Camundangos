using UnityEngine;

public class ChangeMusicOnTrigger : MonoBehaviour
{
    [SerializeField] private string _rightCollisionBGM = "BGM_Cave";
    [SerializeField] private string _leftCollisionBGM = "Theme";

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Rigidbody2D playerRigidbody = collision.GetComponent<Rigidbody2D>();

            if (playerRigidbody.velocity.x > 0)
            {
                AudioManager.Instance.StopBGM(AudioManager.Instance.CurrentBGM);
                AudioManager.Instance.PlayBGM(_rightCollisionBGM);
            }
            else
            {
                AudioManager.Instance.StopBGM(AudioManager.Instance.CurrentBGM);
                AudioManager.Instance.PlayBGM(_leftCollisionBGM);
            }
        }
    }

    //private void OnTriggerExit2D(Collider2D collision)
    //{
    //    if (collision.CompareTag("Player"))
    //    {
    //        Rigidbody2D playerRigidbody = collision.GetComponent<Rigidbody2D>();

    //        if (playerRigidbody.velocity.x > 0)
    //        {
    //            AudioManager.Instance.StopBGM("Theme");
    //            AudioManager.Instance.PlayBGM("BGM_Cave");
    //        }
    //        else
    //        {
    //            AudioManager.Instance.StopBGM("BGM_Cave");
    //            AudioManager.Instance.PlayBGM("Theme");
    //        }
    //    }
    //}
}
