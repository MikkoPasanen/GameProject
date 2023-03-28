using System;
using System.Collections;
using TMPro;
using UnityEngine;

namespace PintRush
{
    public class CustomerSpawnController : MonoBehaviour
    {
        [SerializeField] private GameObject customerPrefab;
        [SerializeField] private GameObject customers;
        [SerializeField] private int customerPatience;
        [SerializeField] private Transform[] endpointPositions;
        [SerializeField] private GameObject[] customerPrefabs;
        [SerializeField] private Transform exitEndPosition;
        [SerializeField] private GameObject gmObject;
        [SerializeField] private GlassSpawner gs;
        private GameManagement gameManagement;
        private int random;
        private int randomCustomer;
        private bool[] occupiedSpace;
        private bool customerSpawned;
        private int spawnTimer = 0;
        private int gameTimer = 0;
        [SerializeField] int spawnRate;
        public static float timer;
        private int spawnRateThreshold = 0;
        private bool spawnStarted;

        // Currently only 1 customer can be spawned at the same time! TO BE FIXED!

        private void Start()
        {
            gameManagement = gmObject.GetComponent<GameManagement>();
            occupiedSpace = new bool[endpointPositions.Length];
            Debug.Log("Endpoints length: "+endpointPositions.Length);
            timer = 0;
            spawnStarted = false;
            StartCoroutine(WaitForFirstSpawn(3f));
        }

        //Timer for the customer spawns
        void FixedUpdate()
        {
            gameTimer++;
            if (spawnStarted)
            {
                spawnTimer++;
                if (spawnTimer >= spawnRate)
                {
                    SpawnCustomer();
                    spawnTimer = 0;
                }
            }

            // Every 10 customers served spawn rate is increased
            if (spawnRateThreshold >= 10)
            {
                spawnRateThreshold = 0;
                spawnRate = spawnRate - 20;
            }
        }

        private void Update()
        {
            //Counts seconds
            timer += Time.deltaTime;

        }

        //Spawns a random customer and then gives the customer a random endpoint where he will walk into
        public void SpawnCustomer()
        {
            // Because otherwise it would start another random generation
            // Now if there is a customer random being generated in the while loop
            // it doesn't start the process again.
            if (!customerSpawned) 
            {
                if (!ScanOccupiedSpaces())
                {
                    //Selects a random endpoint and a random customer prefab to spawn
                    random = UnityEngine.Random.Range(0, endpointPositions.Length);
                    randomCustomer = UnityEngine.Random.Range(0, customerPrefabs.Length);
                    while (occupiedSpace[random] == true)
                    {
                        random = UnityEngine.Random.Range(0, endpointPositions.Length);
                        customerSpawned = true;
                    }
                    GameObject customer = Instantiate(customerPrefabs[randomCustomer], transform.position, Quaternion.identity);
                    CustomerController customerController = customer.GetComponent<CustomerController>();

                    Transform randomEndpointPosition = endpointPositions[random];

                    customerController.SetEndpoint(randomEndpointPosition, random);
                    customerController.SetExitEndpoint(exitEndPosition);
                    occupiedSpace[random] = true;
                    customer.transform.SetParent(customers.transform, false);
                }
            }
            customerSpawned = false;
        }

        public void AddBeerOne()
        {
            gameManagement.AddBeerOne();
        }

        public void AddBeerTwo()
        {
            gameManagement.AddBeerTwo();
        }

        public void AddBeerThree()
        {
            gameManagement.AddBeerThree();
        }

        public void RemoveGlass()
        {
            gs.RemoveGlass();
        }

        public void CustomerLeftHappy(bool happy)
        {
            if (happy)
            {
                Debug.Log("CUSTOMER LEFT: Happy!");
                gameManagement.AddPoint();
                spawnRateThreshold++;
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

        IEnumerator WaitForFirstSpawn(float time)
        {
            yield return new WaitForSeconds(time);
            SpawnCustomer();
            spawnStarted = true;
            Debug.Log("Spawning started!");
        }
    }
}
