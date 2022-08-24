using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private Enemy[] _enemies;

    public void ResumeGame()
    {
        LoadEnemies();        
    }

    public void StartNewGame()
    {
        SceneManager.LoadScene(1);
    }

    public void SaveScene()
    {
        Debug.Log("Scene is saved");

        SaveEnemies();
    }

    private void SaveEnemies()
    {
        _enemies = GameObject.FindObjectsOfType<Enemy>();

        SaveSystem.SaveEnemies(_enemies);
    }

    private void LoadEnemies()
    {
        var data = SaveSystem.LoadEnemies();
    }
}
