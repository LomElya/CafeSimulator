using UnityEngine;

public class MoneyMagnetHaptics : BaseHaptics
{
    [SerializeField] private MoneyMagnit _moneyMagnet;
    [SerializeField] private MoneyAudio _moneyAudio;

    private void OnEnable() => _moneyMagnet.Attracted += OnAttracted;
    private void OnDisable() => _moneyMagnet.Attracted -= OnAttracted;
    private void OnAttracted(int count) => _moneyAudio.PlayMoneySound();
}
