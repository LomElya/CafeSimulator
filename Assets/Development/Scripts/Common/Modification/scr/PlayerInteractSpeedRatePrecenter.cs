using Modification;
using UnityEngine;

public class PlayerInteractSpeedRatePrecenter : ModificationPresenter<PlayerInteractSpeedRate, float>
{
    [SerializeField] private TimerInteractableZone _interactableObject;

    public PlayerInteractSpeedRatePrecenter()
    {
    }

    protected override void Enabled() => AddListener(_interactableObject);
}
