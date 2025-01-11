using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


public class ObstacleFactory : MonoBehaviour
{
    public GameObject[] obstaclePrefabs; // Array to hold your prefab references
    public GameObject obstaclePrefab; // Default prefab to use if no array is needed
    public ObstaclesData obstaclesData; // Array of obstacle data

    public List<GameObject> obstacles = new List<GameObject>(); // List to hold all obstacles in the scene

    // Method to spawn an obstacle
    public void SpawnObstacle(ObstacleData obstacleData)
    {
        int prefabIndex = obstacleData.id;
        Vector3 position = new Vector3(obstacleData.position.x, obstacleData.position.y, obstacleData.position.z);
        Vector3 rotationVector = new Vector3(obstacleData.rotation.x, obstacleData.rotation.y, obstacleData.rotation.z);
        Quaternion rotation = Quaternion.Euler(rotationVector);

        if (prefabIndex < 0 || prefabIndex >= obstaclePrefabs.Length)
        {
            Debug.LogError("Prefab index out of range.");
            return;
        }

        GameObject obstacle = Instantiate(obstaclePrefabs[prefabIndex], position, rotation);
        obstacle.AddComponent<Rigidbody>();
        Rigidbody rg = obstacle.GetComponent<Rigidbody>();
        rg.useGravity = false;
        rg.isKinematic = true;



        PlaceObstacle(obstacle, obstacleData.position);


        // We add the tag "Obstacle" to the obstacle object
        obstacle.tag = "Obstacle";

        obstacles.Add(obstacle);

    }

    public void ResetObstacles()
    {
        // Destroy all obstacles in the scene
        foreach (GameObject obstacle in obstacles)
        {
            Destroy(obstacle);
        }
        obstacles.Clear();
        obstaclesData.obstacles.Clear();
    }
    
    public void AddObstacleData(ObstacleData obstacleData)
    {
        // Add an obstacle to the scene
        obstaclesData.obstacles.Add(obstacleData);
    }

    // Example method to spawn at predefined spawn points
    public void SpawnAllObstacles()
    {
        foreach (ObstacleData obstacleData in obstaclesData.obstacles)
        {
            SpawnObstacle(obstacleData);
        }
    }


    private void PlaceObstacle(GameObject obstacle, Position position)
    {
        obstacle.transform.position = new Vector3(position.x, position.y, position.z);
    }
}
