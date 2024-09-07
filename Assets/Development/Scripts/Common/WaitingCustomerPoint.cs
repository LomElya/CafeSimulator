using System;
using System.Collections;
using UnityEngine;

public abstract class WaitingCustomerPoint : MonoBehaviour
{
    public event Action BecameFree;
    public event Action Complite;

    protected const float NewCustomerSetDelay = 1.0f;

    [SerializeField] protected Transform _customerWaitPoint;
    [SerializeField] private CustomerTrigger _customerTrigger;

    protected Customer _activeCustomer;

    public bool Active { get; private set; }
    public bool Free { get; private set; } = true;

    private void OnEnable() =>
        _customerTrigger.Enter += OnCustomerEnter;

    private void Start() =>
        SetActive(false);

    private void OnDisable() =>
        _customerTrigger.Enter -= OnCustomerEnter;

    public Transform WaitAndTake(Customer customer)
    {
        _activeCustomer = customer;

        Free = false;
        OnEnter();

        return _customerWaitPoint;
    }

    public void EndTransit()
    {
        Free = true;
    }

    public void Leave()
    {
        SetActive(false);
        OnLeave();
        BecameFree?.Invoke();
    }

    public void CompliteInvoke() => Complite?.Invoke();

    private void OnCustomerEnter(Customer customer)
    {
        if (customer != _activeCustomer)
            return;

        StartCoroutine(NewCustomerActivationDelay());
    }

    private IEnumerator NewCustomerActivationDelay()
    {
        yield return new WaitForSeconds(NewCustomerSetDelay);
        SetActive(true);
    }

    private void SetActive(bool active) => Active = active;

    protected virtual void OnLeave() { }
    protected virtual void OnEnter() { }
}
