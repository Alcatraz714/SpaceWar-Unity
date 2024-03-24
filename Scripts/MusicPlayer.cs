using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPlayer : MonoBehaviour
{
    void Awake() 
    {
        int NumMusicPlayers = FindObjectsOfType<MusicPlayer>().Length; //if more than 1 music players - destroy the self
        if (NumMusicPlayers > 1)
        {
            Destroy(gameObject); // destroy self
        }
        else
        {
            DontDestroyOnLoad(gameObject); // if restart then dont destroy - Remains and keeps playing
        }
    }
}
