using UnityEngine;

namespace PintRush
{
    public class GlassSpawner : MonoBehaviour
    {
        [SerializeField] private GameManagement gameManagement;
        [SerializeField] private AudioManager audioManager;

        [SerializeField] GameObject parent;

        [SerializeField] private Vector3 firstSpawn;
        [SerializeField] private Vector3 secondSpawn;
        [SerializeField] private Vector3 thirdSpawn;

        [SerializeField] private GameObject g1Prefab;
        [SerializeField] private GameObject g2Prefab;
        [SerializeField] private GameObject g3Prefab;

        private int upgrades;
        //TEMPORARY
        [SerializeField] bool upgraded;

        private int currentGlasses;
        private int maxGlasses;


        private void Start()
        {
            currentGlasses = 0;
            upgrades = 0;
            maxGlasses = gameManagement.GetMaxGlasses();
        }

        private void Update()
        {
            // Gets maxGlass from "master"
            // Easy to modify
            maxGlasses = gameManagement.GetMaxGlasses();

            //TEMPORARY
            if(upgraded)
            {
                upgraded = false;
                UpgradePourSpeed();
            }
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
                Debug.Log($"Spawnpoint: {firstSpawn}");
                Debug.Log($"Actual spawnpoint: {firstGlass.transform.position}");

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

        public void UpgradePourSpeed()
        {
            upgrades++;
            Debug.Log($"Tap speed upgrade: {upgrades}");
        }
    }
}
