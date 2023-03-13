using UnityEngine;

namespace PintRush
{
    public class CustomerSpawnController : MonoBehaviour
    {
        [SerializeField] private GameObject customerPrefad;
        [SerializeField] private GameObject customers;
        [SerializeField] private int customerPatience;
        private float random;
        private int randomCustomer;
        private bool customerSpawned = false;

        // Currently only 1 customer can be spawned at the same time! TO BE FIXED!
        public void SpawnCustomer()
        {
            if (!customerSpawned)
            {
                random = Random.Range(1, 3);
                randomCustomer = (int)random;
                Debug.Log($"Random: {randomCustomer}");
                Debug.Log("Customer spawned!");
                GameObject customer = Instantiate(customerPrefad, customers.transform.position, Quaternion.identity);
                customer.transform.SetParent(customers.transform, false);
                customerSpawned = true;
            } 
            else
            {
                Debug.Log("Customer limit reached!");
            }
        }

        public int GetCustomerPatience()
        {
            return customerPatience;
        }

        public void SetCustomerSpawned(bool customerSpawned)
        {
            this.customerSpawned = customerSpawned;
        }
    }
}
