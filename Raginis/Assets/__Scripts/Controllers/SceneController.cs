using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

using Utilities;

public class SceneController : MonoBehaviour
{

    // == private fields ==
    private MusicPlayer mp;


    // == private methods ==

    // This method launches the gameScene.
    public void Play_OnClick(){
        // Ensure sounds play.
        mp = FindObjectOfType<MusicPlayer>();

        if(mp)
            mp.Play();

        SceneManager.LoadSceneAsync(SceneNames.GAMESCENE);
    }

    // This method launches the menuScene.
    public void MainMenu_OnClick(){
        // Stop sounds.
        mp = FindObjectOfType<MusicPlayer>();
        mp.Stop();

        SceneManager.LoadSceneAsync(SceneNames.MAINMENU);
    }
    
    // This method launches the optionsScene additively.
    public void Options_OnClick(){
        SceneManager.LoadSceneAsync(SceneNames.OPTIONSMENU, LoadSceneMode.Additive);
    }

    // This method launches the scoreScene additively.
    public void Scores_OnClick(){
        SceneManager.LoadSceneAsync(SceneNames.SCOREMENU, LoadSceneMode.Additive);
    }

    // This method launches the scoreScene additively.
    public void Gameover_OnClick(){
        SceneManager.LoadSceneAsync(SceneNames.GAMEOVERSCENE, LoadSceneMode.Additive);
    }

    // This method mutes or unmutes the game sounds.
    public void Mute_OnClick(){
        AudioListener.pause = !AudioListener.pause;
    }

    // This method unloads the optionsScene.
    public void BackOptions_OnClick(){
        SceneManager.UnloadSceneAsync(SceneNames.OPTIONSMENU);
    }

    // This method unloads the scoreScene.
    public void BackScore_OnClick(){
        SceneManager.UnloadSceneAsync(SceneNames.SCOREMENU);
    }

    // This method quits the game.
    public void QuitGame(){
        Application.Quit();
    }

}
