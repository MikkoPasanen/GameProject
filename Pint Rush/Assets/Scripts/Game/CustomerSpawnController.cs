using UnityEngine;

namespace PintRush
{
    public class CustomerSpawnController : MonoBehaviour
    {
        [SerializeField] private GameObject customerPrefad;
        [SerializeField] private GameObject customers;
        [SerializeField] private int customerPatience;

        public void SpawnCustomer()
        {
            Debug.Log("Customer spawned!");
            GameObject customer = Instantiate(customerPrefad, customers.transform.position, Quaternion.identity);
        }

        public int GetCustomerPatience()
        {
            return customerPatience;
        }
    }
}
