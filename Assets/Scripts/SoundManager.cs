using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour {
    static bool musicIsMuted = false;

    public Text musicMuteStatus;

    private void Start()
    {
        SetMusicMuteText();
    }

    public void ToggleMusic()
    {
        musicIsMuted = !musicIsMuted;

        BackgroundMusic.instance.GetComponent<AudioSource>().mute = musicIsMuted;
        SetMusicMuteText();
    }

    private void SetMusicMuteText()
    {
        if (musicIsMuted)
        {
            musicMuteStatus.text = "Music Off";
            musicMuteStatus.color = Color.red;
        }
        else
        {
            musicMuteStatus.text = "Music On";
            musicMuteStatus.color = Color.green;
        }
    }

}
