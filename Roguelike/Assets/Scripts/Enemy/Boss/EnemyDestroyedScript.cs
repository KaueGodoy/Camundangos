using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class EnemyDestroyedScript : MonoBehaviour
{
    public UnityEvent<GameObject> OnEnemyDestroyed; // Event to be invoked when the enemy is destroyed

    private BossBattle bossBattle;

    private void Start()
    {
        bossBattle.AddComponent<BossBattle>();
    }

    public void OnDestroy()
    {
        // Invoke the OnEnemyDestroyed event when the enemy is destroyed
        //OnEnemyDestroyed?.Invoke(gameObject);
        bossBattle.enemySpawnList.Remove(gameObject);
    }
}
