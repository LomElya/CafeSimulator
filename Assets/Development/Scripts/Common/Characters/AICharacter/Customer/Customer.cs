using System;
using GameHandler;
using UnityEngine;
using Zenject;

public class Customer : AICharacter
{
    [SerializeField] private MoneyPayer _moneyPayer;
    [SerializeField] private PurchaseListFactory _purchaseListFactory;
    // [SerializeField] private CustomersContent _customersContent;
    [SerializeField] private CustomerType _type;
    // [SerializeField] private DeadBody _deadBodyTemplate;

    public PurchaseList PurchaseList { get; private set; }
    public CustomerType Type => _type;
    public event Action<Customer> Left;

    private void Awake() =>
        PurchaseList = _purchaseListFactory.Create();

    public void CreateNewPurchaseList(StackableType type) =>
        CreateNewPurchaseList(Stack.CalculateCount(type));

    public void CreateNewPurchaseList() =>
        CreateNewPurchaseList(Stack.CalculateCount(StackableType.Burger) + 1);

    public void CreateNewPurchaseList(int count) =>
        PurchaseList = _purchaseListFactory.Create(count);


    public void Pay(MoneyZone moneyZone, int totalPrice)
    {
        if (gameObject.activeInHierarchy)
            _moneyPayer.Pay(moneyZone, totalPrice);
    }

    public void Leave() =>
        Left?.Invoke(this);
}
