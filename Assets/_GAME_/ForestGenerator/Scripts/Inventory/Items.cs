using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Items
{
    public enum ItemType
    {
        Egg,
        Fruits,
        Milk,
        Coins
    }

    public ItemType itemType;
    public int amount;
}
