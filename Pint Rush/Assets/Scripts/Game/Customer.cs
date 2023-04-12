using Unity.VisualScripting;
using UnityEngine;

namespace PintRush
{
    public class Customer : MonoBehaviour
    {
        //Patience 
        [SerializeField] private GameObject happinessStateOne;
        [SerializeField] private GameObject happinessStateTwo;
        [SerializeField] private GameObject happinessStateThree;
        [SerializeField] private GameObject happinessStateFour;
        [SerializeField] private GameObject happinessStateFive;

        //Beer choices
        [SerializeField] private GameObject chosenBeerOne;
        [SerializeField] private GameObject chosenBeerTwo;
        [SerializeField] private GameObject chosenBeerThree;

        //Clothing
        private int head;
        private int hair;
        private int shirt;
        private int pants;
        [SerializeField] private GameObject[] headSprites;
        [SerializeField] private GameObject[] hairSprites;
        [SerializeField] private GameObject[] shirtSprites;
        [SerializeField] private GameObject[] pantsSprites;

        //Think bubble that showcases the patience and beer choice
        [SerializeField] private GameObject thinkBubble;

        //List for the possible beer choices
        [SerializeField] private GameObject[] beerChoices;
        private GameObject chosenBeer; 

        [SerializeField] private int patience;
        [SerializeField] BoxCollider2D bc2d;

        private Vector2 currentPos;
        [SerializeField] private Vector2 direction = Vector2.zero;
        [SerializeField] private float enterSpeed;
        [SerializeField] private float exitSpeed;
        [SerializeField] private Transform enterEndPosition;
        private Transform exitEndpoint;

        private int happinessTimer;
        private bool happinessTimerActive = false;
        //private bool removedLife = false;
        private bool exit = false;
        private bool exited = false;
        private bool happy = false;

        private bool beerDecided = false;
        string chosenBeerName;

        private CustomerSpawnController csc;
        private int occupiedSpace;
        private bool left = false;

        private void Awake()
        {
            happinessStateOne.SetActive(false);
            happinessStateTwo.SetActive(false);
            happinessStateThree.SetActive(false);
            happinessStateFour.SetActive(false);
            happinessStateFive.SetActive(false);
            chosenBeerOne.SetActive(false);
            chosenBeerTwo.SetActive(false);
            chosenBeerThree.SetActive(false);
            thinkBubble.SetActive(false);
        }

        private void Start()
        {
            direction = direction.normalized;
            SelectClothes();
        }

        public void ChooseRandomBeer()
        {
            int randomIndex = Random.Range(0, beerChoices.Length);
            chosenBeer = beerChoices[randomIndex];
            chosenBeerName = chosenBeer.name;
            switch (randomIndex)
            {
                case 0:
                    chosenBeerOne.SetActive(true);
                    break;
                case 1:
                    chosenBeerTwo.SetActive(true);
                    break;
                case 2:
                    chosenBeerThree.SetActive(true);
                    break;
            }
            beerDecided = true;
        }

        private void Update()
        {
            Vector2 enterMovement = direction * enterSpeed * Time.deltaTime;
            Vector2 exitMovement = direction * exitSpeed * Time.deltaTime;
            currentPos = transform.position;

            // Customer at the bar counter
            if(currentPos.x >= enterEndPosition.position.x)
            {
                happinessTimerActive = true;
                bc2d.enabled = true;

                // Setting layer to be displayed on top
                ChangeCustomerLayer("CustomerTwo");
            }

            // Customer arriving
            if(currentPos.x <= enterEndPosition.position.x)
            {
                transform.Translate(enterMovement);
                bc2d.enabled = false;

                ChangeCustomerLayer("Customer");
            }

            // Customer leaving
            if(exit && currentPos.x <= exitEndpoint.position.x)
            {
                transform.Translate(exitMovement);
                FreeSpace();
                transform.parent.GetComponent<CustomerSpawnController>().RemoveCustomerCount();
                bc2d.enabled = false;

                // Setting layer back
                ChangeCustomerLayer("Customer");

                if (happy)
                {
                    if(!exited)
                    {
                        exited = true;
                        transform.parent.GetComponent<CustomerSpawnController>().CustomerLeftHappy(true);
                    }
                    happinessStateOne.SetActive(true);
                    happinessStateTwo.SetActive(false);
                    happinessStateThree.SetActive(false);
                    happinessStateFour.SetActive(false);
                    happinessStateFive.SetActive(false);
                }
                else
                {
                    if(!exited)
                    {
                        exited = true;
                        transform.parent.GetComponent<CustomerSpawnController>().CustomerLeftHappy(false);
                    }
                    happinessStateOne.SetActive(false);
                    happinessStateTwo.SetActive(false);
                    happinessStateThree.SetActive(false);
                    happinessStateFour.SetActive(false);
                    happinessStateFive.SetActive(true);
                }
            }
            if(exit && currentPos.x >= exitEndpoint.position.x)
            {
                Destroy(gameObject);
                
            }
        }

        private void FixedUpdate()
        {
            if (happinessTimerActive && !exit) {
                thinkBubble.SetActive(true);
                if (!beerDecided) { ChooseRandomBeer(); }
                happinessTimer++;
                happinessStateOne.SetActive(true);
                happinessStateTwo.SetActive(false);
                happinessStateThree.SetActive(false);
                happinessStateFour.SetActive(false);
                happinessStateFive.SetActive(false);
                
                if (happinessTimer > patience)
                {
                    happinessStateOne.SetActive(false);
                    happinessStateTwo.SetActive(true);
                    happinessStateThree.SetActive(false);
                    happinessStateFour.SetActive(false);
                    happinessStateFive.SetActive(false);
                }
                if (happinessTimer > patience * 2)
                {
                    happinessStateOne.SetActive(false);
                    happinessStateTwo.SetActive(false);
                    happinessStateThree.SetActive(true);
                    happinessStateFour.SetActive(false);
                    happinessStateFive.SetActive(false);
                }
                if (happinessTimer > patience * 3)
                {
                    happinessStateOne.SetActive(false);
                    happinessStateTwo.SetActive(false);
                    happinessStateThree.SetActive(false);
                    happinessStateFour.SetActive(true);
                    happinessStateFive.SetActive(false);
                }
                if(happinessTimer > patience * 4)
                {
                    happinessStateOne.SetActive(false);
                    happinessStateTwo.SetActive(false);
                    happinessStateThree.SetActive(false);
                    happinessStateFour.SetActive(false);
                    happinessStateFive.SetActive(true);
                }

                //If the customer has waited long enough, he will disappear
                if (happinessTimer > patience * 5)
                {
                    happy = false;
                    exit = true;
                }
            }
        }
        public string GetBeerName()
        {
            return chosenBeerName;
        }

        public void SetExiting(bool exit, bool happy)
        {
            this.exit = exit;
            this.happy = happy;
        }

        //Set the endpoint that the customer will walk into
        public void SetEndpoint(Transform enterEndPosition, int occupiedSpace)
        {
            this.enterEndPosition = enterEndPosition;
            this.occupiedSpace = occupiedSpace;
        }
        public void SetExitEndpoint(Transform exitEndpoint)
        {
            this.exitEndpoint = exitEndpoint;
        }

        public void FreeSpace()
        {
            if(!left)
            {
                transform.parent.GetComponent<CustomerSpawnController>().SetOccupiedSpace(occupiedSpace, false);
                left = true;
            }
        }

        public void SelectClothes()
        {
            head = Random.Range(0, headSprites.Length);
            hair = Random.Range(0, hairSprites.Length);
            shirt = Random.Range(0, shirtSprites.Length);
            pants = Random.Range(0, pantsSprites.Length);

            headSprites[head].SetActive(true);
            hairSprites[hair].SetActive(true);
            shirtSprites[shirt].SetActive(true);
            pantsSprites[pants].SetActive(true);
        }

        private void ChangeCustomerLayer(string sortingLayer)
        {
            SpriteRenderer[] spritesInChildren = gameObject.GetComponentsInChildren<SpriteRenderer>();
            foreach (SpriteRenderer sprite in spritesInChildren)
            {
                if(sprite.sortingLayerName == "Customer" || sprite.sortingLayerName == "CustomerTwo")
                {
                    sprite.sortingLayerName = sortingLayer;
                }
            }
        }
    }
}
