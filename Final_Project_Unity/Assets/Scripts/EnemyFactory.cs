using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class EnemyFactory : MonoBehaviour
{
    public GameObject[] enemyPrefabs; // Array to hold your prefab references
    public GameObject enemyPrefab; // Default prefab to use if no array is needed
    public EnemiesData enemiesData; // Array of enemie data
    public GameObject player; // Reference to the player object
    public List<GameObject> enemies = new List<GameObject>(); // List to hold all enemies in the scene

    // Method to spawn an enemy
    public void SpawnEnemy(EnemyData enemyData)
    {
        int prefabIndex = enemyData.id;
        Vector3 position = new Vector3(enemyData.position.x, enemyData.position.y, enemyData.position.z);
        EnemyStats stats = enemyData.stats;

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

        SetStats(aiScript, stats);
        PlaceEnemy(aiScript, enemyData.position);

        // Set the destination for the AI script
        aiScript.target = player;

        // We add the tag "Enemy" to the enemy object
        enemy.tag = "Enemy";

        // We add a collider to the enemy object
        SphereCollider collider = enemy.AddComponent<SphereCollider>();
        enemy.AddComponent<Rigidbody>();
        collider.radius = .25f;
        collider.isTrigger = true;

        enemies.Add(enemy);

    }

    public void ResetEnemies()
    {
        // Destroy all enemies in the scene
        foreach (GameObject enemy in enemies)
        {
            Destroy(enemy);
        }
        enemies.Clear();
        enemiesData.enemies.Clear();
    }

    public void AddEnemyData(EnemyData enemyData)
    {
        // Add an enemy to the scene
        enemiesData.enemies.Add(enemyData);
    }

    // Example method to spawn at predefined spawn points
    public void SpawnAllEnemies()
    {
        foreach (EnemyData enemyData in enemiesData.enemies)
        {
            SpawnEnemy(enemyData);
        }
    }

    private void SetStats(EnemyAIScript enemyController, EnemyStats stats)
    {
        enemyController.health = stats.health;
        enemyController.attackDamage = stats.attackDamage;
        enemyController.attackRange = stats.attackRange;
        enemyController.attackSpeed = stats.attackSpeed;
        enemyController.attackTimer = stats.attackTimer;
    }

    private void PlaceEnemy(EnemyAIScript enemy, EnemyPosition position)
    {
        enemy.transform.position = new Vector3(position.x, position.y, position.z);
    }
}
