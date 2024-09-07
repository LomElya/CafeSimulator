using Zenject;
using UnityEngine;

public class GameInstaller : MonoInstaller
{
    [SerializeField] private CustomerSpawner _customerSpawner;
    [SerializeField] CharacterReferences _characterReferences;

    public override void InstallBindings()
    {
        BindSpawner();
        BindFabric();
        BindReferences();
    }

    private void BindSpawner()
    {
        Container.Bind<CustomerSpawner>().FromInstance(_customerSpawner).AsSingle();
    }

    private void BindFabric()
    {
        CustomerStateeFabric customerStateeFabric = new CustomerStateeFabric();
        Container.BindInstance(customerStateeFabric);
        Container.QueueForInject(customerStateeFabric);
    }

    private void BindReferences()
    {
        Container.Bind<CharacterReferences>().FromInstance(_characterReferences).AsSingle();
    }
}
