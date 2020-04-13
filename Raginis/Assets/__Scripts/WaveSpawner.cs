using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSpawner : MonoBehaviour
{
    // == public fields == 
    public enum SpawnState { SPAWNING, WAITING, COUNTING}

    [System.Serializable] public class Wave{
        public string name;
        public Transform enemy;
        public int count;
        public float rate;
    }


    // == private fields == 
    
    [SerializeField] private Wave[] waves;
    [SerializeField] private Transform[] spawnPoints;
    [SerializeField] private float timeBetweenWaves = 5f;
    [SerializeField] private float waveCountdown;

    private SpawnState state = SpawnState.COUNTING;
    private int nextWave = 0;
    private int waveCount = 1;     //starts at 1.
    private float searchCountdown = 1f;


    // == private methods ==

    void Start(){
        waveCountdown = timeBetweenWaves;
    }

    void Update(){
        if(state == SpawnState.WAITING){
            //check if enemies are still alive
            if(!EnemyIsAlive()){
                //begin a new round
                WaveCompleted();
                return;
            }
            else{
                return;
            }
        }

        if(waveCountdown <= 0){
            if (state != SpawnState.SPAWNING){
                //start spawning a wave
                StartCoroutine( SpawnWave( waves[nextWave] ));
            }
        }
        else{
            waveCountdown -= Time.deltaTime;
        }
    }

    private void WaveCompleted(){
        Debug.Log("Wave Completed!");

        state = SpawnState.COUNTING;
        waveCountdown = timeBetweenWaves;

        // Boss mob spawns every 5th wave, between this wave mobs are random.
        if(waveCount % 4 == 0){
            nextWave = 2;
        }
        else{
            //pick 1 or 2 at random;
            nextWave = Random.Range(0, 2);
        }

        waveCount++;
    }

    private bool EnemyIsAlive(){
        searchCountdown -= Time.deltaTime;
        if(searchCountdown <= 0f){
            searchCountdown = 1f;
            if(GameObject.FindGameObjectWithTag("Enemy") == null){
                return false;
            }
        }
        return true;
    }

    private IEnumerator SpawnWave(Wave _wave){
        state = SpawnState.SPAWNING;

        //spawn logic
        for(int i = 0; i< _wave.count; i++){
            SpawnEnemy(_wave.enemy);
            yield return new WaitForSeconds(1f/_wave.rate);
        }

        state = SpawnState.WAITING;
        yield break;
    }

    private void SpawnEnemy(Transform _enemy){
        if(spawnPoints.Length == 0){
            Debug.Log("No spawn points referenced.");
        }
        Transform _sp = spawnPoints[ Random.Range(0, spawnPoints.Length)];
        Instantiate(_enemy, _sp.position, _sp.rotation);
    }
}
