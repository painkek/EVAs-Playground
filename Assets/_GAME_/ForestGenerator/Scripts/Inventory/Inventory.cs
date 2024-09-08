using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory
{
    private List<Items> itemsList;

    public Inventory()
    {
        itemsList = new List<Items>();
        Debug.Log("Inventory created");
    }
}
