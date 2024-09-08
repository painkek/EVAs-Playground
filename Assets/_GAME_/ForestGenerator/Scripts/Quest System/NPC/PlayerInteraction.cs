using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    private NPC currentNPC;  // Track the NPC the player can interact with

    void Update()
    {
        // Check if the player presses 'E' and there is an NPC to interact with
        if (Input.GetKeyDown(KeyCode.E) && currentNPC != null)
        {
            currentNPC.Interact();  // Call the NPC's Interact method
        }
    }

    // Detect when the player enters the NPC's interaction range
    private void OnTriggerEnter2D(Collider2D collision)
    {
        NPC npc = collision.GetComponent<NPC>();
        if (npc != null)
        {
            currentNPC = npc;
            Debug.Log("Player can interact with NPC. Press 'E'. NPC Type: " + npc.npcType);
        }
        else
        {
            Debug.Log("OnTriggerEnter2D detected, but no NPC component found.");
        }
    }

    // Detect when the player leaves the NPC's interaction range
    private void OnTriggerExit2D(Collider2D collision)
    {
        NPC npc = collision.GetComponent<NPC>();
        if (npc != null)
        {
            Debug.Log("Player left NPC interaction range. NPC Type: " + npc.npcType);
            currentNPC = null;  // Reset currentNPC when leaving an NPC
        }
    }
}
