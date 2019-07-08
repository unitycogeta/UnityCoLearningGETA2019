using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public int maxEjectedSand = 10;
    private Sandpile[] allSandpiles;
    internal List<EjectedSand> allEjectedSand;
    private List<MovableElement> allMovables;
    internal int completedGoals = 0;
    public int allGoals = 3;
    private Vector3[] goalPositions = new Vector3[] { new Vector3(-0f, -4f,1), new Vector3(-1f, -4f, 1f), new Vector3(2f, -4f, 1) };
    private SceneLoader sceneLoader;

    private void Start()
    {
        allSandpiles = FindObjectsOfType<Sandpile>();
        allMovables = FindObjectsOfType<MovableElement>().ToList();
        allEjectedSand = new List<EjectedSand>();
        sceneLoader = GetComponent<SceneLoader>();
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
            var excessSand = allEjectedSand[0];

            allEjectedSand.Remove(excessSand);
            excessSand.Absorb();
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

    public void CompleteGoal(MovableElement element)
    {
        element.transform.position = goalPositions[completedGoals];
        element.transform.rotation = Quaternion.identity;
        element.GetComponent<Rigidbody2D>().Sleep();

        allMovables.Remove(element);
        
        completedGoals++;
        if(completedGoals == allGoals)
        {

            FinishGame();
        }
    }

    private void FinishGame()
    {
        sceneLoader.LoadSCene();

    }
}