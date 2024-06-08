using UnityEngine;

public class Beetle_CollectibleManager : MonoBehaviour
{
    [SerializeField] private Collectible[] _beetleCollectibles;
    public Collectible[] BettleCollectibles { get { return _beetleCollectibles; } private set { } }
}
