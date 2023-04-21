using UnityEngine;
using TMPro;

namespace PintRush
{
    public class GlassSpawner : MonoBehaviour
    {
        [SerializeField] private GameManagement gameManagement;
        [SerializeField] private AudioManager audioManager;
        [SerializeField] private Upgrades upgrades;

        [SerializeField] GameObject parent;

        [SerializeField] private Transform coasterOne;
        [SerializeField] private Transform coasterTwo;
        [SerializeField] private Transform coasterThree;

        [SerializeField] private float glassSpawnOffset;

        private Vector3 firstSpawn;
        private Vector3 secondSpawn;
        private Vector3 thirdSpawn;

        [SerializeField] private GameObject g1Prefab;
        [SerializeField] private GameObject g2Prefab;
        [SerializeField] private GameObject g3Prefab;

        [SerializeField] TextMeshProUGUI glassCounterText;

        [SerializeField] private float[] animationSpeeds = new float[4];
        private float currentAnimationSpeed;

        private int currentGlasses;
        private int maxGlasses;

        private void Start()
        {
            currentGlasses = 0;
            maxGlasses = gameManagement.GetMaxGlasses();
            currentAnimationSpeed = animationSpeeds[0];
            firstSpawn = new Vector3(coasterOne.position.x, coasterOne.position.y +glassSpawnOffset);
            secondSpawn = new Vector3(coasterTwo.position.x, coasterTwo.position.y +glassSpawnOffset);
            thirdSpawn = new Vector3(coasterThree.position.x, coasterThree.position.y +glassSpawnOffset);
        }

        private void Update()
        {
            // Gets maxGlass from "master"
            // Easy to modify
            maxGlasses = gameManagement.GetMaxGlasses();
            currentAnimationSpeed = animationSpeeds[upgrades.GetTapUpgraded()];
            glassCounterText.text = $"{currentGlasses} / {maxGlasses}";
        }

        // Spawns a glass prefab in a specified position
        // Used with UI buttons
        public void OnGlassOne()
        {
            Debug.Log($"{currentGlasses} : {maxGlasses}");
            if (currentGlasses < maxGlasses)
            {
                // Spawn a glass
                GameObject firstGlass = Instantiate(g1Prefab, firstSpawn, Quaternion.identity);
                firstGlass.transform.SetParent(parent.transform, false);
                firstGlass.GetComponent<Glass>().SetAnimatorSpeed(currentAnimationSpeed);

                // Setting type to beers
                firstGlass.GetComponent<Glass>().SetType("Lager");

                // Keep track of glass amount
                currentGlasses++;
            }
        }

        // Spawns a glass prefab in a specified position
        // Used with UI buttons
        public void OnGlassTwo()
        {
            Debug.Log($"{currentGlasses} : {maxGlasses}");
            if (currentGlasses < maxGlasses)
            {
                // Spawn a glass
                GameObject secondGlass = Instantiate(g2Prefab, secondSpawn, Quaternion.identity);
                secondGlass.transform.SetParent(parent.transform, false);
                secondGlass.GetComponent<Glass>().SetAnimatorSpeed(currentAnimationSpeed);
                Debug.Log($"Spawnpoint: {secondSpawn}");
                Debug.Log($"Actual spawnpoint: {secondGlass.transform.position}");


                // Setting type to beers
                secondGlass.GetComponent<Glass>().SetType("Stout");

                // Keep track of glass amount
                currentGlasses++;
            }
        }

        // Spawns a glass prefab in a specified position
        // Used with UI buttons
        public void OnGlassThree()
        {
            Debug.Log($"{currentGlasses} : {maxGlasses}");
            if (currentGlasses < maxGlasses)
            {
                // Spawn a glass
                GameObject thirdGlass = Instantiate(g3Prefab, thirdSpawn, Quaternion.identity);
                thirdGlass.transform.SetParent(parent.transform, false);
                thirdGlass.GetComponent<Glass>().SetAnimatorSpeed(currentAnimationSpeed);
                Debug.Log($"Spawnpoint: {thirdSpawn}");
                Debug.Log($"Actual spawnpoint: {thirdGlass.transform.position}");

                // Setting type to beers
                thirdGlass.GetComponent<Glass>().SetType("Mystery");

                // Keep track of glass amount
                currentGlasses++;
            }
        }

        public void RemoveGlass()
        {
            currentGlasses--;
        }
    }
}
