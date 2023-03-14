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
        private float timer = 0f;
        [SerializeField] float spawnRate;
        [SerializeField] private Vector2 direction = Vector2.zero;
        [SerializeField] private float speed = 1;
        private Vector2 startPos;
        private Vector2 currentPos;
        [SerializeField] private Vector2 target;
        [SerializeField] CustomerController cc;

        // Currently only 1 customer can be spawned at the same time! TO BE FIXED!

        private void Start()
        {
            SpawnCustomer();
            startPos = transform.position;
            direction = direction.normalized;
        }
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

            Vector2 movement = direction * speed * Time.deltaTime;
            currentPos = transform.position;

            if(currentPos == target)
            {
                cc.StartHappiness();
            }
        }
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
