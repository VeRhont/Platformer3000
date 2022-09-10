using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private int _lastLevel = 1;

    public void ResumeGame()
    {
        SceneManager.LoadScene(_lastLevel);
    }

    public void StartNewGame()
    {
        var path = Application.persistentDataPath + "/player.data";

        File.Delete(path);

        SceneManager.LoadScene(_lastLevel);
    }

    public void Quit()
    {
        Application.Quit();
    }
}