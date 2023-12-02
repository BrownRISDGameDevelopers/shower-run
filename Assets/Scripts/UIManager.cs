using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    [SerializeField] GameObject pauseScreen;

    bool paused = false;

    PlayerInputActions _actions;

    public void QuitGame()
    {
        Application.Quit();
    }

    private void Start()
    {
        _actions = new PlayerInputActions();
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
}
