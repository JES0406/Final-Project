using UnityEngine;
using UnityEngine.AI;

public class EnemyFactory : MonoBehaviour
{
    public GameObject[] enemyPrefabs; // Array to hold your prefab references
    public GameObject enemyPrefab; // Default prefab to use if no array is needed
    public Transform[] spawnPoints; // Array of spawn points
    public GameObject player; // Reference to the player object

    // Method to spawn an enemy
    public void SpawnEnemy(int prefabIndex, Vector3 position)
    {
        if (prefabIndex < 0 || prefabIndex >= enemyPrefabs.Length)
        {
            Debug.LogError("Prefab index out of range.");
            return;
        }

        GameObject enemy = Instantiate(enemyPrefabs[prefabIndex], position, Quaternion.identity);

        // Ensure NavMeshAgent and EnemyAIScript are added
        NavMeshAgent agent = enemy.GetComponent<NavMeshAgent>();
        if (agent == null)
        {
            agent = enemy.AddComponent<NavMeshAgent>();
        }

        EnemyAIScript aiScript = enemy.GetComponent<EnemyAIScript>();
        if (aiScript == null)
        {
            aiScript = enemy.AddComponent<EnemyAIScript>();
        }

        // Set the destination for the AI script
        aiScript.target = player;

        // Optionally initialize or configure NavMeshAgent if needed
        // Example: agent.speed = someSpeedValue;
    }

    // Example method to spawn at predefined spawn points
    public void SpawnAllEnemies()
    {
        foreach (Transform spawnPoint in spawnPoints)
        {
            SpawnEnemy(Random.Range(0, enemyPrefabs.Length), spawnPoint.position);
        }
    }
}
