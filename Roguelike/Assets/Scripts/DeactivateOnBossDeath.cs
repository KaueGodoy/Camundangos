using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeactivateOnBossDeath : MonoBehaviour
{
    [SerializeField] private BossBattle _bossBattle;

    private void Awake()
    {
        _bossBattle.OnBossDeath += _bossBattle_OnBossDeath;
    }

    private void _bossBattle_OnBossDeath(object sender, System.EventArgs e)
    {
        BoxCollider2D boxCollider = GetComponent<BoxCollider2D>();
        boxCollider.enabled = false;
        Debug.Log("Collision disabled");
    }

    private void OnDestroy()
    {
        _bossBattle.OnBossDeath -= _bossBattle_OnBossDeath;
    }
}
