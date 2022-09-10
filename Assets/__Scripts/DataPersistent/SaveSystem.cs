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
}