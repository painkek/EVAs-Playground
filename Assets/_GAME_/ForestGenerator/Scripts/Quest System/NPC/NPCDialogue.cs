// using System;
// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;

// public class NPCDIalogue : MonoBehaviour
// {

//     /// <summary>
//     ///  prompt into gpt
//     ///  
//     ///  create an NPC that has a dialogue system, in the dialogue system have a List of Greeting intros, List of Goodbye Outros, and Greeting bodies. also add an NPC that procedural quest generator that generates a random quest for the player to complete. 
//     /// </summary>
//     [SerializeField] private string[] dialogueLines;  // Array to store dialogue lines
//     private int currentLineIndex = 0;  // Keeps track of the current dialogue line

//     private void StartDialogue()
//     {
//         currentLineIndex = 0;  // Reset to the first line of dialogue
//         ShowLine();  // Show the first line
//     }

//     private void ShowLine()
//     {
//         if (currentLineIndex < dialogueLines.Length)
//         {
//             Debug.Log("NPC says: " + dialogueLines[currentLineIndex]);  // Print the current line in the console
//             currentLineIndex++;  // Move to the next line for the next interaction
//         }
//         else
//         {
//             EndDialogue();  // End the dialogue if all lines have been shown
//         }
//     }

//     private void EndDialogue()
//     {
//         Debug.Log("End of Dialogue");
//         // Optionally reset the index to start the dialogue over next time
//         currentLineIndex = 0;
//     }

//     // Trigger method to detect when the player enters the NPC's collider
//     private void OnTriggerEnter2D(Collider2D other)
//     {
//         if (other.CompareTag("Player"))  // Check if the player is the one entering the trigger
//         {
//             StartDialogue();  // Start the dialogue when the player enters the trigger
//         }
//     }
// }


// // public class DialogueNPC : MonoBehaviour
// // {
// //     public string npcName;

// //     [SerializeField]
// //     private List<string> greetingIntros = new List<string>
// //     {
// //         "Hello there!",
// //         "Greetings, traveler!",
// //         "Well met, friend!",
// //         "Ah, a new face!",
// //         "Welcome, stranger!"
// //     };

// //     [SerializeField]
// //     private List<string> goodbyeOutros = new List<string>
// //     {
// //         "Farewell!",
// //         "Safe travels!",
// //         "Until we meet again!",
// //         "May the winds be at your back!",
// //         "Take care, friend!"
// //     };

// //     [SerializeField]
// //     private List<string> greetingBodies = new List<string>
// //     {
// //         "How can I assist you today?",
// //         "What brings you to our humble village?",
// //         "I hope your journey has been pleasant.",
// //         "It's always good to see new faces around here.",
// //         "Is there anything you need help with?"
// //     };

// //     public string Greet()
// //     {
// //         string intro = greetingIntros[Random.Range(0, greetingIntros.Count)];
// //         string body = greetingBodies[Random.Range(0, greetingBodies.Count)];
// //         return $"{npcName}: {intro} {body}";
// //     }

// //     public string SayGoodbye()
// //     {
// //         string outro = goodbyeOutros[Random.Range(0, goodbyeOutros.Count)];
// //         return $"{npcName}: {outro}";
// //     }
// // }

// // public class QuestNPC : MonoBehaviour
// // {
// //     public string npcName;

// //     [SerializeField]
// //     private List<string> questTypes = new List<string> { "fetch", "kill", "escort", "explore" };

// //     [SerializeField]
// //     private List<string> questTargets = new List<string> { "goblins", "wolves", "bandits", "ancient ruins", "lost treasure", "magical artifact" };

// //     [SerializeField]
// //     private List<string> questLocations = new List<string> { "dark forest", "misty mountains", "abandoned mine", "haunted castle", "mysterious cave" };

// //     public string GenerateQuest()
// //     {
// //         string questType = questTypes[Random.Range(0, questTypes.Count)];
// //         string target = questTargets[Random.Range(0, questTargets.Count)];
// //         string location = questLocations[Random.Range(0, questLocations.Count)];

// //         switch (questType)
// //         {
// //             case "fetch":
// //                 return $"Fetch the {target} from the {location}.";
// //             case "kill":
// //                 return $"Defeat the {target} terrorizing the {location}.";
// //             case "escort":
// //                 return $"Escort the merchant safely through the {target}-infested {location}.";
// //             default: // explore
// //                 return $"Explore the {location} and report back any signs of {target}.";
// //         }
// //     }

// //     public string OfferQuest()
// //     {
// //         string quest = GenerateQuest();
// //         return $"{npcName}: Brave adventurer, I have a task for you! {quest} Will you accept this quest?";
// //     }
// // }

// // // PlayerInteraction script remains unchanged
// // public class PlayerInteraction : MonoBehaviour
// // {
// //     private void OnTriggerEnter2D(Collider2D collision)
// //     {
// //         if (collision.gameObject.TryGetComponent<DialogueNPC>(out DialogueNPC dialogueNPC))
// //         {
// //             Debug.Log(dialogueNPC.Greet());
// //             // Here you would typically trigger your dialogue UI
// //         }
// //         else if (collision.gameObject.TryGetComponent<QuestNPC>(out QuestNPC questNPC))
// //         {
// //             Debug.Log(questNPC.OfferQuest());
// //             // Here you would typically trigger your quest offer UI
// //         }
// //     }

// //     private void OnTriggerExit2D(Collider2D collision)
// //     {
// //         if (collision.gameObject.TryGetComponent<DialogueNPC>(out DialogueNPC dialogueNPC))
// //         {
// //             Debug.Log(dialogueNPC.SayGoodbye());
// //             // Here you would typically close your dialogue UI
// //         }
// //     }
// // }