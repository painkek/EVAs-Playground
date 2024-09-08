using UnityEngine;

public class ProceduralQuestGenerator : MonoBehaviour
{

    // On the NPC part how can I make the NPC random choice, the NPC just greeting the player, and the NPC give the player a quest?

    [SerializeField] private string[] questNames = { "Find the Hidden Treasure", "Defeat the Forest Troll", "Gather 10 Herbs" };
    [SerializeField] private string[] questDescriptions = { "The treasure is hidden somewhere in the dungeon.", "A dangerous troll roams the forest. Defeat it!", "Herbs grow in the wild. Collect 10 of them." };
    [SerializeField] private string[] questObjectives = { "Find the Hidden Treasure", "Defeat the Forest Troll", "Gather 10 Herbs" };
    [SerializeField] private string[] questRewards = { "100 gold", "a magical weapon", "50 experience points" };

    public Quests GenerateRandomQuest()
    {
        Quests newQuest = new Quests();

        // Generate a signle random index
        int randomIndex = Random.Range(0, questNames.Length);

        // Use the same index for both the quest name and description
        newQuest.questName = questNames[randomIndex];
        newQuest.questDescription = questDescriptions[randomIndex];
        newQuest.objective = questObjectives[randomIndex];
        newQuest.reward = questRewards[randomIndex];
        newQuest.isCompleted = false;

        return newQuest;
    }
}