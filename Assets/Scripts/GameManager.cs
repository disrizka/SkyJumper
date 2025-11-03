using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
   public Player player;
   public Text scoreText;
   public Text coinText;
   public GameObject playButton;
   public GameObject gameOver;

   private int score;
   private int coins;

   public void Awake()
   {
      Application.targetFrameRate = 60;
      Pause();
   }

   public void Play()
   {
      score = 0;
      coins = 0;
      scoreText.text = score.ToString();
      
      if(coinText != null)
      {
         coinText.text = "Coins: " + coins.ToString();
      }

      playButton.SetActive(false);
      gameOver.SetActive(false);

      Time.timeScale = 1f;
      player.enabled = true;

      Pipes[] pipes = FindObjectsOfType<Pipes>();

      for (int i = 0; i < pipes.Length; i++)
      {
         Destroy(pipes[i].gameObject);
      }
   }

   public void Pause()
   {
      Time.timeScale = 0f;
      player.enabled = false;
   }

   public void GameOver()
   {
      gameOver.SetActive(true);
      playButton.SetActive(true);

      Pause();
   }

   public void IncreaseScore()
   {
      score++;
      scoreText.text = score.ToString();
   }

   public void AddCoin(int amount)
   {
      coins += amount;
      if(coinText != null)
      {
         coinText.text = "Coins: " + coins.ToString();
      }
   }
}