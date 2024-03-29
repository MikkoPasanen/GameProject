using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Unity.VisualScripting;

namespace PintRush
{
    public class Upgrades : MonoBehaviour
    {
        private int tapUpgraded;
        private int glassesUpgraded;
        private int timesUpgraded;

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

        [SerializeField] private int[] upgradeCosts;

        [SerializeField] private GameObject[] glassCoasters = new GameObject[3];

        [SerializeField] private GameManagement gm;
        [SerializeField] private AudioManager audioManager;
        [SerializeField] private Settings settings;

        [SerializeField] private BeerTap beerTapOne;
        [SerializeField] private BeerTap beerTapTwo;
        [SerializeField] private BeerTap beerTapThree;

        [SerializeField] private Image upgradeTapImage;

        [SerializeField] private GameObject[] upgradeGlassImages;

        [SerializeField] private GameObject[] locks;


        private void Start()
        {
            tapUpgraded = 0;
            glassesUpgraded = 0;
            timesUpgraded = 0;

            tapCurrentCost = upgradeCosts[0];
            glassCurrentCost = upgradeCosts[0];

            upgradeGlassImages[0].SetActive(false);
            upgradeGlassImages[1].SetActive(false);
            upgradeGlassImages[2].SetActive(false);
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
            
            CheckTaps();
            CheckGlasses();
        }

        public void CheckTaps()
        {
            if (tapUpgraded == 0)
            {
                upgradeTapImage.sprite = beerTapOne.GetNextUpgradeSprite(1);
            }
            else if (tapUpgraded == 1)
            {
                tapStars[0].SetActive(true);
                beerTapOne.SetTapUp();
                beerTapTwo.SetTapUp();
                beerTapThree.SetTapUp();
                upgradeTapImage.sprite = beerTapOne.GetNextUpgradeSprite(2);
            }
            else if (tapUpgraded == 2)
            {
                tapStars[1].SetActive(true);
                beerTapOne.SetTapUp();
                beerTapTwo.SetTapUp();
                beerTapThree.SetTapUp();
                upgradeTapImage.sprite = beerTapOne.GetNextUpgradeSprite(3);
            }
            else if (tapUpgraded == 3)
            {
                tapStars[2].SetActive(true);
                beerTapOne.SetTapUp();
                beerTapTwo.SetTapUp();
                beerTapThree.SetTapUp();
            }
            else
            {
                Debug.Log($"TapUpgraded: {tapUpgraded} \n Something might have gone wrong");
            }
        }

        public void CheckGlasses()
        {
            if (glassesUpgraded == 0)
            {
                upgradeGlassImages[0].SetActive(true);
            }
            else if (glassesUpgraded == 1) 
            {
                glassStars[0].SetActive(true);
                glassCoasters[0].GetComponent<SpriteRenderer>().color = new UnityEngine.Color32(137, 137, 137, 100);
                locks[0].SetActive(false);
                upgradeGlassImages[1].SetActive(true);
            }
            else if (glassesUpgraded == 2)
            {
                glassStars[1].SetActive(true);
                glassCoasters[1].GetComponent<SpriteRenderer>().color = new UnityEngine.Color32(137, 137, 137, 100);
                locks[1].SetActive(false);
                upgradeGlassImages[2].SetActive(true);
            }
            else if (glassesUpgraded == 3)
            {
                glassStars[2].SetActive(true);
                glassCoasters[2].GetComponent<SpriteRenderer>().color = new UnityEngine.Color32(137, 137, 137, 100);
                locks[2].SetActive(false);
            }
        }

        public void UpgradeTap()
        {
            if (gm.GetPoints() >= tapCurrentCost) 
            {
                if (tapUpgraded < maxTap)
                {
                    tapUpgraded++;
                    timesUpgraded++;
                    gm.SetPoints(tapCurrentCost);
                    audioManager.PlayUpgradeSuccessSound();
                }
                else
                {
                    audioManager.PlayUpgradeFailedSound();
                    if(settings.GetVibrationState())
                    {
                        Handheld.Vibrate();
                    }
                }
                if (tapUpgraded < 3)
                {
                    tapCurrentCost = upgradeCosts[timesUpgraded];
                }
                if (glassesUpgraded < 3)
                {
                    glassCurrentCost = upgradeCosts[timesUpgraded];
                }
                Debug.Log($"Tap upgrades: {tapUpgraded} / {maxTap}");
            }
            else
            {
                audioManager.PlayUpgradeFailedSound();
                if(settings.GetVibrationState())
                {
                    Handheld.Vibrate();
                }
            }
        }

        public void UpgradeGlasses()
        {
            if(gm.GetPoints() >= glassCurrentCost)
            {
                if (glassesUpgraded < maxGlasses)
                {
                    glassesUpgraded++;
                    timesUpgraded++;
                    gm.SetPoints(glassCurrentCost);
                    gm.AddMaxGlasses();
                    audioManager.PlayUpgradeSuccessSound();

                }
                else
                {
                    audioManager.PlayUpgradeFailedSound();
                    if(settings.GetVibrationState())
                    {
                        Handheld.Vibrate();
                    }
                }
                if (tapUpgraded < 3)
                {
                    tapCurrentCost = upgradeCosts[timesUpgraded];
                }
                if (glassesUpgraded < 3)
                {
                    glassCurrentCost = upgradeCosts[timesUpgraded];
                }
                Debug.Log($"Glass upgrades: {glassesUpgraded} / {maxGlasses}");
            }
            else
            {
                audioManager.PlayUpgradeFailedSound();
                if(settings.GetVibrationState())
                {
                    Handheld.Vibrate();
                }
            }
        }

        public int GetTapUpgraded()
        {
            return tapUpgraded;
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
