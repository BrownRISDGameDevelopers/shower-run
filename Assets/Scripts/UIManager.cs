using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    [SerializeField] GameObject pauseScreen;
    [SerializeField] GameObject gameOverScreen;
    [SerializeField] GameObject wonScreen;
    [SerializeField] GameObject tutorialScreen;

    bool paused = false;

    PlayerInputActions _actions;

    public void QuitGame()
    {
        Application.Quit();
    }

    private void Start()
    {
        _actions = new PlayerInputActions();

        if(SceneManager.GetActiveScene().name == "MainMenu") return;
        tutorialScreen.SetActive(true);
    }

    private void OnEnable()
    {
        if(SceneManager.GetActiveScene().name == "MainMenu") return;
        EnemyController.foundPlayer += OnGameOver;
        TeleportManager.Instance.WonGame += OnGameWon;
    }

    private void OnDisable()
    {
        if(SceneManager.GetActiveScene().name == "MainMenu") return;
        EnemyController.foundPlayer -= OnGameOver;
        TeleportManager.Instance.WonGame -= OnGameWon;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) TogglePauseScreen();
    }

    public void StartGame()
    {
        SceneManager.LoadScene("hearnest");
    }

    void TogglePauseScreen()
    {
        switch(paused)
        {
            case true:
                Cursor.visible = false;
                Cursor.lockState = CursorLockMode.Locked;
                pauseScreen.SetActive(false);
                paused = false;
                Time.timeScale = 1;
                break;
                 
            case false:
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
                pauseScreen.SetActive(true);
                paused = true;
                Time.timeScale = 0;
                break;
        }
    }

    public void ReturnToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    void OnGameWon()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        wonScreen.SetActive(true);
        Time.timeScale = 0;
    }

    void OnGameOver()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        gameOverScreen.SetActive(true);
        Time.timeScale = 0;
    }
}
