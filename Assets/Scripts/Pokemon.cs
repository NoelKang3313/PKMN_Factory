using UnityEngine;

[CreateAssetMenu]
public class Pokemon : ScriptableObject
{
    [Header("Pokemon Info")]
    public string Name;
    public int PokedexNumber;
    public string Gender;
    public bool Genderless;    
    public Sprite Regular;
    public Sprite Regular_Back;    
    public Sprite Icon_Regular;
    public string Type1;
    public string Type2;
    public string Nature;
    public string[] Abilitys = new string[2];   // 특성들
    public string Ability;  // 보유 특성
    public AudioClip PokemonCry;


    [Header("Different Form")]
    public bool hasDifferentForm;
    public Sprite NewForm1;
    public Sprite NewForm1_Back;
    public Sprite NewForm2;
    public Sprite NewForm2_Back;
    public Sprite NewForm3;
    public Sprite NewForm3_Back;
    public Sprite NewForm_Icon1;
    public Sprite NewForm_Icon2;
    public Sprite NewForm_Icon3;
    public string NewAbility1;
    public string NewAbility2;
    public string NewAbility3;
    public AudioClip NewPokemonCry1;
    public AudioClip NewPokemonCry2;
    public AudioClip NewPokemonCry3;

    [Header("Pokemon Stats")]
    public int HP;
    public int Attack;
    public int Defense;
    public int SAttack;
    public int SDefense;
    public int Speed;

    [Header("Pokemon Move Name")]
    public string[] MoveName = new string[4];

    [Header("Pokemon Move Type")]
    public string[] MoveType = new string[4];

    [Header("Pokemon Move Category")]
    public string[] MoveCategory = new string[4];

    [Header("Pokemon Move Power")]
    public int[] MovePower = new int[4];

    [Header("Pokemon Move Accuracy")]
    public int[] MoveAccuracy = new int[4];

    [Header("Pokemon Move Current PP")]
    public int[] CurrentMovePP = new int[4];

    [Header("Pokemon Move Full PP")]
    public int[] FullMovePP = new int[4];

    private void OnEnable()
    {
        for(int i = 0; i < CurrentMovePP.Length; i++)
        {
            CurrentMovePP[i] = FullMovePP[i];
        }

        Gender = null;
        Nature = null;
        Ability = null;
    }
}
