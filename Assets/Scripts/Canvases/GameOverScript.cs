using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverScript : MonoBehaviour
{
    public GameObject gameOverMenu;
    void Start()
    {
        gameOverMenu.SetActive(false);
    }
    public void Activate()
    {
        gameOverMenu.SetActive(true);
    }
    public void Restart()
    {
        SceneManager.LoadSceneAsync("Game");
    }
    public void MainMenu()
    {
        GameObject.FindGameObjectWithTag("PauseMenu").GetComponent<PauseMenu>().resumeGame();
        SceneManager.LoadSceneAsync("Main Menu");
    }
    public void Quit()
    {
        Application.Quit();
    }
}
