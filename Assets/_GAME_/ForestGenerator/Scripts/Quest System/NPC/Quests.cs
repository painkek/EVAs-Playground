using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Quests : MonoBehaviour
{
    public string questName;
    public string questDescription;
    public bool isCompleted;

    public string objective; // e.g., "Collect 5 herbs"
    public string reward;   // e.g., "100 gold"

    // Add more fields as needed, such as objectives, rewards, etc.
}
