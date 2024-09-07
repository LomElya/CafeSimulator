using UnityEngine;

public abstract class Item : MonoBehaviour
{
    [SerializeField] protected Collider _collider;

    private void Awake()
        => OnAwake();

    protected virtual void OnAwake() { }
}
