using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioClip backgroundMusicLevel1;
    [SerializeField] private AudioClip backgroundMusicMainMenu;
    private AudioSource audioPlayer;
    [SerializeField] private float volume = 0.25f;

    private void Awake()
    {
        int countAM = FindObjectsOfType<AudioManager>().Length;

        if (countAM > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            FindObjectOfType<HUDController>().OnSceneChange += PlayMusic;
            DontDestroyOnLoad(gameObject);
        }
    }

    private void PlayMusic()
    {
        audioPlayer = GetComponent<AudioSource>();

        audioPlayer.volume = volume;

        if (SceneManager.GetActiveScene().name == "Level1")
        {
            audioPlayer.clip = backgroundMusicLevel1;
        }
        if (SceneManager.GetActiveScene().name == "Main")
        {
            audioPlayer.clip = backgroundMusicMainMenu;
        }

        audioPlayer.Play();
    }
}
