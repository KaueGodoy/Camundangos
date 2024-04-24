using UnityEngine;

public class SkeletonV3_Bone : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        NewPlayerController player = collision.GetComponent<NewPlayerController>();

        if (player != null)
        {
            NewPlayerController.Instance.TakeDamage(2f);
        }
    }
}
