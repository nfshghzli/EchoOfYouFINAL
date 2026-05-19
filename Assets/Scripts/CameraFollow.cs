using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player;

    private float fixedY;
    private float fixedZ;

    void Start()
    {
        fixedY = transform.position.y;
        fixedZ = transform.position.z;
    }

    void LateUpdate()
    {
        transform.position = new Vector3(
            player.position.x + 5f,
            fixedY,
            fixedZ
        );
    }
}