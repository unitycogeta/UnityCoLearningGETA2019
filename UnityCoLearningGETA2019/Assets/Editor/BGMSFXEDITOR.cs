using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(MusicPlayer))]
public class BGMSFXEDITOR : Editor
{
    int songCount;
    float[] volume;

    public override void OnInspectorGUI()
    {
        songCount = (target as MusicPlayer).songlist.Collection.Count;
        volume = new float[songCount];

        base.OnInspectorGUI();

        GUILayout.Label("");

        for (int i = 0; i < (target as MusicPlayer).songlist.Collection.Count; i++ )
        {
            AudioClip audioClip = (target as MusicPlayer).songlist.Collection[i].audioCLip;
            GUILayout.BeginHorizontal();
            GUILayout.Label(i + " : " + audioClip.name + "  Volume: ");

            (target as MusicPlayer).songlist.Collection[i].volume = GUILayout.HorizontalSlider((target as MusicPlayer).songlist.Collection[i].volume, 0, 100, GUILayout.Width(100));
            GUILayout.Label(" ");
            GUILayout.Label(Mathf.FloorToInt((target as MusicPlayer).songlist.Collection[i].volume).ToString());
            
            GUILayout.EndHorizontal();
        }
    
    }
}
