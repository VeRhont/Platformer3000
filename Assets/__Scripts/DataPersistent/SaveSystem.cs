using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveSystem
{
    public static void SavePlayer(PlayerController player, Inventory inventory)
    {
        var formatter = new BinaryFormatter();
        var path = Application.persistentDataPath + "/player.data";
        var stream = new FileStream(path, FileMode.Create);

        var data = new PlayerData(player, inventory);

        formatter.Serialize(stream, data);
        stream.Close();
    }

    public static PlayerData LoadPlayer()
    {
        var path = Application.persistentDataPath + "/player.data";

        if (File.Exists(path))
        {
            var formatter = new BinaryFormatter();
            var stream = new FileStream(path, FileMode.Open);

            PlayerData data = formatter.Deserialize(stream) as PlayerData;

            stream.Close();
            return data;
        }
        Debug.Log("Error with SaveSystem.LoadPlayer");
        return null;
    }

    public static void SaveEnemies(Enemy[] enemies)
    {
        var formatter = new BinaryFormatter();
        var path = Application.persistentDataPath + "/enemy.data";
        var stream = new FileStream(path, FileMode.Create);

        var data = new EnemyData(enemies);

        formatter.Serialize(stream, data);
        stream.Close();
    }

    public static EnemyData LoadEnemies()
    {
        var path = Application.persistentDataPath + "/enemy.data";

        if (File.Exists(path))
        {
            var formatter = new BinaryFormatter();
            var stream = new FileStream(path, FileMode.Open);

            EnemyData data = formatter.Deserialize(stream) as EnemyData;

            stream.Close();
            return data;
        }
        Debug.Log("Error with SaveSystem.LoadEnemies");
        return null;
    }
}