using UnityEngine;

public class CashDeskAudio : BaseAudio
{
    [SerializeField] private SoundSettings _cashDeskSound;

    public void PlayAudio()
    {
        _cashDeskSound.MixerGroup.audioMixer.SetFloat("CashDeskPitch", UnityEngine.Random.Range(0.98f, 1.03f));
        Play(_cashDeskSound);
    }
}
