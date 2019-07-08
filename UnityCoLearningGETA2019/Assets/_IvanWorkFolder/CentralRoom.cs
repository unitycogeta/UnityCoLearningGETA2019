using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CentralRoom : MonoBehaviour
{
    public GameController gameController;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Debug.Log(collision.name);
        //Debug.Log(collision.gameObject.name);
        //Debug.Log(collision.gameObject.GetComponent<MovableElement>());
        if (collision.gameObject.tag == "Finish")
        {
            var movable = collision.gameObject.GetComponent<MovableElement>();
            gameController.CompleteGoal(movable);
        }
    }
}
