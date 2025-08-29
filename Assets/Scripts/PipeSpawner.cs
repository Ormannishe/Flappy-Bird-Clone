// 2025-08-25 AI-Tag
// This was created with the help of Assistant, a Unity Artificial Intelligence product.

using UnityEngine;

public class PipeSpawner : MonoBehaviour
{
    public GameObject pipePrefab; // The pipe prefab to spawn
    public float initialSpawnInterval = 3; // Time between spawns
    public float spawnIntervalDecreaseRate = 0.025f;
    public float minimumSpawnInterval = 1.5f;
    public float minY = -1f; // Minimum vertical position of the gap
    public float maxY = 2f; // Maximum vertical position of the gap

    private float timer = 0f;
    private float currentSpawnInterval;

    void Start()
    {
        currentSpawnInterval = initialSpawnInterval;
    }

    void Update()
    {
        // Increment the timer
        timer += Time.deltaTime;

        // Check if it's time to spawn a new pipe
        if (timer >= currentSpawnInterval)
        {
            SpawnPipe();
            timer = 0f; // Reset the timer

            // Adjust the spawn interval
            if (currentSpawnInterval > minimumSpawnInterval)
            {
                currentSpawnInterval -= spawnIntervalDecreaseRate;
            }
        }
    }

    void SpawnPipe()
    {
        // Randomize the vertical position of the pipe gap
        float randomY = Random.Range(minY, maxY);

        // Spawn the pipe prefab at the spawner's position with a random Y offset
        Vector3 spawnPosition = new Vector3(transform.position.x, randomY, 0);
        Instantiate(pipePrefab, spawnPosition, Quaternion.identity);
    }
}