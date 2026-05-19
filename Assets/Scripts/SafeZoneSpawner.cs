using UnityEngine;

public class SafeZoneSpawner : MonoBehaviour
{
    public GameObject safeZonePrefab;

    public float spawnTime = 25f;

    public string nextSceneName;

    private bool spawned = false;

    private float timer = 0f;

    void Update()
    {
        timer += Time.deltaTime;

        if (!spawned && timer >= spawnTime)
        {
            spawned = true;

            SpawnSafeZone();
        }
    }

    void SpawnSafeZone()
    {
        GameObject safeZone = Instantiate(
            safeZonePrefab,
            new Vector3(15f, 0f, 0f),
            Quaternion.identity
        );

        SafeZone safeZoneScript =
            safeZone.GetComponent<SafeZone>();

        safeZoneScript.nextScene = nextSceneName;
    }
}