using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public int maxEjectedSand = 10;
    private Sandpile[] allSandpiles;
    internal List<EjectedSand> allEjectedSand;
    private MovableElement[] allMovables;

    private void Start()
    {
        allSandpiles = FindObjectsOfType<Sandpile>();
        allMovables = FindObjectsOfType<MovableElement>();
        allEjectedSand = new List<EjectedSand>();
    }

    private void Update()
    {
        if (Input.GetButtonDown("FullReset"))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    public void AddEjectedSand(EjectedSand eject)
    {
        allEjectedSand.Add(eject);

        if(allEjectedSand.Count > maxEjectedSand)
        {
            allEjectedSand.RemoveRange(0, allEjectedSand.Count - maxEjectedSand);
        }
        Debug.Log(allEjectedSand.Count);
        
    }

    public void PerformCheckpointReset()
    {
        foreach(var sandpile in allSandpiles)
        {
            sandpile.Reset();
        }
        foreach(var ejectedSand in allEjectedSand.ToArray())
        {
            allEjectedSand.Remove(ejectedSand);
            ejectedSand.Absorb();
        }
        foreach(var movable in allMovables)
        {
            movable.ResetToInitial();
        }
    }

}