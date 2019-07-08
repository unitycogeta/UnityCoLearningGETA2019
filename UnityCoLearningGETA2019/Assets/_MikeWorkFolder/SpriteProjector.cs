using System.Collections;
using UnityEngine;

public class SpriteProjector : MonoBehaviour
{
    private SceneLoader nextScene;

    [SerializeField]
    private float switchTime;
    [SerializeField]
    private float steadyTime;

    [SerializeField]
    private int nextSpriteIndex;

    private int spriteCount;

    [SerializeField]
    private SpriteCollection spriteCollection;

    [SerializeField]
    float alphaValue;

    [SerializeField]
    SpriteRenderer spriteRenderer1;

    [SerializeField]
    SpriteRenderer spriteRenderer2;

    bool firstSpriteRendererActive;

    private void Awake()
    {
        GetSpriteRenderers();
        firstSpriteRendererActive = true;
        nextSpriteIndex = 1;
        nextScene = GetComponent<SceneLoader>();
        spriteCount = spriteCollection.sprites.Count;
    }

    private void Start()
    {
        Begin();
    }

    private void Begin()
    {
        StartCoroutine(FadeOut());
    }

    private void GetStartingSprites()
    {
        spriteRenderer1.sprite = spriteCollection.sprites[0];
        spriteRenderer2.sprite = spriteCollection.sprites[1];
    }


    private void GetSpriteRenderers()
    {
        spriteRenderer1 = transform.GetChild(0).GetComponent<SpriteRenderer>();
        spriteRenderer2 = transform.GetChild(1).GetComponent<SpriteRenderer>();
    }

    private void NewSprite()
    {
        if (nextSpriteIndex < spriteCount)
        {
            spriteRenderer2.sprite = spriteCollection.sprites[nextSpriteIndex];
            StartCoroutine(FadeOut());
        }
        else
        {
            StartCoroutine(LaunchNextScene());
        }
    }

    IEnumerator LaunchNextScene()
    {
        yield return new WaitForSeconds(switchTime);
        nextScene.LoadSCene();
    }


    private IEnumerator FadeOut()
    {
        Color tempColor = spriteRenderer1.color;
        float alpha = 1+steadyTime / switchTime;
        while (tempColor.a > 0)
        {
            alpha -= Time.deltaTime / switchTime;
            tempColor.a = Mathf.Min(alpha,1);
            spriteRenderer1.color = tempColor;

            yield return null;
        }
        Next();
    }


    private void Next()
    {
        Debug.Log("Next: " + Time.time);
        spriteRenderer1.sprite = spriteRenderer2.sprite;
        spriteRenderer1.color = Color.white;
        nextSpriteIndex++;
        NewSprite();
    }
}
