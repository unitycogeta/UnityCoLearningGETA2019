using UnityEngine;
using System.Collections;

public class EjectedSand : MonoBehaviour
{
    internal bool isAbsorable;

    private void Start()
    {
        StartCoroutine("WaitAndPrint",1);
    }

    private IEnumerator WaitAndPrint(float waittime)
    {
        yield return new WaitForSeconds(waittime);
        isAbsorable = true;
    }

    internal void Absorb()
    {
        Destroy(gameObject);
    }
}