using System;
using UnityEngine;

public class PlayRandomAudio : MonoBehaviour
{
    [SerializeField] private TextAsset _file;
    [SerializeField] private string[] _environmentSoundNames;

    [SerializeField] private float _minTimeBetweenPlays = 5f;
    [SerializeField] private float _maxTimeBetweenPlays = 15f;

    private bool _isInForest;
    private float _nextPlayTime;

    private void Awake()
    {
        LoadFile();
    }

    private void Start()
    {
        _isInForest = true;
        SetNextPlayTime();
    }

    private void LoadFile()
    {
        _environmentSoundNames = _file ? _file.text.Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries) : null;
    }

    private void Update()
    {
        if (Time.time >= _nextPlayTime)
        {
            PlayRandomizedAudio();
            SetNextPlayTime();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Rigidbody2D playerRigidbody = collision.GetComponent<Rigidbody2D>();

            if (playerRigidbody.velocity.x > 0)
            {
                _isInForest = true;
                Debug.Log("From right collision, enabling bird sounds");
            }
            else
            {
                _isInForest = false;
                Debug.Log("From left collision, desabling bird sounds");
            }
        }
    }

    private void PlayRandomizedAudio()
    {
        if (_environmentSoundNames.Length == 0)
        {
            Debug.LogError("No audio clip names assigned to environmentSoundNames.");
            return;
        }

        string randomSoundName = _environmentSoundNames[UnityEngine.Random.Range(0, _environmentSoundNames.Length)];

        if (_isInForest)
        {
            AudioManager.Instance.PlaySound(randomSoundName);
            Debug.Log("Is in forest:" + _isInForest);
        }
        else
        {
            AudioManager.Instance.StopSFX(randomSoundName);
            Debug.Log("Is in forest:" + _isInForest);

        }

        Debug.Log("Playing audio: " + randomSoundName);
    }

    private void SetNextPlayTime()
    {
        _nextPlayTime = Time.time + UnityEngine.Random.Range(_minTimeBetweenPlays, _maxTimeBetweenPlays);
    }
}
