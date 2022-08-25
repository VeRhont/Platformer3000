using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private GameObject[] _enemies;

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
        _enemies = GameObject.FindGameObjectsWithTag("Enemy");

        SaveSystem.SaveEnemies(_enemies);
    }

    private void LoadEnemies()
    {
        var data = SaveSystem.LoadEnemies();

        for (int i = 0; i < data.Healths.Length; i++)
        {
            Debug.Log($"{data.Healths[i]}, {data.MaxHealths[i]}, {data.PositionsX[i]}, {data.PositionsY[i]}, {data.PositionsZ[i]}");
        }
    }
}