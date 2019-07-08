using UnityEngine;

public class PersistentAudioSources : MonoBehaviour
{
    public static PersistentAudioSources Instance { get; private set; }

    public int Value;

    [SerializeField]
    private MusicPlayer musicPLayer;

    [SerializeField]
    private SFXPlayer sfxPLayer;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        ExtendAudioSources();
    }

    private void ExtendAudioSources()
    {
        musicPLayer.music = transform.GetChild(0).GetComponent<AudioSource>();
        sfxPLayer.sound = transform.GetChild(1).GetComponent<AudioSource>();
    }
}
