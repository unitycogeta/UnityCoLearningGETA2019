using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu]
public class SpriteCollection : ScriptableObject
{
    [SerializeField]
    public List<Sprite> sprites;
}
