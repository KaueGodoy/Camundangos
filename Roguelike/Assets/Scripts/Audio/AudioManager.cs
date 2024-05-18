using UnityEngine;
using System;
using UnityEngine.Rendering;

public class AudioManager : MonoBehaviour
{
    public Sound[] sounds;

    public static AudioManager Instance;

    [SerializeField] private float _masterVolume = 1.0f;
    [SerializeField] private float _bgmVolume = 1.0f;
    [SerializeField] private float _sfxVolume = 1.0f;

    public string CurrentBGM { get; set; }
    public float CurrentMasterVolume { get { return _masterVolume; } set { _masterVolume = value; } }
    public float CurrentBGMVolume { get { return _bgmVolume; } set { _bgmVolume = value; } }
    public float CurrentSFXVolume { get { return _sfxVolume; } set { _sfxVolume = value; } }

    public Sound CurrentBGMPlaying { get; set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);

        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;

            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
            s.source.spatialBlend = s.spatialBlend;
        }
    }

    private void Start()
    {
        CurrentBGM = "Theme";

        PlayBGM(CurrentBGM);
    }

    public void PlayBGM(string name)
    {
        Sound sound = Array.Find(sounds, sound => sound.name == name);
        if (sound == null)
        {
            Debug.LogWarning("Sound " + name + " not found!");
            return;
        }

        if (PauseMenu.GameIsPaused)
        {
            sound.source.pitch = 0f;
            Debug.Log("volume changed");
        }
        sound.source.volume = sound.volume * CurrentBGMVolume * CurrentMasterVolume;
        CurrentBGMPlaying = sound;
        CurrentBGM = sound.name;
        CurrentBGMPlaying.source.Play();
    }

    public void PlaySound(string name)
    {
        Sound sound = Array.Find(sounds, sound => sound.name == name);
        if (sound == null)
        {
            Debug.LogWarning("Sound " + name + " not found!");
            return;
        }

        if (PauseMenu.GameIsPaused)
        {
            sound.source.pitch = 0f;
            Debug.Log("volume changed");
        }

        sound.source.volume = sound.volume * CurrentSFXVolume * CurrentMasterVolume;
        Debug.Log(sound.volume);
        sound.source.Play();
    }

    public void PlaySoundOneShot(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.LogWarning("Sound " + name + " not found!");
            return;

        }
        s.source.volume = s.volume * CurrentMasterVolume;
        s.source.PlayOneShot(s.clip);

    }

    public void StopBGM(string name)
    {
        Sound sound = Array.Find(sounds, sound => sound.name == name);
        if (sound == null)
        {
            Debug.LogWarning("Sound " + name + " not found!");
            return;
        }

        CurrentBGMPlaying = sound;
        CurrentBGM = sound.name;
        CurrentBGMPlaying.source.Stop();
    }

    public void SetMasterVolume(float volume)
    {
        CurrentMasterVolume = volume;
    }

    public void SetBGMVolume(float volume)
    {
        CurrentBGMVolume = volume;
    }

    public void SetSFXVolume(float volume)
    {
        CurrentSFXVolume = volume;
    }

    public void ChangeGlobalVolume(float masterVolume, float bgmVolume, float sfxVolume)
    {
        SetMasterVolume(masterVolume);
        SetBGMVolume(bgmVolume);
        SetSFXVolume(sfxVolume);

        StopBGM(CurrentBGM);
        PlayBGM(CurrentBGM);
    }
}
