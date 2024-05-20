using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossEntrance : MonoBehaviour
{

    [SerializeField] EnemyKnockback enemyKnockback;

    private BoxCollider2D boxCollider;

    private void Awake()
    {
        boxCollider = GetComponent<BoxCollider2D>();
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

                enemyKnockback.GetComponent<EnemyKnockback>().enabled = false;
                boxCollider.GetComponent<EnemyKnockback>().enabled = false;
                Debug.Log("All characters unlocked");
            }
            else
            {
                Debug.Log("Unlock all characters");
            }
        }
    }

}
