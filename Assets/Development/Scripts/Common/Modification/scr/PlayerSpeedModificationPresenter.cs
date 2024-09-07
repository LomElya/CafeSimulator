using Modification;
using UnityEngine;

public class PlayerSpeedModificationPresenter : ModificationPresenter<PlayerSpeedRateModification, float>
{
    [SerializeField] private PlayerMovable _movement;

    public PlayerSpeedModificationPresenter()
    {
    }

    protected override void Enabled() => AddListener(_movement);
}
