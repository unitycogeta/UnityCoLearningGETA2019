using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    [SerializeField]
    public Object sceneToLoad;

    [HideInInspector]
    public string sceneString;

    public void LoadSCene()
    {
        SceneManager.LoadScene(sceneString);
    }
    
}
