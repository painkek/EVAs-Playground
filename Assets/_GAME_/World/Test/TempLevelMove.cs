using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TempLevelMove : MonoBehaviour
{
    public int sceneBuildIndex;

    private void OnTriggerEnter2d(Collider2D other)
    {
        print("Triggered Entered");

        if (other.tag == "Player")
        {

            print("Switching scene to " + sceneBuildIndex);
            SceneManager.LoadScene(sceneBuildIndex, LoadSceneMode.Single);
        }
    }
}
