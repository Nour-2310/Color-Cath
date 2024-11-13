using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpSpawner : MonoBehaviour
{
    public GameObject CorrectPickUpPrefab;
    public GameObject WrongPickUpPrefab;
    public int correctPickUpCount = 6;
    public int wrongPickUpCount = 6;
    public Vector3 spawnAreaSize = new Vector3(20f, 0f, 20f); // Define the X and Z range of the spawning area
    public float minimumDistance = 3f; // Minimum distance between pickups

    private List<Vector3> spawnedPositions = new List<Vector3>();

    void Start()
    {
        SpawnPickUps(CorrectPickUpPrefab, correctPickUpCount, "CorrectPickUp");
        SpawnPickUps(WrongPickUpPrefab, wrongPickUpCount, "WrongPickUp");
    }

    void SpawnPickUps(GameObject prefab, int count, string tag)
    {
        for (int i = 0; i < count; i++)
        {
            Vector3 spawnPosition;
            bool positionIsValid;

            // Try to find a valid position
            do
            {
                spawnPosition = GenerateRandomPosition();
                
                positionIsValid = true;
                foreach (Vector3 pos in spawnedPositions)
                {
                    if (Vector3.Distance(pos, spawnPosition) < minimumDistance)
                    {
                        positionIsValid = false;
                        break;
                    }
                }
            } while (!positionIsValid);

            // Instantiate the pickup at the spawn position
            GameObject spawnedPickUp = Instantiate(prefab, spawnPosition, Quaternion.identity);
            if (spawnedPickUp != null)
            {
                spawnedPickUp.tag = tag;
                spawnedPositions.Add(spawnPosition); // Add to list of spawned positions
            }
            else
            {
                Debug.LogError("Failed to instantiate PickUpPrefab!");
            }
        }
    }

    Vector3 GenerateRandomPosition()
    {
        // Generate a random position within the defined spawn area size
        return new Vector3(
            Random.Range(-spawnAreaSize.x / 2, spawnAreaSize.x / 2),
            0.5f, // Adjust the Y position to be slightly above the ground
            Random.Range(-spawnAreaSize.z / 2, spawnAreaSize.z / 2)
        );
    }
}
