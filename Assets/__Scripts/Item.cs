using UnityEngine;

public class Item
{
    public enum ItemType
    {
        Sword, 
        HealthPotion,
        JumpPotion,
        Coin,
        ThrowingKnife,
        Key
    }

    public ItemType itemType;
    public int amount;
}
