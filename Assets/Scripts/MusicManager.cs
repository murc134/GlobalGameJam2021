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
    [SerializeField]
    private AudioClip menuIntro;
    [SerializeField]
    private AudioClip menuLoop;
    [SerializeField]
    private AudioClip mainLoop;
    [SerializeField]
    private AudioClip dangerLoop;
    [SerializeField]
    private AudioClip kinderzimmerLoop;
    [SerializeField]
    private AudioClip LostGameLoop;

    [SerializeField]
    private AudioSource mainLoopSource;

    [SerializeField]
    private AudioSource dangerLoopSource;

    public float DangerIntensity
    {
        get
        {
            return dangerLoopSource.volume;
        }
        set
        {
            dangerLoopSource.volume = Mathf.Clamp(value, 0, 1);
        }
    }

    private bool inGame = false;

    public BasicBehaviour Player;

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
        dangerLoopSource.volume = 0;
        dangerLoopSource.Play();
    }

    public void PlayWinGameMusic()
    {
        inGame = true;
        mainLoopSource.Stop();
        dangerLoopSource.Stop();

        mainLoopSource.clip = kinderzimmerLoop;
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

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    static void OnBeforeSceneLoadRuntimeMethod()
    {
        //Instance.PlayMenuMusic();
    }

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
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
            if(Player != null && Player.IsActive)
            {
                Debug.Log(Player.EnemiesInRadius.Count);
                float smallesDistance = Player.DangerRadius;
                foreach(Transform enemy in Player.EnemiesInRadius)
                {
                    float dist = Vector3.Distance(enemy.position, Player.transform.position);
                    if (dist < smallesDistance)
                    {
                        smallesDistance = dist;
                    }
                }

                DangerIntensity = 1.0f - ( 1.0f / Player.DangerRadius * smallesDistance );
            }
            else
            {
                DangerIntensity = 0;
            }
        }

    }
}
