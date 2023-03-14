using UnityEngine;

namespace PintRush
{
    public class CustomerController : MonoBehaviour
    {
        [SerializeField] private GameObject happinessStateOne;
        [SerializeField] private GameObject happinessStateTwo;
        [SerializeField] private GameObject happinessStateThree;
        [SerializeField] private GameObject happinessStateFour;
        [SerializeField] private GameObject happinessStateFive;

        [SerializeField] private int patience;
        [SerializeField] BoxCollider2D bc2d;
        [SerializeField] GameManagement gm;

        private Vector2 currentPos;
        [SerializeField] private Vector2 direction = Vector2.zero;
        //[SerializeField] private Vector2 targetPos;
        [SerializeField] private float speed;
        private Transform endpointPosition;

        private int happinessTimer;
        private bool happinessTimerActive = false;


        private void Awake()
        {
            happinessStateOne.SetActive(false);
            happinessStateTwo.SetActive(false);
            happinessStateThree.SetActive(false);
            happinessStateFour.SetActive(false);
            happinessStateFive.SetActive(false);
        }

        private void Start()
        {
            direction = direction.normalized;
        }

        public void SetEndpoint(Transform endpointPosition)
        {
            this.endpointPosition = endpointPosition;
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
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if(collision.gameObject.tag == "Glass")
            {
                GlassController glass = collision.gameObject.GetComponent<GlassController>();
                if(glass.GetFill())
                {
                    Debug.Log("Customer served!");
                    gm.RemoveGlass();
                    Destroy(gameObject);
                    Destroy(collision.gameObject);
                    transform.parent.GetComponent<CustomerSpawnController>().SetCustomerSpawned(false);
                    glass.SetFill(false);
                    Debug.Log($"Glasses: {gm.GetCurrentGlasses()}");
                }
            }
        }

        public void StartHappiness()
        {
            happinessTimerActive = true;
        }
    }
}
