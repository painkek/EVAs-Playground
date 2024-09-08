using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerQuestManager : MonoBehaviour
{
    private Quests currentQuest;
    [SerializeField] private ProceduralQuestGenerator questGenerator;

    // Start is called before the first frame update
    void Start()
    {
        // Start with a new quest
        currentQuest = questGenerator.GenerateRandomQuest();
        DisplayQuest(currentQuest);
    }

    // Update is called once per frame
    void Update()
    {
        // check for quest completion (this logic will vary based on your game's mechanics)
        if (CheckQuestCompletion())
        {
            CompleteQuest();
            currentQuest = questGenerator.GenerateRandomQuest(); // Generate a new quest
            DisplayQuest(currentQuest); // Update UI or player information
        }
    }

    private bool CheckQuestCompletion()
    {
        // This is just an example. Replace this with the actual check in your game.
        // Example: Check if the player has collected 10 herbs or defeated the troll.
        return currentQuest.isCompleted;
    }

    private void CompleteQuest()
    {
        // Handle the reward
        Debug.Log("Quest completed! Reward: " + currentQuest.reward);
        // You can add reward logic here, e.g., give the player items, gold, etc.
    }

    private void DisplayQuest(Quests quest)
    {
        // Update the quest information in the UI or inform the player in some way
        Debug.Log("New Quest: " + quest.questName);
        Debug.Log("Objective: " + quest.objective);
        Debug.Log("Reward: " + quest.reward);
    }
}
