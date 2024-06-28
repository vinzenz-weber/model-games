using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Vinni {

public class Score : MonoBehaviour
{
    public TMP_Text scoreText;
    public TMP_Text highScoreText; // Add this to display high score on game over menu

    public TMP_Text scoreMenu; // Add this to display final score on game over menu

    private int currentScore = 0;
    private int highScore = 0;



    // Start is called before the first frame update
    void Start()
    {
        // Load the high score from PlayerPrefs
        highScore = PlayerPrefs.GetInt("HighScore", 0);
        highScoreText.text = "High Score: " + highScore.ToString();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        IncreaseScore(1);
    }

    void IncreaseScore(int amount)
    {
        currentScore += amount;
        scoreText.text = currentScore.ToString();

        // Check if the current score is higher than the high score
        if (currentScore > highScore)
        {
            highScore = currentScore;
            highScoreText.text = "High Score: " + highScore.ToString();

            // Save the new high score
            PlayerPrefs.SetInt("HighScore", highScore);
            PlayerPrefs.Save();
        }
    }

    // Call this function when the game is over
    public void OnGameOver()
    {
        // Display the final score and the high score
        highScoreText.text = "High Score: " + highScore.ToString();
        scoreMenu.text = "Score: " + currentScore.ToString();
    }
}
}

