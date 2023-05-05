using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEditor;

namespace PintRush
{
    public class GameManagement : MonoBehaviour
    {
        private int maxGlasses;
        [SerializeField] private GameObject gameOverScreen;
        [SerializeField] private GameObject howToPlay;

        [SerializeField] TextMeshProUGUI scoreText;
        [SerializeField] TextMeshProUGUI upgradePointsText;
        [SerializeField] TextMeshProUGUI totalPointText;
        

        [SerializeField] private Life life;
        [SerializeField] private CustomerSpawnController csc;
        [SerializeField] private AudioManager audioManager;
        [SerializeField] private Settings settings;

        [SerializeField] private int developerPoints;
        [SerializeField] private bool allowSpawn;

        public int upgradePoints;
        public int totalPoints;
        private int beerOne;
        private int beerTwo;
        private int beerThree;

        private int highScore;
        private int savedHighscore;

        //Playerprefs
        private const string playerPrefKey = "HowToPlay";
        private const string highscoreKey = "HIGHSCORE";
        private bool defaultValue = true;

        [SerializeField] private GameObject checkMark;
        [SerializeField] private GameObject checkMarkTwo;

        private void Start()
        {
            bool showHowToPlay = PlayerPrefs.GetInt(playerPrefKey, defaultValue ? 1 : 0) == 1;
            defaultValue = showHowToPlay;
            highScore = PlayerPrefs.HasKey(highscoreKey) ? PlayerPrefs.GetInt(highscoreKey) : 0;

            gameOverScreen.SetActive(false);
            beerOne = 0;

            // DEVELOPER MODE
            if(settings.CheckDeveloperMode())
            {
                upgradePoints = developerPoints;
            }
            else
            {
                upgradePoints = 0;
            }

            beerTwo = 0;
            beerThree = 0;
            maxGlasses = 1;

            scoreText.text = $"{upgradePoints}";
            upgradePointsText.text = $"{upgradePoints}";
            totalPointText.text = $"{totalPoints}";
                
            if(showHowToPlay)
            {
                howToPlay.SetActive(true);
                checkMarkTwo.SetActive(true);

            }
            else
            {
                csc.StartSpawn();
                checkMarkTwo.SetActive(false);
            }
        }

        public void SetGameOver()
        {
            Debug.Log("Game Over!");
            gameOverScreen.SetActive(true);
            Time.timeScale = 0;

            savedHighscore = PlayerPrefs.HasKey(highscoreKey) ? PlayerPrefs.GetInt(highscoreKey) : 0;
            if(totalPoints > savedHighscore)
            {
                PlayerPrefs.SetInt(highscoreKey, totalPoints);
                Debug.Log($"New high score: {totalPoints}");
            }
        }
        public void RestartGame()
        {
            SceneManager.LoadScene(1);
            Time.timeScale = 1;
        }

        // Point methods
        public void AddPoint()
        {
            upgradePoints++;
            totalPoints++;
            audioManager.PlayPointSound();
            Debug.Log("Added a point... Point: " + upgradePoints);
            scoreText.text = $"{upgradePoints}";
            upgradePointsText.text = $"{upgradePoints}";
            totalPointText.text = $"{totalPoints}";
        }
        
        public void SetPoints(int points)
        {
            this.upgradePoints -= points;
            scoreText.text = $"{this.upgradePoints}";
            upgradePointsText.text = $"{this.upgradePoints}";
            totalPointText.text = $"{this.totalPoints}";
        }

        public void AddMaxGlasses()
        {
            maxGlasses++;
        }

        public int GetPoints()
        {
            return this.upgradePoints;
        }

        public void AddBeerOne()
        {
            this.beerOne += 1;
        }

        public void AddBeerTwo()
        {
            this.beerTwo += 1;
        }

        public void AddBeerThree()
        {
            this.beerThree += 1;
        }

        // Life methods
        public void RemoveLife()
        {
            Debug.Log("Removing a life");
            life.LifeLost();
            if(life.GetALife())
            {
                StartCoroutine(WaitForGameOver(2));
            }
        }

        IEnumerator WaitForGameOver(int time)
        {
            yield return new WaitForSeconds(time);
            SetGameOver();
        }
        
        // Glass methods
        public int GetMaxGlasses()
        {
            return this.maxGlasses;
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

        public bool CheckAllowSpawn()
        {
            return allowSpawn;
        }

        public int GetTotalPoints()
        { 
            return totalPoints;
        }

        public void SetShowHowToPlay()
        {
            defaultValue = !defaultValue;
            PlayerPrefs.SetInt(playerPrefKey, defaultValue ? 1 : 0);
            PlayerPrefs.Save();

            if(defaultValue)
            {
                checkMark.SetActive(true);
                Debug.Log("SHOW DEM OHJEET");
                checkMarkTwo.SetActive(true);
            }
            else if(!defaultValue)
            {
                checkMark.SetActive(false);
                Debug.Log("DONT SHOW DEM OHJEET");
                checkMarkTwo.SetActive(false);
            }
        }

        public void ShowHowToPlay()
        {
            /*
            defaultValue = true;
            PlayerPrefs.SetInt(playerPrefKey, defaultValue ? 1 : 0);
            PlayerPrefs.Save();
            Debug.Log("show ohjeet ysdrtfd sw");
            checkMarkTwo.SetActive(true);
            */

            defaultValue = !defaultValue;
            PlayerPrefs.SetInt(playerPrefKey, defaultValue ? 1 : 0);
            PlayerPrefs.Save();

            if (defaultValue)
            {
                Debug.Log("SHOW DEM OHJEET");
                checkMarkTwo.SetActive(true);
            }
            else if (!defaultValue)
            {
                Debug.Log("DONT SHOW DEM OHJEET");
                checkMarkTwo.SetActive(false);
            }

        }
    }
}
