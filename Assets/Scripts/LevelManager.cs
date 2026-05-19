using UnityEngine;
using TMPro;

public class LevelManager : MonoBehaviour
{
    [Header("UI")]
    public TextMeshProUGUI levelText;

    [Header("Level ID (set per scene manually)")]
    public int levelID = 1;

    [Header("References")]
    public ObstacleSpawner obstacleSpawner;
    public WhisperSystem whisperSystem;
    public EntityFollow entity;

    void Start()
    {
        ApplyLevelSettings();
    }

    void ApplyLevelSettings()
    {
        switch (levelID)
        {
            case 1:
                levelText.text = "LEVEL 1 — DENIAL";
                obstacleSpawner.spawnRate = 3f;
                whisperSystem.minTriggerTime = 12f;
                whisperSystem.maxTriggerTime = 15f;
                entity.normalSpeed = 1.5f;
                break;

            case 2:
                levelText.text = "LEVEL 2 — PARANOIA";
                obstacleSpawner.spawnRate = 2.3f;
                whisperSystem.minTriggerTime = 9f;
                whisperSystem.maxTriggerTime = 11f;
                entity.normalSpeed = 2.5f;
                break;

            case 3:
                levelText.text = "LEVEL 3 — CONFRONTATION";
                obstacleSpawner.spawnRate = 1.8f;
                whisperSystem.minTriggerTime = 7f;
                whisperSystem.maxTriggerTime = 9f;
                entity.normalSpeed = 3.5f;
                break;

            case 4:
                levelText.text = "FINAL LEVEL — ACCEPTANCE";
                obstacleSpawner.spawnRate = 1.3f;
                whisperSystem.minTriggerTime = 5f;
                whisperSystem.maxTriggerTime = 7f;
                entity.normalSpeed = 5f;
                break;
        }
    }
}