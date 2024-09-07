using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

public class CustomerSpawner : MonoBehaviour
{
    [SerializeField] private int _maxCustomersAmount = 3;
    [SerializeField] private WorldZone[] _spawnZones;
    [SerializeField] private CustomersContent _customers;

    private readonly List<Customer> _spawnedCustomers = new List<Customer>();

    private CharacterReferences _characterReferences;
    private DiContainer _container;
    private Coroutine _spawning;

    public int SpawnedCount => _spawnedCustomers.Count;

    public event Action<Customer> Spawned;

    [Inject]
    private void Construct(CharacterReferences characterReferences, DiContainer container)
    {
        _characterReferences = characterReferences;
        _container = container;
    }

    private void Start() =>
        _spawning = StartCoroutine(Spawn());

    public void DestroyCustomer(Customer customer)
    {
        OnCustomerLeft(customer);
        Destroy(customer.gameObject);
    }

    private IEnumerator Spawn()
    {
        yield return new WaitForSeconds(2f);
        yield return new WaitUntil(() => _spawnedCustomers.Count < _maxCustomersAmount + 1);


        TrySpawnCustomer(_customers.TryGetRandomCustomer(CustomerType.Human));

        yield return new WaitForSeconds(1f);

        Customer customer = TrySpawnCustomer(_customers.TryGetRandomCustomer(CustomerType.Human));

        yield return new WaitUntil(() => _spawnedCustomers.Count == 0);

        for (int i = 0; ; i++)
        {
            yield return new WaitForSeconds(Random.Range(0.5f, 2f));
            yield return new WaitUntil(() => _characterReferences.ShopQueues.NotFull);

            TrySpawnCustomer(_customers.GetRandomCustomer());

            yield return new WaitForSeconds(1f);

            if (_characterReferences.ShopQueues.NotFull == false)
                yield return new WaitUntil(() => _characterReferences.ShopQueues.Empty);
        }
    }

    private Customer TrySpawnCustomer(Customer customer)
    {
        if (_spawnedCustomers.Count == _maxCustomersAmount + 1)
            return null;

        Customer result = CanSpawnOfType(customer) == false ? null : SpawnCustomer(customer);

        return result;
    }

    private bool CanSpawnOfType(Customer customer)
    {
        switch (customer.Type)
        {
            case CustomerType.Human:
                return true;

            default:
                return false;
        }
    }

    private Customer SpawnCustomer(Customer customer)
    {
        Vector3 spawnPosition = _spawnZones[Random.Range(0, _spawnZones.Length)].GetPoint();

        customer = Instantiate(customer, spawnPosition, Quaternion.identity);

        _container.Inject(customer);

        customer.transform.parent = transform;
        customer.Init(_characterReferences);
        customer.Run();
        _spawnedCustomers.Add(customer);
        customer.Left += OnCustomerLeft;
        Spawned?.Invoke(customer);

        return customer;
    }


    private void OnCustomerLeft(Customer customer)
    {
        customer.Left -= OnCustomerLeft;
        _spawnedCustomers.Remove(customer);
    }
}