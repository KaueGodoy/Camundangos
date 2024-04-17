using System;
using UnityEngine;

public class PlayRandomAudio : MonoBehaviour, IHasSound
{
    public AudioManager AudioManagerInstance { get; set; }

    [SerializeField] private TextAsset _file;
    [SerializeField] private string[] _environmentSoundNames;

    [SerializeField] private float _minTimeBetweenPlays = 5f;
    [SerializeField] private float _maxTimeBetweenPlays = 15f;

    private float _nextPlayTime;

    private void Awake()
    {
        AudioManagerInstance = FindObjectOfType<AudioManager>();
        LoadFile();
    }

    private void Start()
    {
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

    private void PlayRandomizedAudio()
    {
        if (_environmentSoundNames.Length == 0)
        {
            Debug.LogError("No audio clip names assigned to environmentSoundNames.");
            return;
        }

        string randomSoundName = _environmentSoundNames[UnityEngine.Random.Range(0, _environmentSoundNames.Length)];

        AudioManagerInstance.PlaySound(randomSoundName);
        Debug.Log("Playing audio: " + randomSoundName);
    }

    private void SetNextPlayTime()
    {
        _nextPlayTime = Time.time + UnityEngine.Random.Range(_minTimeBetweenPlays, _maxTimeBetweenPlays);
    }
}
