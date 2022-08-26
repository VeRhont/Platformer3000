using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private int _lastLevel = 1;

    public void StartGame()
    {
        SceneManager.LoadScene(_lastLevel);
        PlayerDataSaveLoad.S.LoadPlayer();
    }

    public void Quit()
    {
        Debug.Log("Quit");
        Application.Quit();
    }
}