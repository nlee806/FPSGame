using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BG_MusicControl : MonoBehaviour {
    public bool playAudio;
    [SerializeField] private AudioSource bgMusic;
    void Start()
    {
        bgMusic.ignoreListenerVolume = true;
        bgMusic.ignoreListenerPause = true;
        bgMusic.clip = Resources.Load("BugHunt") as AudioClip;
        playAudio = true;
        //bgMusic.volume = 0.5;
    }
    private void PlayMusic()
    {
        bgMusic.Play();
    }
    private void StopMusic()
    {
        bgMusic.Stop();
    }
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.P)) //turn off audio
        {
            if (playAudio == true) {
                StopMusic();
                playAudio = false;
            }
            else if (playAudio == false) {
                PlayMusic();
                playAudio = true;
            }
        }
    }
}
