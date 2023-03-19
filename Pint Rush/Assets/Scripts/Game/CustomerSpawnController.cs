using UnityEngine;

namespace PintRush
{
    public class CustomerSpawnController : MonoBehaviour
    {
        [SerializeField] private GameObject customerPrefab;
        [SerializeField] private GameObject customers;
        [SerializeField] private int customerPatience;
        [SerializeField] private Transform[] endpointPositions;
        [SerializeField] private Transform exitEndPosition;
        [SerializeField] private GameObject gmObject;
        private GameManagement gameManagement;
        private int random;
        private int randomCustomer;
        private bool[] occupiedSpace;
        private int timer = 0;
        [SerializeField] float spawnRate;

        // Currently only 1 customer can be spawned at the same time! TO BE FIXED!

        private void Start()
        {
            gameManagement = gmObject.GetComponent<GameManagement>();
            occupiedSpace = new bool[endpointPositions.Length];
            Debug.Log("Endpoints length: "+endpointPositions.Length);
            SpawnCustomer();
        }

        //Timer for the customer spawns
        void FixedUpdate()
        {
            timer++;
            if(timer >= spawnRate)
            {
                SpawnCustomer();
                timer = 0;
            }
        }

        //Spawns a random customer and then gives the customer a random endpoint where he will walk into
        public void SpawnCustomer()
        {

            if (!ScanOccupiedSpaces())
            {
                random = Random.Range(0, endpointPositions.Length);
                while (occupiedSpace[random] == true)
                {
                    random = Random.Range(0, endpointPositions.Length);
                }
                GameObject customer = Instantiate(customerPrefab, transform.position, Quaternion.identity);
                CustomerController customerController = customer.GetComponent<CustomerController>();

                Transform randomEndpointPosition = endpointPositions[random];

                customerController.SetEndpoint(randomEndpointPosition, random);
                customerController.SetExitEndpoint(exitEndPosition);
                occupiedSpace[random] = true;
                customer.transform.SetParent(customers.transform, false);
            }
        }

        public void CustomerLeftHappy(bool happy)
        {
            if (happy)
            {
                Debug.Log("CUSTOMER LEFT: Happy!");
                gameManagement.AddPoint();
            }
            else
            {
                Debug.Log("CUSTOMER LEFT: Not happy!");
                gameManagement.RemoveLife();
            }
        }

        public int GetCustomerPatience()
        {
            return customerPatience;
        }

        public void SetOccupiedSpace(int space, bool occupied)
        {
            this.occupiedSpace[space] = occupied;
        }
        public bool ScanOccupiedSpaces()
        {
            bool allOccupied = false;
            for(int i=0; i<occupiedSpace.Length; i++)
            {
                if(occupiedSpace[i] == true)
                {
                    allOccupied = true;
                }
                else
                {
                    allOccupied = false;
                    return allOccupied;
                }
            }
            return allOccupied;
        }
    }
}
