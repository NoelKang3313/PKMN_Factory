using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public bool isFirstSelection = true;

    public static GameManager instance;

    public Pokemon CurrentPokemon;   // 현재 나와서 싸우고 있는 포켓몬    
    public List<Pokemon> MyPokemons = new List<Pokemon>();  // 가지고 있는 포켓몬

    private List<ScriptableObject> pokemonScriptableObject = new List<ScriptableObject>();    // 포켓몬 ScriptableObjects

    public List<Pokemon> PokemonLists = new List<Pokemon>();   // 총 포켓몬 (전설 제외)
    public List<Pokemon> LegendaryPokemonLists = new List<Pokemon>();   // 전설 포켓몬

    public string[] pokemonNatures;    // 포켓몬 성격
    public List<Dictionary<string, object>> pokemonAbility;    // 포켓몬 특성 저장

    public List<Dictionary<string, object>> PokemonMove;   // 포켓몬 기술 저장

    public bool PokemonSelection;   // 포켓몬 대여 구간 진입
    public bool FactoryPokemonSelection; // 포켓몬 첫번째 대여 (처음에는 3마리 선택, 이후에 추가)
    public bool isFirstBattle = true;
    public bool BattleStart;    // 포켓몬 배틀 시작

    public AudioSource BGMAudioSource;
    public AudioClip CurrentClip;
    public AudioClip FactoryClip;
    public AudioClip TrainerBattleClip;
    //public bool isPlay;

    void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }

        ScriptableObject[] pokemonAssets = Resources.LoadAll<ScriptableObject>("Pokemons");

        foreach (var psc in pokemonAssets)
        {
            pokemonScriptableObject.Add(psc);
        }

        foreach (Pokemon pokemons in pokemonScriptableObject)
        {
            if(pokemons.isLegendary)
            {
                LegendaryPokemonLists.Add(pokemons);
            }
            else
            {
                PokemonLists.Add(pokemons);
            }
        }

        pokemonNatures = new string[25]
        {
            "노력", "외로움", "고집", "개구쟁이", "용감",
            "대담", "온순", "장난꾸러기", "촐랑", "무사태평",
            "조심", "의젓", "수줍음", "덜렁", "냉정",
            "차분", "얌전", "신중", "변덕", "건방",
            "겁쟁이", "성급", "명랑", "천진난만", "성실"
        };

        pokemonAbility = CSVReader.Read("Ability");
        PokemonMove = CSVReader.Read("Pokemon Moves");

        //isPlay = true;
        CurrentClip = FactoryClip;
    }

    void Start()
    {
        //CurrentPokemon = MyPokemons[0];

        PokemonSelection = true;
        FactoryPokemonSelection = true;

        //BattleStart = true;

        //Debug.Log(pokemonMove[427]["MoveName"]);
    }

    void Update()
    {
        if (BattleStart)
        {
            CheckAudioSource(TrainerBattleClip);
        }
    }

    // CSV파일을 통해 포켓몬 특성을 가져와 특성 설명 문자열 불러오기
    public string SetPokemonAbility(int number, List<Pokemon> pokemon)
    {
        for(int i = 0; i < pokemonAbility.Count; i++)
        {
            if(pokemon[number].Ability == pokemonAbility[i]["Ability"].ToString())
            {
                return pokemonAbility[i]["Description"].ToString();
            }
        }

        return null;
    }

    public void CheckAudioSource(AudioClip nextClip)
    {
        if(CurrentClip != nextClip)
        {
            BGMAudioSource.clip = nextClip;
            BGMAudioSource.PlayDelayed(1.0f);
            CurrentClip = nextClip;
        }
    }
}