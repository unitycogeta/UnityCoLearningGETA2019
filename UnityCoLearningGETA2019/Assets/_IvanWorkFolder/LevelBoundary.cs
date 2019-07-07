using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelBoundary : MonoBehaviour
{
    public GameController gameController;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        var player = collision.gameObject.GetComponent<CharacterController>();
        if(player != null)
        {
            player.PerformSoftReset();
            return;
        }
        var ejectedSand = collision.gameObject.GetComponent<EjectedSand>();
        if(ejectedSand != null)
        {
            if (gameController.allEjectedSand.Contains(ejectedSand)) {
                gameController.allEjectedSand.Remove(ejectedSand);
            }
            GameObject.Destroy(ejectedSand.gameObject);
            return;
        }
        var movableElement = collision.gameObject.GetComponent<MovableElement>();
        if(movableElement != null)
        {
            movableElement.gameObject.SetActive(false);
            return;
        }

        
    }
}
