using UnityEngine;

public class ParallaxScroller : MonoBehaviour
{
    public float speed = 2f;
    public Transform[] backgrounds;
    public float backgroundWidth = 20f;

    void Update()
    {
        foreach (Transform bg in backgrounds)
        {
            bg.position += Vector3.left * speed * Time.deltaTime;

            if (bg.position.x <= -backgroundWidth)
            {
                MoveToEnd(bg);
            }
        }
    }

    void MoveToEnd(Transform bg)
    {
        float rightMost = GetRightMostX();

        bg.position = new Vector3(
            rightMost + backgroundWidth,
            bg.position.y,
            bg.position.z
        );
    }

    float GetRightMostX()
    {
        float maxX = float.MinValue;

        foreach (Transform bg in backgrounds)
        {
            if (bg.position.x > maxX)
                maxX = bg.position.x;
        }

        return maxX;
    }
}
