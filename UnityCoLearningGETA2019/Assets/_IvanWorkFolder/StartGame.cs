using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartGame : MonoBehaviour
{
    public SceneLoader sceneLoader;
    public Image buttonImage;
    public float pulseSpeed;

    private bool startingLevel;
    // Update is called once per frame
    void Update()
    {
        if (!startingLevel)
        {
            var color = buttonImage.color;
            color.a = 0.5f * (1f - Mathf.Cos(Time.time * pulseSpeed));
            buttonImage.color = color;
        }
    }

    public void StartLevel()
    {
        startingLevel = true;
        StartCoroutine(FadeAndStart());
    }

    IEnumerator FadeAndStart()
    {
        while(buttonImage.color.a > 0)
        {
            var color = buttonImage.color;
            color.a -= Time.deltaTime * pulseSpeed / 6f;
            buttonImage.color = color;
            yield return null;
        }
        sceneLoader.LoadSCene();
    }
}
