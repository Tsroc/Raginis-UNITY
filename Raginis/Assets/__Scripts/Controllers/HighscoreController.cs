using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;    // text mesh pro library

public class HighscoreController: MonoBehaviour
{

    // == private fields ==
    
    [SerializeField] private TextMeshProUGUI scoreText;

    private int playerScore;


    // == private methods ==

    // Sets the scoreText to the highscore.
    void Start(){
        playerScore = PlayerPrefs.GetInt("HighScore", 0);
        scoreText.text = playerScore.ToString();
    }
}
