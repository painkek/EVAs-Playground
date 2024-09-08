using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "QuestInfoSO", menuName = "ScriptableObjects/QuestInfoSO", order = 1)]

public class QuestInfoSO : ScriptableObject
{

    // to reference any quest id
    [field: SerializeField] public string id { get; private set; }

    [Header("General")]

    public string displayName;

    [Header("Requirements")]
    public int levelRequirement;
    public QuestInfoSO[] questPrerequisites;

    [Header("Steps")]
    public GameObject[] questStepsPrefabs;

    [Header("Rewards")]
    public int goldReward;
    public int expReward;

    // ensure the id is always the name of the SO asset.
    private void OnValidate()
    {
#if UNITY_EDITOR
        id = this.name;
        UnityEditor.EditorUtility.SetDirty(this);
#endif

    }
}

// using UnityEngine;
// using System.Collections.Generic;

// public class QuestGenerator : MonoBehaviour
// {
//     [SerializeField] private QuestDatabase questDatabase; // Reference to a database of quest templates
//     [SerializeField] private PlayerController playerController; // Reference to the player's controller

//     private List<Quest> activeQuests = new List<Quest>();

//     public void GenerateQuest()
//     {
//         // Choose a random quest template from the database
//         QuestTemplate questTemplate = questDatabase.GetRandomQuestTemplate();

//         // Instantiate a new quest based on the template
//         Quest newQuest = Instantiate(questTemplate.questPrefab, transform);
//         newQuest.Initialize(questTemplate.questName, questTemplate.questDescription, questTemplate.questObjectives, questTemplate.questRewards);

//         // Add the quest to the list of active quests
//         activeQuests.Add(newQuest);

//         // Update the player's quest log
//         playerController.UpdateQuestLog(activeQuests);
//     }

//     public void CompleteQuest(Quest quest)
//     {
//         // Remove the completed quest from the list of active quests
//         activeQuests.Remove(quest);

//         // Award the player the quest rewards
//         playerController.AwardRewards(quest.questRewards);

//         // Generate a new quest
//         GenerateQuest();
//     }
// }

// // QuestTemplate class (defined in the QuestDatabase)
// public class QuestTemplate
// {
//     public string questName;
//     public string questDescription;
//     public List<QuestObjective> questObjectives;
//     public List<QuestReward> questRewards;
//     public GameObject questPrefab;
// }

// // Quest class
// public class Quest
// {
//     public string questName;
//     public string questDescription;
//     public List<QuestObjective> questObjectives;
//     public List<QuestReward> questRewards;
//     public bool isCompleted;

//     public void Initialize(string name, string description, List<QuestObjective> objectives, List<QuestReward> rewards)
//     {
//         questName = name;
//         questDescription = description;
//         questObjectives = objectives;
//         questRewards = rewards;
//         isCompleted = false;
//     }
// }

// // QuestObjective class
// public class QuestObjective
// {
//     public string objectiveText;
//     public bool isCompleted;
// }

// // QuestReward class
// public class QuestReward
// {
//     public string rewardName;
//     public int rewardValue;
// }