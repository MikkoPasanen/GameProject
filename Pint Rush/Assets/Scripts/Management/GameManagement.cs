using System.Collections;
using UnityEngine;
using UnityEngine.Localization.Settings;
using UnityEngine.SceneManagement;
using TMPro;


namespace PintRush
{
    public class GameManagement : MonoBehaviour
    {
        // Mute set by default to false!
        [SerializeField] private static bool isMuted = false;
        [SerializeField] private static string language = "fin";
        [SerializeField] private int currentGlasses;
        [SerializeField] private int maxGlasses;
        private bool active = false;
        [SerializeField] private GameObject gameOverScreen;
        private bool gameOver = false;


        [SerializeField] TextMeshProUGUI scoreText;

        [SerializeField] GameObject lifeBarOne;
        [SerializeField] GameObject lifeBarTwo;
        [SerializeField] GameObject lifeBarThree;

        // Glasses
        public Vector3 glassOneSpawn;
        public Vector3 glassTwoSpawn;
        public Vector3 glassThreeSpawn;
        public GameObject glassOnePrefab;
        public GameObject glassTwoPrefab;
        public GameObject glassThreePrefab;

        [SerializeField] private int maxLives = 3;
        private int currentLives;

        public int points;
        private int beerOne;
        private int beerTwo;
        private int beerThree;
        private void Start()
        {
            gameOverScreen.SetActive(false);
            currentLives = maxLives;
            beerOne = 0;
            points = 0;
            beerTwo = 0;
            beerThree = 0;
            if(SceneManager.GetActiveScene().name == "Game")
            {
                //scoreText.text = $"Served: {points}";
            }
            currentGlasses = 0;
            Debug.Log($"Start Currentlives: {currentLives}");
        }

        public bool GetMuteState()
        {
            return isMuted;
        }
        public string GetLanguage()
        {
            return language;
        }

        public void SetMuteState(bool newIsMuted)
        {
            isMuted = newIsMuted;
        }
        public void SetLanguage(string newLanguage)
        {
            language = newLanguage;
        }
        public void SetGameOver()
        {
            Debug.Log("Game Over!");
            gameOverScreen.SetActive(true);
            Time.timeScale = 0;
        }
        public void RestartGame()
        {
            SceneManager.LoadScene(1);
            Time.timeScale = 1;
        }

        // Point methods
        public void AddPoint()
        {
            points++;
            Debug.Log("Added a point... Point: " + points);
            scoreText.text = $"Served: {points}";
        }

        public void AddBeerOne()
        {
            this.beerOne =+ 1;
            Debug.Log("BeerOne: " + this.beerOne);
        }

        public void AddBeerTwo()
        {
            this.beerTwo = +1;
            Debug.Log("BeerTwo: " + this.beerTwo);
        }

        public void AddBeerThree()
        {
            this.beerThree = +1;
        }

        // Life methods
        public void RemoveLife()
        {
            currentLives -= 1;
            Debug.Log("Removed a life... Lives: " + currentLives);
            if (currentLives <= 0) { SetGameOver(); }
            switch (currentLives)
            {
                case 2:
                    lifeBarOne.GetComponent<Animator>().SetBool("PlayLifeLostAnimation", true);
                    break;
                case 1:
                    lifeBarTwo.GetComponent<Animator>().SetBool("PlayLifeLostAnimation", true);
                    break;
                case 0:
                    lifeBarThree.GetComponent<Animator>().SetBool("PlayLifeLostAnimation", true);
                    break;
                default:
                    Debug.Log("There might be an error!");
                    break;
            }
        }
        public int GetCurrentLives()
        {
            return this.currentLives;
        }
        
        // Glass methods
        public void RemoveGlass()
        {
            this.currentGlasses -= 1;
        }
        public void AddGlass()
        {
            this.currentGlasses += 1;
        }
        public int GetMaxGlasses()
        {
            return this.maxGlasses;
        }
        public int GetCurrentGlasses()
        {
            return this.currentGlasses;
        }

        public int GetBeerOneScore()
        {
            return this.beerOne;
        }

        public int GetBeerTwoScore()
        {
            return this.beerTwo;
        }

        public int GetBeerThreeScore()
        {
            return this.beerThree;
        }

        //Change the locale aka language and make it that it is not called more than once
        public void ChangeLocal(int localeID)
        {
            if(active == true)
            {
                return;
            }

            StartCoroutine(SetLocale(localeID));
        }

        //Select the locale aka language
        IEnumerator SetLocale(int _localeID)
        {
            active= true;
            yield return LocalizationSettings.InitializationOperation;
            LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[_localeID];
            active= false;
        }
    }
}
