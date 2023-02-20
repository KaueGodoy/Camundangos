using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Sound
{
    public AudioClip clip;

    public string name;
    [Range(0f, 1f)] public float volume;
    [Range(.1f, 3f)] public float pitch;
    public bool loop;
    [Range(0f, 1f)] public float spatialBlend;

    [HideInInspector]
    public AudioSource source;
}
