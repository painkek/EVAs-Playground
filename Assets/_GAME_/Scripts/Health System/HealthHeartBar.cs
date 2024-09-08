using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthHeartBar : MonoBehaviour
{
    public GameObject heartPrefab;

    // create playerhealth value future reference
    public float health, maxHealth;
    List<HealthHeart> hearts = new List<HealthHeart>();

    private void Start()
    {
        DrawHearts();
    }

    // draw hearts
    public void DrawHearts()
    {
        // 8 -> 4 full hearts
        ClearHearts();

        // determnine how many hearts to make total
        // based off the max health
        float maxHealthRemainder = maxHealth % 2;
        int heartsToMake = (int)((maxHealth / 2) + maxHealthRemainder);
        // make 5 hearts 
        for (int i = 0; i < heartsToMake; i++)
        {
            CreateEmptyHeart(); // make total hearts needed
        }

        for (int i = 0; i < hearts.Count; i++)
        {
            // crazy math to determine the status of the heart
            int heartStatusRemainder = (int)Mathf.Clamp(health - (i * 2), 0, 2);
            hearts[i].SetHeartImage((HeartStatus)heartStatusRemainder);

        }
    }

    // create the heart bar
    public void CreateEmptyHeart()
    {
        GameObject newHeart = Instantiate(heartPrefab);
        newHeart.transform.SetParent(transform);

        HealthHeart heartComponent = newHeart.GetComponent<HealthHeart>();
        heartComponent.SetHeartImage(HeartStatus.Empty);
        hearts.Add(heartComponent);
    }


    // remove all the heart under canvas
    public void ClearHearts()
    {
        foreach (Transform t in transform)
        {
            Destroy(t.gameObject);
        }
        hearts = new List<HealthHeart>();
    }

}
