using UnityEngine;

[System.Serializable]
public class PlayerData
{
    public int XpCount;
    public float CurrentHealth;
    public float[] Position;

    //public bool[] IsFull;
    //public string[] Slots;

    public PlayerData(PlayerController player, Inventory inventory)
    {
        XpCount = player.XpCount;
        CurrentHealth = player.Health;

        Position = new float[2];
        var playerPosition = player.transform.position;
        Position[0] = playerPosition.x;
        Position[1] = playerPosition.y;

        //IsFull = new bool[inventory.IsFull.Length];
        //Slots = new string[inventory.Slots.Length];

        //for (int i = 0; i < inventory.IsFull.Length; i++)
        //{
        //    IsFull[i] = inventory.IsFull[i];

        //    if (IsFull[i])
        //    {
        //        Slots[i] = inventory.Slots[i].name;
        //    }
        //    else
        //    {
        //        Slots[i] = null;
        //    }
        //}
    }
}