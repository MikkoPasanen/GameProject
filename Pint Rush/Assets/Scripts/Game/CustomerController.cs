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

        private Vector2 startPos;
        private Vector2 currentPos;
        [SerializeField] private Vector2 direction = Vector2.zero;
        [SerializeField] private Vector2 targetPos;
        [SerializeField] private float speed;

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
            startPos = transform.position;
            direction = direction.normalized;
        }

        private void Update()
        {
            Vector2 movement = direction * speed * Time.deltaTime;
            currentPos = transform.position;

            if(currentPos.x >= targetPos.x)
            {
                happinessTimerActive = true;
            }
            if(currentPos.x <= targetPos.x)
            {
                transform.Translate(movement);
            }
        }

        private void FixedUpdate()
        {
            if (happinessTimerActive) {
                happinessTimer++;
                // Second state
                if (happinessTimer > patience)
                {
                    happinessStateOne.SetActive(true);
                    happinessStateTwo.SetActive(false);
                    happinessStateThree.SetActive(false);
                    happinessStateFour.SetActive(false);
                    happinessStateFive.SetActive(false);
                }
                // Third state
                if (happinessTimer > patience * 2)
                {
                    happinessStateOne.SetActive(false);
                    happinessStateTwo.SetActive(true);
                    happinessStateThree.SetActive(false);
                    happinessStateFour.SetActive(false);
                    happinessStateFive.SetActive(false);
                }
                // Fourth state
                if (happinessTimer > patience * 3)
                {
                    happinessStateOne.SetActive(false);
                    happinessStateTwo.SetActive(false);
                    happinessStateThree.SetActive(true);
                    happinessStateFour.SetActive(false);
                    happinessStateFive.SetActive(false);
                }
                // Fifth state
                if (happinessTimer > patience * 4)
                {
                    happinessStateOne.SetActive(false);
                    happinessStateTwo.SetActive(false);
                    happinessStateThree.SetActive(false);
                    happinessStateFour.SetActive(true);
                    happinessStateFive.SetActive(false);
                }
                if(happinessTimer > patience * 5)
                {
                    happinessStateOne.SetActive(false);
                    happinessStateTwo.SetActive(false);
                    happinessStateThree.SetActive(false);
                    happinessStateFour.SetActive(false);
                    happinessStateFive.SetActive(true);
                }
                if (happinessTimer > patience * 6)
                {
                    Destroy(gameObject);
                    transform.parent.GetComponent<CustomerSpawnController>().SetCustomerSpawned(false);
                }
            }
        }

        public void StartHappiness()
        {
            happinessTimerActive = true;
        }
    }
}
