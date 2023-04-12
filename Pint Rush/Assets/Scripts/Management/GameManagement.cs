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

        [SerializeField] private Life life;

        [SerializeField] private bool developerMode;

        public int points;
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
                points = 5000;
            }
            else
            {
                points = 0;
            }

            beerTwo = 0;
            beerThree = 0;
            maxGlasses = 1;
            if(SceneManager.GetActiveScene().name == "Game")
            {
                scoreText.text = $"{points}";
                upgradePointsText.text = $"{points}";
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
            points++;
            Debug.Log("Added a point... Point: " + points);
            scoreText.text = $"{points}";
            upgradePointsText.text = $"{points}";
        }
        
        public void SetPoints(int points)
        {
            this.points -= points;
            scoreText.text = $"{this.points}";
            upgradePointsText.text = $"{this.points}";
        }

        public void AddMaxGlasses()
        {
            maxGlasses++;
        }

        public int GetPoints()
        {
            return this.points;
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
    }
}
