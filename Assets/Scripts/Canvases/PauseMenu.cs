using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenu;
    public static bool isPaused;
    private bool gameOver = false;
    public bool langSelect = false;

    public Sprite helpSprite;

    // Start is called before the first frame update
    void Start()
    {
        pauseMenu.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)&&!langSelect&&!gameOver)
        {
            if (isPaused) resumeGame();
            else if (!isPaused) pauseGame();
        }
    }

    public void LoadMainMenu()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
        SceneManager.LoadSceneAsync("Main Menu");
    }

    public void Exit() 
    {
        Application.Quit();
    }

    public void pauseGame()
    {
        pauseMenu.SetActive(true);
        GameObject.FindGameObjectWithTag("LanguageHelp").GetComponent<SpriteRenderer>().sprite=helpSprite;
        Time.timeScale = 0f;
        isPaused = true;
    }

    public void pause()
    {
        Time.timeScale = 0f;
        isPaused = true;
    }

    public void GameOver()
    {
        Time.timeScale = 0f;
        isPaused = true;
        gameOver = true;
        GameObject.FindGameObjectWithTag("GameOverMenu").GetComponent<GameOverScript>().Activate();
    }

    public void resumeGame()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
    }

    public bool getIsPaused()
    {
        return isPaused;
    }

    public void overrideResumeGame()
    {
        isPaused = false;
        Time.timeScale = 1f;
    }
}
