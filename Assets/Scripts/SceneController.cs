using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    public GameObject tutorialMenu;

    public AudioClip bgrMusic;

    public float volume;

    AudioSource SoundPlayer;

    // Start is called before the first frame update
    void Start()
    {
        SoundPlayer = GetComponent<AudioSource>();
        PlayBackgroundMusic();
        tutorialMenu.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void showTutorial()
    {
        tutorialMenu.SetActive(true);
    }

    public void LoadGame()
    {
        SceneManager.LoadSceneAsync("Game");
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadSceneAsync("Main Menu");
        //GameObject.FindGameObjectWithTag("PauseMenu").GetComponent<PauseMenu>().overrideResumeGame();
    }

    public void Exit() 
    {
        Application.Quit();
    }

    public void PlayBackgroundMusic()
    {
        SoundPlayer.volume=volume;
        SoundPlayer.clip = bgrMusic;
        SoundPlayer.loop = true;
        SoundPlayer.Play();
    }
}
