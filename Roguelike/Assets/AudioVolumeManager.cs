using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioVolumeManager : MonoBehaviour
{
    [SerializeField] private Slider _masterSlider;
    [SerializeField] private Slider _bgmSlider;
    [SerializeField] private Slider _sfxSlider;

    private float _sliderDefaultValue = 1;

    private void Awake()
    {
        _masterSlider.value = _sliderDefaultValue;
        _bgmSlider.value = _sliderDefaultValue;
        _sfxSlider.value = _sliderDefaultValue;

        _masterSlider.onValueChanged.AddListener((volume) =>
        {
            //AudioManager.Instance.SetMasterVolume(volume);
            AudioManager.Instance.ChangeGlobalVolume(volume, AudioManager.Instance.CurrentBGMVolume, AudioManager.Instance.CurrentSFXVolume);
            Debug.Log(volume);

        });


        _bgmSlider.onValueChanged.AddListener((volume) =>
        {
            //AudioManager.Instance.SetBGMVolume(volume);
            AudioManager.Instance.ChangeGlobalVolume(AudioManager.Instance.CurrentMasterVolume, volume, AudioManager.Instance.CurrentSFXVolume);

            Debug.Log(volume);

        });


        _sfxSlider.onValueChanged.AddListener((volume) =>
        {
            //AudioManager.Instance.SetSFXVolume(volume);
            AudioManager.Instance.ChangeGlobalVolume(AudioManager.Instance.CurrentMasterVolume, AudioManager.Instance.CurrentBGMVolume, volume);

            Debug.Log(volume);

        });
    }


}
