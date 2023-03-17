using UnityEngine;

namespace PintRush
{
    public class CustomerController : MonoBehaviour
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

        //Think bubble that showcases the patience and beer choice
        [SerializeField] private GameObject thinkBubble;

        //List for the possible beer choices
        [SerializeField] private GameObject[] beerChoices;
        private GameObject chosenBeer; 

        [SerializeField] private int patience;
        [SerializeField] BoxCollider2D bc2d;
        [SerializeField] GameManagement gm;

        private Vector2 currentPos;
        [SerializeField] private Vector2 direction = Vector2.zero;
        [SerializeField] private float speed;
        private Transform endpointPosition;

        private int happinessTimer;
        private bool happinessTimerActive = false;

        private bool beerDecided = false;

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
        }

        //Set the endpoint that the customer will walk into
        public void SetEndpoint(Transform endpointPosition)
        {
            this.endpointPosition = endpointPosition;
        }

        public void ChooseRandomBeer()
        {
            int randomIndex = Random.Range(0, beerChoices.Length);
            chosenBeer = beerChoices[randomIndex];
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
            Debug.Log("Chosen beer: " + chosenBeer.name);
            beerDecided = true;
        }


        private void Update()
        {
            Vector2 movement = direction * speed * Time.deltaTime;
            currentPos = transform.position;

            if(currentPos.x >= endpointPosition.position.x)
            {
                happinessTimerActive = true;
                bc2d.enabled = true;
            }
            if(currentPos.x <= endpointPosition.position.x)
            {
                transform.Translate(movement);
                bc2d.enabled = false;
            }
        }

        private void FixedUpdate()
        {
            if (happinessTimerActive) {
                thinkBubble.SetActive(true);
                if (!beerDecided) { ChooseRandomBeer(); }
                happinessTimer++;

                happinessStateOne.SetActive(true);
                happinessStateTwo.SetActive(false);
                happinessStateThree.SetActive(false);
                happinessStateFour.SetActive(false);
                happinessStateFive.SetActive(false);
                
                // Second state
                if (happinessTimer > patience)
                {
                    happinessStateOne.SetActive(false);
                    happinessStateTwo.SetActive(true);
                    happinessStateThree.SetActive(false);
                    happinessStateFour.SetActive(false);
                    happinessStateFive.SetActive(false);
                }
                // Third state
                if (happinessTimer > patience * 2)
                {
                    happinessStateOne.SetActive(false);
                    happinessStateTwo.SetActive(false);
                    happinessStateThree.SetActive(true);
                    happinessStateFour.SetActive(false);
                    happinessStateFive.SetActive(false);
                }
                // Fourth state
                if (happinessTimer > patience * 3)
                {
                    happinessStateOne.SetActive(false);
                    happinessStateTwo.SetActive(false);
                    happinessStateThree.SetActive(false);
                    happinessStateFour.SetActive(true);
                    happinessStateFive.SetActive(false);
                }
                //Fifth state
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
                    Destroy(gameObject);
                    transform.parent.GetComponent<CustomerSpawnController>().SetCustomerSpawned(false);
                }
            }
        }

        //If the customer gets his drink that is full, he will disappear
        //Checks if the glass is filled and if the drink is the same as what the customer ordered
        /*private void OnTriggerEnter2D(Collider2D collision)
        {
            if(collision.gameObject.tag == "Glass")
            {
                GlassController glass = collision.gameObject.GetComponent<GlassController>();
                if (glass.GetFill())
                {
                    
                    // Get the customer's chosen beer name
                    string chosenBeerName = chosenBeer.name;

                    // Get the name of the glass that collided with the customer
                    string glassName = collision.gameObject.name;

                    // Check if the glass name contains the name of the customer's chosen beer and that the finger is up when giving the drink
                    if (glassName.Contains(chosenBeerName))
                    {
                        gm.RemoveGlass();
                        Destroy(gameObject);
                        Destroy(collision.gameObject);
                        transform.parent.GetComponent<CustomerSpawnController>().SetCustomerSpawned(false);
                        glass.SetFill(false);
                        glass.SetOnCustomer(false);
                    }
                }
            }
        }
        */

        public void GiveGlass()
        {

        }

        public void StartHappiness()
        {
            happinessTimerActive = true;
        }
    }
}
