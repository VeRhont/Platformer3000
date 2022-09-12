using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private int _lastLevel = 1;
    [SerializeField] private AudioSource _clickSound;

    public void ResumeGame()
    {
        _clickSound.Play();
        SceneManager.LoadScene(_lastLevel);
    }

    public void StartNewGame()
    {
        _clickSound.Play();
        var path = Application.persistentDataPath + "/player.data";

        File.Delete(path);

        SceneManager.LoadScene(_lastLevel);
    }

    public void Quit()
    {
        _clickSound.Play();
        Application.Quit();
    }

    public void PlayClickSound()
    {
        _clickSound.Play(); 
    }
}