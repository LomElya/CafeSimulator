using UnityEngine;

public class CharacterReferences : MonoBehaviour
{
       [field: SerializeField] public ShopQueues ShopQueues { get; private set; }
       [field: SerializeField] public ShopQueues DeliveryQueues { get; private set; }
       [field: SerializeField] public CashDesk CashDesk { get; private set; }
       [field: SerializeField] public DeliveryTable DeliveryTable { get; private set; }
       [field: SerializeField] public Transform ExitPoint { get; private set; }
}
