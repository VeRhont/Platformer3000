using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool IsGamePaused = false;

    [SerializeField] private GameObject _pauseMenuUI;
    [SerializeField] private GameObject _activeGameUI;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (IsGamePaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    public void Pause()
    {
        _activeGameUI.SetActive(false);
        _pauseMenuUI.SetActive(true);
        IsGamePaused = true;
        Time.timeScale = 0f;
    }

    public void Resume()
    {
        _activeGameUI.SetActive(true);
        _pauseMenuUI.SetActive(false);
        IsGamePaused = false;
        Time.timeScale = 1f;
    }

    public void LoadMenu()
    {
        Time.timeScale = 1f;

        Debug.Log("MenU");
        SceneManager.LoadScene("Menu");
    }

    public void Quit()
    {
        Debug.Log("QUIT");
        Application.Quit();
    }
}