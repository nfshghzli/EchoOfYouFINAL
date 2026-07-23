using UnityEngine;

public class UrgeTrigger : MonoBehaviour
{
    public WhisperSystem whisperSystem;

    [Tooltip("Override the default reaction time if needed.")]
    public float customReactionTime = -1f;

    private bool triggered = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (triggered)
            return;

        if (!other.CompareTag("Player"))
            return;

        triggered = true;

        if (customReactionTime > 0)
            whisperSystem.reactionTime = customReactionTime;

        whisperSystem.TriggerWhisper();

        Destroy(gameObject);
    }
}