using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public Pokemon CurrentPokemon;   // 현재 나와서 싸우고 있는 포켓몬    
    public List<Pokemon> MyPokemons = new List<Pokemon>();  // 가지고 있는 포켓몬

    private List<ScriptableObject> pokemonScriptableObject = new List<ScriptableObject>();    // 포켓몬 ScriptableObjects

    public List<Pokemon> PokemonLists = new List<Pokemon>();   // 총 포켓몬

    public List<Pokemon> RandomPokemon = new List<Pokemon>();// 랜덤 포켓몬 6마리 지정
    public bool[] RandomPokemonSelected = new bool[6];  // 포켓몬 대여 선택되었음을 확인

    private string[] pokemonNatures;    // 포켓몬 성격
    private List<Dictionary<string, object>> pokemonAbility;    // 포켓몬 특성 저장
    public string[] AbilityDescription = new string[6]; // 포켓몬 특성 설명

    public bool PokemonSelection;   // 포켓몬 대여 구간 진입
    public bool FactoryPokemonSelection; // 포켓몬 첫번째 대여 (처음에는 3마리 선택, 이후에 추가)
    public bool Maintenance;    // 포켓몬 배틀 전 유지/정비/샵
    public bool BattleStart;    // 포켓몬 배틀 시작

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
            PokemonLists.Add(pokemons);
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
    }

    void Start()
    {
        //CurrentPokemon = MyPokemons[0];

        PokemonSelection = true;
        FactoryPokemonSelection = true;

        //BattleStart = true;

        PokemonRandom();
    }

    // 팩토리 포켓몬 랜덤 선택 및 성격 결정
    void PokemonRandom()
    {
        for(int i = 0; i < 6; i++)
        {
            int random = Random.Range(0, PokemonLists.Count);

            int natureRandom = Random.Range(0, pokemonNatures.Length);

            RandomPokemon.Add(PokemonLists[random]);
            RandomPokemon[i].Nature = pokemonNatures[natureRandom];

            if (RandomPokemon[i].Abilitys[1] == "")
            {
                RandomPokemon[i].Ability = RandomPokemon[i].Abilitys[0];
            }
            else
            {
                int abilityRandom = Random.Range(0, RandomPokemon[i].Abilitys.Length);

                RandomPokemon[i].Ability = RandomPokemon[i].Abilitys[abilityRandom];
            }

            SetPokemonGender(i);

            PokemonLists.RemoveAt(random);
        }

        SetPokemonAbilityDescription();
    }

    // 성별 결정
    void SetPokemonGender(int number)
    {
        int genderRandom = Random.Range(1, 101);

        if (RandomPokemon[number].Genderless)
        {
            return;
        }
        else
        {
            if (genderRandom <= 50)
            {
                RandomPokemon[number].Gender = "Male";
            }
            else
            {
                RandomPokemon[number].Gender = "Female";
            }
        }
    }

    // CSV파일을 통해 포켓몬 특성을 가져와 특성 설명 문자열 불러오기
    void SetPokemonAbilityDescription()
    {
        for (int i = 0; i < RandomPokemon.Count; i++)
        {
            for (int j = 0; j < pokemonAbility.Count; j++)
            {
                if (RandomPokemon[i].Ability == pokemonAbility[j]["Ability"].ToString())
                {
                    AbilityDescription[i] = pokemonAbility[j]["Description"].ToString();
                }
            }
        }
    }
}
