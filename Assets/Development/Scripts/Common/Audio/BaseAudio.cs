using System;
using UnityEngine;
using UnityEngine.Audio;

public abstract class BaseAudio : MonoBehaviour
{
    [SerializeField] private AudioSource _audioSource;

    protected void Play(SoundSettings soundSettings)
    {
        _audioSource.outputAudioMixerGroup = soundSettings.MixerGroup;
        _audioSource.PlayOneShot(soundSettings.Clip);
    }
}

[Serializable]
public class SoundSettings
{
    [SerializeField] private AudioClip _clip;
    [SerializeField] private AudioMixerGroup _mixerGroup;

    public AudioClip Clip => _clip;
    public AudioMixerGroup MixerGroup => _mixerGroup;
}
