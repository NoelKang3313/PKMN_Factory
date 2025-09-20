using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Trainer : ScriptableObject
{
    public string Name;
    public Sprite Sprite;
    public Pokemon[] Pokemon = new Pokemon[3];
    public bool IsSpecialTrainer;

    void OnEnable()
    {
        if (IsSpecialTrainer)
            return;

        Initialize();
    }

    void Initialize()
    {
        for(int i = 0; i < this.Pokemon.Length; i++)
        {
            Pokemon[i] = null;
        }
    }
}
