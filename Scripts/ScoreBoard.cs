using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreBoard : MonoBehaviour
{
    // Score varaible
    int Score;
    // TMP text variable to hold score
    TMP_Text ScoreText;

    // Start method - added for starting the script 
    void Start()
    {
        ScoreText = GetComponent<TMP_Text>();
        ScoreText.text = "Destroy the Invaders"; // C style string input for the level
    }

    // Update Score
    public void UpdateScore(int AmountToIncrease)
    {
        Score += AmountToIncrease;
        //Debug.Log(Score); - [Redacted]
        ScoreText.text = Score.ToString();
    }
}
