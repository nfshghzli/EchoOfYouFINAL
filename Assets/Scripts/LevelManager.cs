using UnityEngine;
using TMPro;

public class LevelManager : MonoBehaviour
{
    [Header("UI")]
    public TextMeshProUGUI levelText;

    [Header("Level ID")]
    public int levelID = 1;

    [Header("References")]
    
    public EntityFollow entity;


    void Start()
    {
        ApplyLevelSettings();
    }


    void ApplyLevelSettings()
    {
        switch(levelID)
        {
            case 1:

                levelText.text = "LEVEL 1 — DENIAL";


                entity.normalSpeed = 1.5f;

                break;


            case 2:

                levelText.text = "LEVEL 2 — PARANOIA";

                entity.normalSpeed = 2.5f;

                break;


            case 3:

                levelText.text = "LEVEL 3 — CONFRONTATION";

                entity.normalSpeed = 3.5f;

                break;


            case 4:

                levelText.text = "FINAL LEVEL — ACCEPTANCE";

                entity.normalSpeed = 5f;

                break;
        }
    }
}