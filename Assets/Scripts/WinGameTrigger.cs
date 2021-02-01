using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinGameTrigger : MonoBehaviour
{
    public int WinGameSceneIndex;

    private void OnTriggerEnter(Collider other)
    {
        if(!other.isTrigger && other.CompareTag("Player"))
        {
            Debug.Log("Win Player");
            SceneManager.LoadScene(WinGameSceneIndex);
            MusicManager.Instance.PlayWinGameMusic();
        }
        else
        {
            Debug.Log(other.name);
        }
    }
}
