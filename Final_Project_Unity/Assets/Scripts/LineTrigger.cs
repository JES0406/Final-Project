using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineTrigger : MonoBehaviour
{
    [SerializeField] private int currentLevel = 1;
    private void OnTriggerEnter(Collider other)
    {
        // Debug.Log(other.tag);
        if (other.CompareTag("Player"))
        {

            // Debug.Log($"{GameManager.Instance.GetCurrentLevel()} - {currentLevel}");
            if (GameManager.Instance.GetCurrentLevel() == currentLevel)
            {
                // Debug.Log("Loading level...");
                currentLevel++;
                GameManager.Instance.LevelUp();
            }
        }
    }
}

