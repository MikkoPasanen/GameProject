using UnityEngine;
using TMPro;

namespace PintRush
{
    public class Upgrades : MonoBehaviour
    {
        private int tapUpgraded;
        private int glassesUpgraded;

        private int maxTap = 3;
        private int maxGlasses = 3;

        private int tapCurrentCost;
        [SerializeField] private TextMeshProUGUI tapCostText;
        [SerializeField] private int[] tapCosts = new int[4];
        [SerializeField] private GameObject[] tapStars = new GameObject[3];

        private int glassCurrentCost;
        [SerializeField] private TextMeshProUGUI glassCostText;
        [SerializeField] private int[] glassCosts = new int[4];
        [SerializeField] private GameObject[] glassStars = new GameObject[3];



        private void Start()
        {
            tapUpgraded = 0;
            glassesUpgraded = 0;

            tapCurrentCost = tapCosts[1];
            glassCurrentCost = glassCosts[1];
        }

        private void Update()
        {
            if(tapUpgraded == maxTap)
            {
                tapCostText.text = "MAX";
            }
            else
            {
                tapCostText.text = $"{tapCurrentCost}";
            }

            if(glassesUpgraded == maxGlasses)
            {
                glassCostText.text = "MAX";
            }
            else
            {
                glassCostText.text = $"{glassCurrentCost}";
            }

            switch(tapUpgraded)
            {
                case 0:
                    break;
                case 1:
                    tapStars[0].SetActive(true);
                    break;
                case 2:
                    tapStars[1].SetActive(true);
                    break;
                case 3:
                    tapStars[2].SetActive(true);
                    break;
                default:
                    break;
            }

            switch (glassesUpgraded)
            {
                case 0:
                    break;
                case 1:
                    glassStars[0].SetActive(true);
                    break;
                case 2:
                    glassStars[1].SetActive(true);
                    break;
                case 3:
                    glassStars[2].SetActive(true);
                    break;
                default:
                    break;
            }
        }

        public void UpgradeTap()
        {
            if (tapUpgraded < maxTap) 
            { 
                tapUpgraded++;
            }
            if (tapUpgraded < tapCosts.Length-1)
            {
                tapCurrentCost = tapCosts[tapUpgraded+1];
            }
            Debug.Log($"Tap upgrades: {tapUpgraded} / {maxTap}");
            
        }

        public void UpgradeGlasses()
        {
            if (glassesUpgraded < maxGlasses) 
            { 
                glassesUpgraded++; 
            }
            if (glassesUpgraded < tapCosts.Length-1)
            {
                glassCurrentCost = glassCosts[glassesUpgraded+1];
            }
            Debug.Log($"Glass upgrades: {glassesUpgraded} / {maxGlasses}");
        }

        public void OpenUpgradeWindow()
        {
            gameObject.SetActive(true);
            Time.timeScale = 0f;
        }

        public void CloseUpgradeWindow()
        {
            gameObject.SetActive(false);
            Time.timeScale = 1f;
        }
    }
}
