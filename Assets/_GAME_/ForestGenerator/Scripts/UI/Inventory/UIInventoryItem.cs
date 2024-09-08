using System;
using System.Collections;
using System.Collections.Generic;
using Microsoft.Unity.VisualStudio.Editor;
using TMPro;
using UnityEngine;

public class UIInventoryItem : MonoBehaviour
{
    [SerializeField]
    private Image itemImage; // display image for the item
    [SerializeField]
    private TMP_Text quantityText; //display text for the quantity of the item

    [SerializeField]
    private Image borderImage; // display image for the border of the item

    public event Action<UIInventoryItem> OnItemClicked, OnItemDroppedOn, OnItemBeginDrag, OnItemEndDrag;

    private bool empty = true;
}
