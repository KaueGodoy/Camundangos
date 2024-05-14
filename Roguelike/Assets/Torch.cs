using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Torch : MonoBehaviour
{
    private bool _hasStartedPlaying = false;

    private void Update()
    {
        float timer = 0;
        if (!_hasStartedPlaying)
        {
            timer += Time.deltaTime;
        }

        if (timer >= 2f)
        {
            PlaySoundOnce();
            _hasStartedPlaying = true;
        }

    }

    private void PlaySoundOnce()
    {
        if (!_hasStartedPlaying)
        {
            AudioManager.Instance.PlaySound("OnTorchPlaying");
            Debug.Log("Audio playing");
        }
    }
}
