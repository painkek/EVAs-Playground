// using UnityEngine;

// public class NPCQuestGiver : MonoBehaviour
// {

//       // possible bug: assignedQuest gives a different name and random descripion. it should match for example: "Find the Hidden Treasure" and "The treasure is hidden somewhere in the dungeon."
//       public Quests assignedQuest;
//       public ProceduralQuestGenerator questGenerator;

//       private void Start()
//       {
//             questGenerator = GetComponent<ProceduralQuestGenerator>();
//             if (questGenerator != null)
//             {
//                   assignedQuest = questGenerator.GenerateRandomQuest();
//             }
//       }

//       public void GiveQuest()
//       {
//             // Replace with your UI code to show the quest information to the player
//             Debug.Log("Here's your Quest: " + assignedQuest.questName);
//             Debug.Log("Description: " + assignedQuest.questDescription);
//       }

//       private void OnMouseDown() // Assuming you click on the NPC to get the quest
//       {
//             GiveQuest();
//       }
// }
