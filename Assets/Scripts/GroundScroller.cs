using UnityEngine;

public class GroundScroller : MonoBehaviour
{
    public float speed = 5f;
    public Transform[] tiles;
    public float tileWidth = 10f;

    void Update()
    {
        foreach (Transform tile in tiles)
        {
            tile.position += Vector3.left * speed * Time.deltaTime;

            if (tile.position.x <= -tileWidth)
            {
                MoveToEnd(tile);
            }
        }
    }

    void MoveToEnd(Transform tile)
    {
        float rightMost = GetRightMostX();

        tile.position = new Vector3(
            rightMost + tileWidth,
            tile.position.y,
            tile.position.z
        );
    }

    float GetRightMostX()
    {
        float maxX = float.MinValue;

        foreach (Transform tile in tiles)
        {
            if (tile.position.x > maxX)
                maxX = tile.position.x;
        }

        return maxX;
    }
}
