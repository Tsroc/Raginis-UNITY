using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;    // text mesh pro library

public class GameController : MonoBehaviour
{

    // == public fields ==

    public int StartingLives { get { return startingLives; } }
    public int RemainingLives { get { return remainingLives; } }
    public int PlayerScore { get { return playerScore; } }


    // == private fields ==

    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private int startingLives = 3;
    
    private int playerScore = 0;
    private int remainingLives;


    // == private methods ==

    private void Awake(){
        SetupSingleton();
    }

    private void Start(){
        UpdateScore();
        remainingLives = startingLives;
    }

    // Set up singleton.
    private void SetupSingleton(){
        if (FindObjectsOfType(GetType()).Length > 1){
            Destroy(gameObject);  
        }
        else{
            DontDestroyOnLoad(gameObject); 
        }
    }

    private void OnEnable(){
        Enemy.EnemyKilledEvent += OnEnemyKilledEvent;
    }

    private void OnDisable(){
        Enemy.EnemyKilledEvent -= OnEnemyKilledEvent;
    }

    // add the score value for the enemy to the player score
    private void OnEnemyKilledEvent(Enemy enemy){
        playerScore += enemy.ScoreValue;
        UpdateScore();
    }

    // display on screen
    private void UpdateScore(){
        scoreText.text = playerScore.ToString();
    }


    // == private methods ==

    public void LoseOneLife(){
        remainingLives--;
    }

}
