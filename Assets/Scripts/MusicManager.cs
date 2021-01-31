using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    private static MusicManager instance;

    public static MusicManager Instance
    {
        get
        {
            if(instance == null)
            {
                instance = GameObject.FindObjectOfType<MusicManager>();
                if(instance == null)
                {
                    instance = Instantiate<MusicManager>(Resources.Load<MusicManager>("MusicManager"));
                }
            }
            return instance;
        }
    }

    public AudioClip menuIntro, menuLoop, mainLoop, dangerLoop, kinderzimmerLoop, LostGameLoop;

    public AudioSource mainLoopSource;
    public AudioSource dangerLoopSource;

    private bool inGame = false;

    public void PlayMenuMusic()
    {
        inGame = false;
        mainLoopSource.Stop();
        dangerLoopSource.Stop();

        mainLoopSource.clip = menuIntro;
        dangerLoopSource.clip = dangerLoop;

        mainLoopSource.Play();
    }

    public void PlayGameMusic()
    {
        inGame = true;
        mainLoopSource.Stop();
        dangerLoopSource.Stop();

        mainLoopSource.clip = mainLoop;
        dangerLoopSource.clip = dangerLoop;

        mainLoopSource.Play();
    }

    public void PlayWinGameMusic()
    {
        inGame = true;
        mainLoopSource.Stop();
        dangerLoopSource.Stop();

        mainLoopSource.clip = menuIntro;
        dangerLoopSource.clip = dangerLoop;

        mainLoopSource.Play();
    }

    public void PlayLooseGameMusic()
    {
        inGame = true;
        mainLoopSource.Stop();
        dangerLoopSource.Stop();

        mainLoopSource.clip = LostGameLoop;
        dangerLoopSource.clip = dangerLoop;

        mainLoopSource.Play();
    }


    private void Update()
    {
        if (!inGame)
        {
            if (!mainLoopSource.isPlaying)
            {
                mainLoopSource.clip = menuLoop;
                mainLoopSource.Play();
            }
        }
        else
        {
            mainLoopSource.loop = dangerLoopSource.loop = true;
            if (!mainLoopSource.isPlaying)
            {
                mainLoopSource.Play();
            }
        }
    }
}
