using UnityEngine;

public class IntroScene : MonoBehaviour
{
    [SerializeField]
    private MusicPlayer musicPLayer;

    private void Start()
    {
        musicPLayer.PlaySong(0);
    }
}
