using UnityEngine;

public class CaskDeskHaptics : BaseHaptics
{
    [SerializeField] private CashDesk _cashDesk;
    [SerializeField] private CashDeskAudio _audio;

    private void OnEnable() => _cashDesk.BecameFree += OnBecameFree;
    private void OnDisable() => _cashDesk.BecameFree -= OnBecameFree;

    private void OnBecameFree()
    {
        _audio.PlayAudio();
    }
}
