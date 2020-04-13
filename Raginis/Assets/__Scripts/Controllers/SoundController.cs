using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class SoundController : MonoBehaviour
{

    // == private fields ==
    
    private AudioSource audioSource;


    // == public methods ==
    
    void Start(){
        audioSource = GetComponent <AudioSource>();
    }


    // == public methods ==

    public void PlayOneShot(AudioClip clip){
        if(clip){
            audioSource.PlayOneShot(clip);
        }
    }

    public void ToggleSounds(){
        audioSource.mute = !audioSource.mute;
    }

    public static SoundController FindSoundController(){
        SoundController sc = FindObjectOfType<SoundController>();
        if(!sc){
            Debug.LogWarning("Missing Sound Controller");
        }
        return sc;
    }
}
