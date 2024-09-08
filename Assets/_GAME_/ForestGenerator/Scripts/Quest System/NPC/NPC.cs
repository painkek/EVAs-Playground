using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour
{
    // NPC Type Enum
    public enum NPCType
    {
        Greeter,
        Helper,
        QuestGiver
    }

    public NPCType npcType;
    [SerializeField]
    private ProceduralQuestGenerator questGenerator; // If it's a Quest Giver NPC

    // void Start()
    // {
    //     AssignRandomNPCType();
    // }

    // // Randomly assign the NPC's role
    // private void AssignRandomNPCType()
    // {
    //     npcType = (NPCType)Random.Range(0, System.Enum.GetValues(typeof(NPCType)).Length);
    // }

    // When the player interacts with the NPC
    public void Interact()
    {
        switch (npcType)
        {
            case NPCType.Greeter:
                GreetPlayer();
                break;
            case NPCType.Helper:
                HelpPlayer();
                break;
            case NPCType.QuestGiver:
                GiveQuestToPlayer();
                break;
        }
    }

    private void GreetPlayer()
    {
        Debug.Log("Hello, traveler!");
        // Add any additional logic for greeting
    }

    private void HelpPlayer()
    {
        Debug.Log("Here, take this potion to help you on your journey.");
        // Add logic for helper NPC
    }

    private void GiveQuestToPlayer()
    {
        if (questGenerator != null)
        {
            Quests assignedQuest = questGenerator.GenerateRandomQuest();
            Debug.Log("Here's your quest: " + assignedQuest.questName + ". " + assignedQuest.questDescription);
            // Add logic to assign the quest to the player
        }
    }
}
