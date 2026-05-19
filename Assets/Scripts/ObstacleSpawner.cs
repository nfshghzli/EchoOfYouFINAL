using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    public GameObject[] obstacles;

    public float spawnRate = 2f;

    private float timer;

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= spawnRate)
        {
            SpawnObstacle();

            timer = 0;
        }
    }

    void SpawnObstacle()
    {
        int randomIndex =
            Random.Range(0, obstacles.Length);

        GameObject obstaclePrefab =
            obstacles[randomIndex];

        // Use prefab's original Y position
        Vector3 spawnPos = new Vector3(
            transform.position.x,
            obstaclePrefab.transform.position.y,
            0
        );

        Instantiate(
            obstaclePrefab,
            spawnPos,
            Quaternion.identity
        );
    }
}