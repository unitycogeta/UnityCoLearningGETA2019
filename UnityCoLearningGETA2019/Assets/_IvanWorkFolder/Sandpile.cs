using UnityEngine;
using UnityEditor;
using System;

public class Sandpile : MonoBehaviour
{
    public GameObject pileMesh;
    public int maxPileSize;
    internal int currentPileSize;

    private void Start()
    {
        Reset();
    }

    public void Reset()
    {
        currentPileSize = maxPileSize;
        pileMesh.transform.localScale = Vector3.one * currentPileSize;
    }

    public int DrainPile(int drainAmount)
    {
        drainAmount = Mathf.Min(drainAmount, currentPileSize);
        currentPileSize -= drainAmount;
        pileMesh.transform.localScale = Vector3.one * currentPileSize;
        return drainAmount;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        var characterController = collision.gameObject.GetComponent<CharacterController>();
        if (characterController != null)
        {
            characterController.checkpoint = this;
            characterController.inRangeOfSandpile = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        var characterController = collision.gameObject.GetComponent<CharacterController>();
        if (characterController != null)
        {
            characterController.inRangeOfSandpile = false;
        }
    }
}