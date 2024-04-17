using UnityEngine;
using UnityEngine.Tilemaps;

public class DestroyTile : MonoBehaviour
{
    [SerializeField] private Tilemap _destructibleTileMap;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("PlayerProjectile"))
        {
            Vector3 hitPosition = Vector3.zero;

            Debug.Log("Hit");

            foreach (ContactPoint2D hit in collision.contacts)
            {
                hitPosition.x = hit.point.x - 0.01f * hit.normal.x;
                hitPosition.y = hit.point.y - 0.01f * hit.normal.y;

                _destructibleTileMap.SetTile(_destructibleTileMap.WorldToCell(hitPosition), null);
            }
        }
    }
}
