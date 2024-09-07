using UnityEngine;

[RequireComponent(typeof(Animator))]
public class CharacterView : MonoBehaviour
{
    protected Animator _animator;

    public const string isRun = nameof(isRun);

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        OnAwake();
    }

    public void SetMove(bool moving)
    {
        if (_animator)
            _animator.SetBool(isRun, moving);
    }

    protected virtual void OnAwake() { }
}
