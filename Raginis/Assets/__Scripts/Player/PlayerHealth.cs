using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{

    // == private fields == 
    [SerializeField] Image healthBar;

    private GameController gc;
    private SceneController sc;
    private WeaponsController wc;
    private Vector3 startlocation;
    private float starthealth;
    private float currenthealth;
    private float currentValue;
    private int prevHighscore;
    private int currentScore;


    // == private methods == 
    
    private void Start(){
        gc = FindObjectOfType<GameController>();
        sc = FindObjectOfType<SceneController>();
        startlocation = new Vector3(transform.position.x, transform.position.y, transform.position.z);

        if(gc){
            starthealth = gc.StartingLives;
        }
    }
    
    // When the player collides with an object.
    private void OnTriggerEnter2D(Collider2D whatHitMe){
        var enemy = whatHitMe.GetComponent<Enemy>();
        gc.LoseOneLife();
        currenthealth = gc.RemainingLives;

        if(enemy){
            //reduce life
            if(gc){
                if(gc.RemainingLives > 0){
                    HandleHealthBar();
                    // Respawn
                    transform.position = startlocation;

                }
                else{
                    // Gameover - End Game
                    HandleHealthBar();

                    // Set high score.
                    Debug.Log("Previous high score: " + PlayerPrefs.GetInt("HighScore"));
                    prevHighscore = PlayerPrefs.GetInt("HighScore", 0);
                    currentScore = gc.PlayerScore;

                    if(currentScore > prevHighscore){
                        PlayerPrefs.SetInt("HighScore", currentScore);
                        Debug.Log("New high score: " + PlayerPrefs.GetInt("HighScore"));
                    }
                    else{
                        Debug.Log("No new high score.");
                    }
                    sc.Gameover_OnClick();
                    Cursor.visible = true;

                    wc = FindObjectOfType<WeaponsController>();
                    wc.hideCrosshairs();

                    Time.timeScale = 0f;
                    Destroy(gameObject);
                }
            }
        } 
    }

    private void HandleHealthBar(){
        healthBar.fillAmount = currenthealth/starthealth;
    }
}
