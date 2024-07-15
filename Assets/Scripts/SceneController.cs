using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    public AudioClip bgrMusic;

    public AudioClip placeFoodSound;

    public float volume;

    AudioSource SoundPlayer;

    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(gameObject);
        SoundPlayer = GetComponent<AudioSource>();
        PlayBackgroundMusic();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LoadGame()
    {
        SceneManager.LoadSceneAsync("Game");
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadSceneAsync("Main Menu");
    }

    public void Exit() 
    {
        Application.Quit();
    }

    public void PlayPlaceFood()
    {
        AudioSource.PlayClipAtPoint(placeFoodSound, transform.position, 0.5f);
    }
    public void PlayBackgroundMusic()
    {
        SoundPlayer.volume=volume;
        SoundPlayer.clip = bgrMusic;
        SoundPlayer.loop = true;
        SoundPlayer.Play();
    }
}
