using UnityEngine;

public class EntityFollow : MonoBehaviour
{

    public Transform player;



    [Header("Movement")]
    public float normalSpeed = 2f;
    public float panicSpeed = 12f;



    [Header("Distance")]
    public float followDistance = 6f;
    public float catchDistance = 0.7f;



    private bool panicMode = false;




    void Update()
    {

        if(player == null)
            return;



        if(!panicMode)
        {

            // Stay behind player

            Vector3 targetPosition = new Vector3(
                player.position.x - followDistance,
                transform.position.y,
                transform.position.z
            );



            transform.position = Vector3.Lerp(
                transform.position,
                targetPosition,
                normalSpeed * Time.deltaTime
            );

        }



        else
        {

            // Chase player

            transform.position = Vector3.MoveTowards(
                transform.position,
                player.position,
                panicSpeed * Time.deltaTime
            );



            CheckCatch();

        }

    }






    void CheckCatch()
    {

        float distance = Vector2.Distance(
            transform.position,
            player.position
        );



        if(distance <= catchDistance)
        {

            PlayerController controller =
                player.GetComponent<PlayerController>();


            if(controller != null)
            {

                Debug.Log("ENTITY CAUGHT SAYY!");

                controller.Die();

            }

        }

    }







    public void SetPanic(bool state)
    {

        panicMode = state;



        if(state)
        {
            Debug.Log("ENTITY PANIC MODE");
        }
        else
        {
            Debug.Log("ENTITY CALMED");
        }

    }

}