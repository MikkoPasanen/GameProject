using UnityEngine;

namespace PintRush
{
    public class CustomerSpawnController : MonoBehaviour
    {
        [SerializeField] private GameObject customerPrefab;
        [SerializeField] private GameObject customers;
        [SerializeField] private int customerPatience;
        [SerializeField] private Transform[] endpointPositions;
        private float random;
        private int randomCustomer;
        private bool customerSpawned = false;
        private float timer = 0f;
        [SerializeField] float spawnRate;
        [SerializeField] CustomerController cc;

        // Currently only 1 customer can be spawned at the same time! TO BE FIXED!

        private void Start()
        {
            SpawnCustomer();
        }

        //Timer for the customer spawns
        void Update()
        {
            if (timer < spawnRate)
            {
                timer += Time.deltaTime;
            }
            else
            {
                SpawnCustomer();
                timer = 0;
            }
        }

        //Spawns a random customer and then gives the customer a random endpoint where he will walk into
        public void SpawnCustomer()
        {
            if (!customerSpawned)
            {
                random = Random.Range(1, 3);
                randomCustomer = (int)random;
                Debug.Log($"Random customer: {randomCustomer}");
                cc.ChooseRandomBeer();

                GameObject customer = Instantiate(customerPrefab, transform.position, Quaternion.identity);
                CustomerController customerController = customer.GetComponent<CustomerController>();
                
                Transform randomEndpointPosition = endpointPositions[Random.Range(0, endpointPositions.Length)];

                customerController.SetEndpoint(randomEndpointPosition);
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
