using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Trainer : ScriptableObject
{
    public string Name;
    public Sprite Sprite;
    public Pokemon[] Pokemon = new Pokemon[3];
}
