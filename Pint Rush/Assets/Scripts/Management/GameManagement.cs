using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;


namespace PintRush
{
    public class GameManagement : MonoBehaviour
    {
        // Mute set by default to false!
        [SerializeField] private static bool isMuted = false;
        private int maxGlasses;
        [SerializeField] private GameObject gameOverScreen;
        [SerializeField] private GameObject howToPlay;

        [SerializeField] TextMeshProUGUI scoreText;
        [SerializeField] TextMeshProUGUI upgradePointsText;
        [SerializeField] TextMeshProUGUI totalPointText;

        [SerializeField] private Life life;

        [SerializeField] private bool developerMode;
        [SerializeField] private bool allowSpawn;

        public int upgradePoints;
        public int totalPoints;
        private int beerOne;
        private int beerTwo;
        private int beerThree;

        private void Start()
        {
            gameOverScreen.SetActive(false);
            beerOne = 0;

            // DEVELOPER MODE
            if(developerMode)
            {
                upgradePoints = 5000;
            }
            else
            {
                upgradePoints = 0;
            }

            beerTwo = 0;
            beerThree = 0;
            maxGlasses = 1;
            if(SceneManager.GetActiveScene().name == "Game")
            {
                scoreText.text = $"{upgradePoints}";
                upgradePointsText.text = $"{upgradePoints}";
                totalPointText.text = $"{totalPoints}";
            }
            howToPlay.SetActive(true);
        }

        public bool GetMuteState()
        {
            return isMuted;
        }

        public void SetMuteState(bool newIsMuted)
        {
            isMuted = newIsMuted;
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
            upgradePoints++;
            totalPoints++;
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
    }
}
