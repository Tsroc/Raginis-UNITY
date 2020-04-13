using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// use this to manage collisions

public class Enemy : MonoBehaviour
{

    // == public fields ==
    
    public int ScoreValue { get { return scoreValue; } }
    // delegate type to use for event
    public delegate void EnemyKilled(Enemy enemy);
    // create static method to be implemented in the listener
    public static EnemyKilled EnemyKilledEvent;


    // == private fields ==

    [SerializeField] private int scoreValue = 10;
    [SerializeField] private GameObject explosionFX;
    [SerializeField] private AudioClip crashSound;
    [SerializeField] private AudioClip dieSound;
    [SerializeField] private AudioClip spawnSound;

    private SoundController sc;
    private float explosionDuration = 1.0f;


    // == Health fields ==
    
    [Header("Health")]
    [SerializeField] private Image healthBar;
    [SerializeField] private float starthealth;
    private float currenthealth;
    private float currentValue;


    // == private methods ==

    private void Start(){
        sc = SoundController.FindSoundController();
        PlaySound(spawnSound);

        // Health.
        currenthealth = starthealth;
        HandleHealthBar(); 
    }

    private void PlaySound(AudioClip clip){
        if (sc){
            sc.PlayOneShot(clip);
        }
    }

    private void OnTriggerEnter2D(Collider2D whatHitMe){
        var player = whatHitMe.GetComponent<PlayerMovement>();
        var bullet = whatHitMe.GetComponent<Bullet>();

        if(player){
            PlaySound(crashSound);
            Destroy(gameObject);
        }

        if(bullet){
            Destroy(bullet.gameObject);
            currenthealth--;
            HandleHealthBar();

            Debug.Log("Health: " + currenthealth);
            if(currenthealth <= 0){
                PlaySound(dieSound);
                Destroy(bullet.gameObject);
                PublishEnemyKilledEvent();

                GameObject explosion = Instantiate(explosionFX, transform.position, transform.rotation);
                Destroy(explosion, explosionDuration);
                Destroy(gameObject);
           }

        }
    }

    private void PublishEnemyKilledEvent(){
        if(EnemyKilledEvent != null){
            EnemyKilledEvent(this);
        }
    }

    private void HandleHealthBar(){
        healthBar.fillAmount = currenthealth/starthealth;
    }
}
