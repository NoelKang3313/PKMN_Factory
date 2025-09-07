using System.Collections.Generic;
using UnityEngine;

public class PokemonManager : MonoBehaviour
{
    // 랜덤 포켓몬 설정
    public List<Pokemon> RandomPokemon = new List<Pokemon>();  // 랜덤 포켓몬
    public bool[] RandomPokemonSelected = new bool[6]; // 랜덤 포켓몬 중 대여 확인

    [Header("Type Images")]
    public Sprite[] PokemonTypeSprites = new Sprite[18];
    public Dictionary<string, Sprite> PokemonTypes;

    void Awake()
    {
        PokemonTypes = new Dictionary<string, Sprite>
        {
            { "벌레", PokemonTypeSprites[0] },
            { "악", PokemonTypeSprites[1] },
            { "드래곤", PokemonTypeSprites[2] },
            { "전기", PokemonTypeSprites[3] },
            { "페어리", PokemonTypeSprites[4] },
            { "격투", PokemonTypeSprites[5] },
            { "불꽃", PokemonTypeSprites[6] },
            { "비행", PokemonTypeSprites[7] },
            { "고스트", PokemonTypeSprites[8] },
            { "풀", PokemonTypeSprites[9] },
            { "땅", PokemonTypeSprites[10] },
            { "얼음", PokemonTypeSprites[11] },
            { "노말", PokemonTypeSprites[12] },
            { "독", PokemonTypeSprites[13] },
            { "에스퍼", PokemonTypeSprites[14] },
            { "바위", PokemonTypeSprites[15] },
            { "강철", PokemonTypeSprites[16] },
            { "물", PokemonTypeSprites[17] }
        };
    }

    void Start()
    {
        SetRandomPokemon();
    }

    // Pokemon Manager
    // 대여 가능한 포켓몬 랜덤으로 설정
    void SetRandomPokemon()
    {
        if (GameManager.instance.isFirstSelection)
        {
            for (int i = 0; i < 6; i++)
            {
                int pokemonRandom = Random.Range(0, GameManager.instance.PokemonLists.Count);
                RandomPokemon.Add(GameManager.instance.PokemonLists[pokemonRandom]);

                GameManager.instance.PokemonLists.RemoveAt(pokemonRandom);

                int natureRandom = Random.Range(0, GameManager.instance.pokemonNatures.Length);

                RandomPokemon[i].Nature = GameManager.instance.pokemonNatures[natureRandom];

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
            }
        }
    }

    // Pokemon Manager
    // 랜덤 포켓몬 성별 설정
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

        SetSingleGender(number);
    }

    // Pokemon Manager
    // 단일 성별 포켓몬 설정
    void SetSingleGender(int number)
    {
        // 남성
        if (RandomPokemon[number].Name == "니드킹" || RandomPokemon[number].Name == "볼비트" ||
            RandomPokemon[number].Name == "라티오스" || RandomPokemon[number].Name == "엘레이드" ||
                RandomPokemon[number].Name == "시라소몬" || RandomPokemon[number].Name == "홍수몬" ||
                RandomPokemon[number].Name == "카포에라" || RandomPokemon[number].Name == "켄타로스" ||
                RandomPokemon[number].Name == "던지미" || RandomPokemon[number].Name == "타격귀" ||
                RandomPokemon[number].Name == "워글" || RandomPokemon[number].Name == "토네로스" ||
                RandomPokemon[number].Name == "볼트로스" || RandomPokemon[number].Name == "랜드로스" ||
                RandomPokemon[number].Name == "오롱털" || RandomPokemon[number].Name == "조타구" ||
                RandomPokemon[number].Name == "이야후" || RandomPokemon[number].Name == "기로치")
        {
            RandomPokemon[number].Gender = "Male";
        }
        //여성
        else if (RandomPokemon[number].Name == "니드퀸" || RandomPokemon[number].Name == "네오비트" ||
            RandomPokemon[number].Name == "라티아스" || RandomPokemon[number].Name == "눈여아" ||
            RandomPokemon[number].Name == "도롱마담" || RandomPokemon[number].Name == "비퀸" ||
            RandomPokemon[number].Name == "염뉴트" || RandomPokemon[number].Name == "해피너스" ||
            RandomPokemon[number].Name == "캥카" || RandomPokemon[number].Name == "루주라" ||
            RandomPokemon[number].Name == "밀탱크" || RandomPokemon[number].Name == "크레세리아" ||
            RandomPokemon[number].Name == "드레디어" || RandomPokemon[number].Name == "버랜지나" ||
            RandomPokemon[number].Name == "플라제스" || RandomPokemon[number].Name == "달코퀸" ||
            RandomPokemon[number].Name == "브리무음" || RandomPokemon[number].Name == "마휘핑" ||
            RandomPokemon[number].Name == "러브로스" || RandomPokemon[number].Name == "두드리짱" ||
            RandomPokemon[number].Name == "오거폰")
        {
            RandomPokemon[number].Gender = "Female";
        }
    }
}
