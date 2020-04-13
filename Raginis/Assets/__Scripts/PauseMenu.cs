using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{

    // == private fields

    [SerializeField] private GameObject player;

    public static bool gameIsPaused = false;
    public GameObject pauseMenuUI;


    // == private methods == 

    void Start(){
        Resume();
    }

    void Update(){
        if(Input.GetKeyDown(KeyCode.Escape)){
            if(gameIsPaused){
                Resume();
            }
            else{
                Pause();
            }
        }
    }

    private void Resume(){
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        gameIsPaused = false;
        Cursor.visible = false;

        SetComponentsEnabled(true);
    }

    void Pause(){
        player.GetComponent<WeaponsController>().StopCoroutine("firingCoroutine");
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        gameIsPaused = true;
        Cursor.visible = true;

        SetComponentsEnabled(false);
    }

    private void SetComponentsEnabled(bool status){
        player.GetComponent<WeaponsController>().enabled = status;
        player.GetComponent<PlayerMovement>().enabled = status;
    }
}
