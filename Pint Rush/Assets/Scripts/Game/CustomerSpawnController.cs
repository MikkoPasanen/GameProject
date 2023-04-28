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

        [SerializeField] private Transform[] endpointPositions;
        [SerializeField] private GameObject customerPre;

        [SerializeField] private Transform exitEndPosition;
        [SerializeField] private GameObject gmObject;
        [SerializeField] private GlassSpawner gs;
        private GameManagement gameManagement;
        private int random;
        private int randomCustomer;
        [SerializeField] private bool[] occupiedSpace;
        private bool allSpacesOccupied;
        private bool customerSpawned;
        private bool doContinue;
        private bool stageTwoEnabled;

        private int spawnTimer = 0;
        private int gameTimer = 0;

        [SerializeField] private int spawnRate;
        [SerializeField] private int spawnRateUpdateFrequency;
        [SerializeField] private int spawnRateSubstractAmount;

        [SerializeField] private int patience;
        [SerializeField] private int patienceUpdateFrequency;
        [SerializeField] private int patienceSubstractAmount;

        public static float timer;
        private int successfulServings = 0;
        private bool spawnStarted;

        private void Start()
        {
            gameManagement = gmObject.GetComponent<GameManagement>();
            Debug.Log("Endpoints length: "+endpointPositions.Length);
            timer = 0;
            spawnStarted = false;
            doContinue = false;
            stageTwoEnabled = false;
        }

        //Timer for the customer spawns
        void FixedUpdate()
        {
            gameTimer++;

            if (spawnStarted && gameManagement.CheckAllowSpawn())
            {
                spawnTimer++;

                if (spawnTimer >= spawnRate)
                {
                    spawnTimer = 0;
                    SpawnCustomer();
                }
            }

            if(spawnRate >= 100)
            {
                if (successfulServings >= spawnRateUpdateFrequency)
                {
                    successfulServings = 0;
                    spawnRate -= spawnRateSubstractAmount;
                }
            }
            else
            {
                stageTwoEnabled = true;
            }

            if(stageTwoEnabled && patience >= 100)
            {
                if(successfulServings >= patienceUpdateFrequency)
                {
                    successfulServings = 0;
                    patience -= patienceSubstractAmount;
                }
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
            if (!customerSpawned) 
            {
                customerSpawned = true;
                doContinue = false;
                random = UnityEngine.Random.Range(0, 3);
                  
                if(occupiedSpace[random])
                {
                    for(int i = 0; i < occupiedSpace.Length; i++)
                    {
                        if(!occupiedSpace[i])
                        {
                            doContinue = true;
                            random = i;
                            break;
                        }
                    }
                }
                else
                {
                    doContinue = true;
                }

                if(doContinue)
                {
                    occupiedSpace[random] = true;

                    GameObject customer = Instantiate(customerPre, transform.position, Quaternion.identity);
                    Customer customerController = customer.GetComponent<Customer>();

                    Transform randomEndpointPosition = endpointPositions[random];

                    customerController.SetEndpoint(randomEndpointPosition, random);
                    customerController.SetExitEndpoint(exitEndPosition);
                    customerController.SetPatience(patience);
                    customer.transform.SetParent(customers.transform, false);
                }
            
            }
            customerSpawned = false;
        }

        public void CustomerLeftHappy(bool happy)
        {
            if (happy)
            {
                gameManagement.AddPoint();
                successfulServings++;
            }
            else
            {
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
