using UnityEngine;

public class SetActiveOnTrigger : MonoBehaviour
{
    private Transform[] _children;
    private BoxCollider2D _boxCollider;
    private AudioManager _audioManager;

    private void Awake()
    {
        _boxCollider = GetComponent<BoxCollider2D>();
        _audioManager = FindObjectOfType<AudioManager>();
        GetAllChildren();
    }

    private void Start()
    {
        if (_children == null || _children.Length == 0)
        {
            Debug.LogError("No children found!");
            Destroy(this.gameObject);
        }
        else
        {
            SetObjectActive(this.transform, true);
            SetChildrenActive(false);
        }
    }

    private void GetAllChildren()
    {
        int childCount = transform.childCount;
        _children = new Transform[childCount];

        for (int i = 0; i < childCount; i++)
        {
            _children[i] = transform.GetChild(i);
        }
    }

    private void SetObjectActive(Transform gameObject, bool state)
    {
        if (gameObject != null)
        {
            gameObject.gameObject.SetActive(state);
        }
    }

    private void SetChildrenActive(bool state)
    {
        foreach (Transform child in _children)
        {
            if (child != null)
            {
                child.gameObject.SetActive(state);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            SetChildrenActive(true);
            PlaySound();
            _boxCollider.enabled = false;
        }
    }

    private void PlaySound()
    {
        _audioManager.PlaySound("OnEventTrigger");
    }
}
