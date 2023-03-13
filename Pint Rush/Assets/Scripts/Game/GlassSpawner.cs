using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PintRush
{
    public class GlassSpawner : MonoBehaviour
    {
        [SerializeField] private int maxGlasses = 3;
        private int currentGlasses = 0;
        [SerializeField] GameObject parent;
        GlassSpawnInfo glassSpawnInfo;

        //Get the info for the glasses from another script
        private void Awake()
        {
            glassSpawnInfo = GetComponent<GlassSpawnInfo>();
        }

        //Instatiates the glass prefab in the location provided by the GlassSpawnInfo script
        //Sets the glass prefab as the child of canvas
        //Adds +1 to the current glass counter every time a glass is spawned
        public void OnGlassOne()
        {
            if (currentGlasses < maxGlasses)
            {
                Debug.Log("Glass 1 spawned at: " + glassSpawnInfo.glassOneSpawn);
                GameObject firstGlass = Instantiate(glassSpawnInfo.glassOnePrefab, glassSpawnInfo.glassOneSpawn, Quaternion.identity);
                firstGlass.transform.SetParent(parent.transform, false);
                currentGlasses++;
            }
        }

        public void OnGlassTwo()
        {
            if (currentGlasses < maxGlasses)
            {
                Debug.Log("Glass 2 spawned at: " + glassSpawnInfo.glassTwoSpawn);
                GameObject secondGlass = Instantiate(glassSpawnInfo.glassTwoPrefab, glassSpawnInfo.glassTwoSpawn , Quaternion.identity);
                secondGlass.transform.SetParent(parent.transform, false);
                currentGlasses++;
            }
        }

        public void OnGlassThree()
        {
            if (currentGlasses < maxGlasses)
            {
                Debug.Log("Glass 3 spawned at: " + glassSpawnInfo.glassThreeSpawn);
                GameObject thirdGlass = Instantiate(glassSpawnInfo.glassThreePrefab, glassSpawnInfo.glassThreeSpawn, Quaternion.identity);
                thirdGlass.transform.SetParent(parent.transform, false);
                currentGlasses++;
            }
        }
    }
}
