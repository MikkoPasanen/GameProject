using System;
using System.Collections;
using TMPro;
using Unity.VisualScripting;
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
        [SerializeField] private bool[] occupiedSpace;
        private bool allSpacesOccupied;
        private bool customerSpawned;

        private int spawnTimer = 0;
        private int gameTimer = 0;

        [SerializeField] int spawnRate;
        public static float timer;
        private int spawnRateThreshold = 0;
        private bool spawnStarted;

        private int customerCount;

        // Currently only 1 customer can be spawned at the same time! TO BE FIXED!

        private void Start()
        {
            gameManagement = gmObject.GetComponent<GameManagement>();
            occupiedSpace = new bool[3];
            Debug.Log("Endpoints length: "+endpointPositions.Length);
            timer = 0;
            spawnStarted = false;
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
                    if(customerCount < 3)
                    {
                        if (!customerSpawned)
                        {
                            spawnTimer = 0;
                            SpawnCustomer();
                        }
                    }
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

        public void StartSpawn()
        {
            StartCoroutine(WaitForFirstSpawn(3f));
        }

        //Spawns a random customer and then gives the customer a random endpoint where he will walk into
        public void SpawnCustomer()
        {
            // Because otherwise it would start another random generation
            // Now if there is a customer random being generated in the while loop
            // it doesn't start the process again.
            if (!customerSpawned) 
            {
              
                  customerSpawned = true;
                  //Debug.Log(allSpacesOccupied);
                  //Selects a random endpoint and a random customer prefab to spawn
                  random = UnityEngine.Random.Range(0, 3); // Select a random point for the customer to occupie
                  //Debug.Log($"Random {random}");
                  
                  while(occupiedSpace[random] == true)
                  {
                      random = UnityEngine.Random.Range(0, 3);
                  }
                  occupiedSpace[random] = true;

                  randomCustomer = UnityEngine.Random.Range(0, customerPrefabs.Length); //Select a random customer to spawn from an array (different sprite for the customer)
                  GameObject customer = Instantiate(customerPrefabs[randomCustomer], transform.position, Quaternion.identity);
                  AddCustomerCount();
                  CustomerController customerController = customer.GetComponent<CustomerController>();

                  Transform randomEndpointPosition = endpointPositions[random];

                  customerController.SetEndpoint(randomEndpointPosition, random);
                  customerController.SetExitEndpoint(exitEndPosition);
                  customer.transform.SetParent(customers.transform, false); 
            
            }
            customerSpawned = false;
        }

        public void AddCustomerCount()
        {
            customerCount++;
        }

        public void RemoveCustomerCount()
        {
            customerCount--;
        }

        public void CustomerLeftHappy(bool happy)
        {
            if (happy)
            {
                //Debug.Log("CUSTOMER LEFT: Happy!");
                gameManagement.AddPoint();
                spawnRateThreshold++;
            }
            else
            {
                //Debug.Log("CUSTOMER LEFT: Not happy!");
                gameManagement.RemoveLife();
            }
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


        public void SetOccupiedSpace(int space, bool occupied)
        {
            this.occupiedSpace[space] = occupied;
        }

        IEnumerator WaitForFirstSpawn(float time)
        {
            yield return new WaitForSeconds(time);
            SpawnCustomer();
            spawnStarted = true;
        }
    }
}
