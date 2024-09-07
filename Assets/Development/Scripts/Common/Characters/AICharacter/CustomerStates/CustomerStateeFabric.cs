using UnityEngine;
using Zenject;

public class CustomerStateeFabric
{
    private DiContainer _container;

    [Inject]
    public void Construct(DiContainer container)
    {
        _container = container;
    }

    public CustomerState CreateState(CustomerStateType stateType, AICharacter parent)
    {
        CustomerState state;

        switch (stateType)
        {
            //Заходит в кафе и ждет очередь на кассе
            case CustomerStateType.JoinCafe:
                state = new JoinCafeCustomerState(stateType, parent);
                break;
            //Ждет когда его обслужат на кассе
            case CustomerStateType.WaitTakingOrder:
                state = new WaitTakingOrderState(stateType, parent);
                break;
                
            //Ждет очереди на выдаче
            case CustomerStateType.JoinDeliveryTable:
                state = new JoinDeliveryTableState(stateType, parent);
                break;
            //Ждет когда его обслужат на выдаче заказа
            case CustomerStateType.WaitReceiptOrder:
                state = new WaitReceiptOrderState(stateType, parent);
                break;

            //Уходит из кафе
            case CustomerStateType.LeaveCafe:
                state = new LeaveState(stateType, parent);
                break;

            default:
                Debug.LogErrorFormat("Нет switch-case для типа \"{0}\" ", stateType);
                state = new LeaveState(stateType, parent);
                break;
        }

        _container.Inject(state);
        return state;
    }
}