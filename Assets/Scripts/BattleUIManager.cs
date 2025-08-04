using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BattleUIManager : MonoBehaviour
{
    public GameObject SelectionPanel;
    public GameObject MovePanel;

    public Image NPCImage;

    [Header("Fight/Bag/Pokemon")]
    public Button[] SelectionButtons = new Button[3];
    public GameObject BattlePanel;
    public GameObject BagPanel;
    public GameObject PokemonPanel;

    public Image MyPokemonImage;

    public Button[] MoveButtons = new Button[4];
    public TextMeshProUGUI[] MoveTexts = new TextMeshProUGUI[4];
    public TextMeshProUGUI[] PPTexts = new TextMeshProUGUI[4];    

    public Button CancelButton;

    [Header("Bag")]
    public GameObject ItemCategoryPanel;
    public GameObject ItemListPanel;
    public GameObject ItemDescriptionPanel;
    public Button BagReturnButton;
    private bool isItemCategoryOpen;
    private bool isItemListOpen;
    public Button[] ItemCategoryButtons = new Button[3];
    public Image ScrollViewBG;
    public Sprite[] ScrollViewSprites = new Sprite[3];

    [Header("My Pokemons")]
    public GameObject PokemonButtons;
    public Button FirstPokemonButton;
    public Button EmptyPokemonButton;
    public Button PokemonButton;
    private Sprite PokemonIcon;
    [SerializeField]
    private List<Button> MyPokemonButtons = new List<Button>();
    [SerializeField]
    private List<Pokemon> MyPokemonInfos = new List<Pokemon>();
    public Button PokemonReturnButton;
    public AudioSource PokemonCryAudioSource;
    public GameObject PokemonSprite;

    [Header("Pokemon Summary")]
    private bool isPokemonSummaryOpen = true;
    public GameObject[] PokemonSummaries = new GameObject[3];
    public GameObject PokemonMoveInfo;
    private int pokemonSummaryIndex = 0;    
    public Image PokemonSummaryImage;
    public TextMeshProUGUI PokemonName;

    [Header("Pokemon Info")]
    public TextMeshProUGUI PokemonNumber;
    public TextMeshProUGUI PokemonInfoName;
    public TextMeshProUGUI OwnerName;
    public TextMeshProUGUI ID;
    public TextMeshProUGUI PokemonNature;
    public TextMeshProUGUI PokemonHoldItemName;
    public Image PokemonType1;
    public Image PokemonType2;

    [Header("Pokemon Skills")]
    public TextMeshProUGUI PokemonHP;
    public TextMeshProUGUI PokemonAttack;
    public TextMeshProUGUI PokemonDefense;
    public TextMeshProUGUI PokemonSpecialAttack;
    public TextMeshProUGUI PokemonSpecialDefense;
    public TextMeshProUGUI PokemonSpeed;
    public TextMeshProUGUI PokemonAbility;

    [Header("Pokemon Moves")]
    public TextMeshProUGUI[] PokemonMove = new TextMeshProUGUI[4];
    public TextMeshProUGUI[] PokemonMovePP = new TextMeshProUGUI[4];
    private string[] PokemonMoveType = new string[4];
    public Image[] PokemonMoveTypeImage = new Image[4];
    private int[] PokemonMovePower = new int[4];
    private int[] PokemonMoveAccuracy = new int[4];
    private string[] PokemonMoveCategory = new string[4];

    public Button[] MoveInfoButtons = new Button[4];

    [Header("Pokemon Move Info")]
    public TextMeshProUGUI MovePowerText;
    public TextMeshProUGUI MoveAccuracyText;
    public Sprite PhysicalCategory;
    public Sprite SpecialCategory;
    public Sprite StatusCategory;
    public Image MoveInfoCategory;
    public Image MoveInfoPokemonIcon;
    public Image MoveInfoPokemonType1;
    public Image MoveInfoPokemonType2;

    // Touch Swipe
    private Vector3 fingerDownPos;
    private Vector3 fingerUpPos;
    private bool isSwiping;
    private float swipeX;
    private float swipeThreshold = 50.0f;

    [Header("Type Buttons")]
    public Sprite[] TypeButtonSprites = new Sprite[18];
    private Dictionary<string, Sprite> TypeButtons;

    [Header("Type Images")]
    public Sprite[] PokemonTypeSprites = new Sprite[18];
    private Dictionary<string, Sprite> PokemonTypes;

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

        TypeButtons = new Dictionary<string, Sprite>
        {
            { "벌레", TypeButtonSprites[0] },
            { "악", TypeButtonSprites[1] },
            { "드래곤", TypeButtonSprites[2] },
            { "전기", TypeButtonSprites[3] },
            { "페어리", TypeButtonSprites[4] },
            { "격투", TypeButtonSprites[5] },
            { "불꽃", TypeButtonSprites[6] },
            { "비행", TypeButtonSprites[7] },
            { "고스트", TypeButtonSprites[8] },
            { "풀", TypeButtonSprites[9] },
            { "땅", TypeButtonSprites[10] },
            { "얼음", TypeButtonSprites[11] },
            { "노말", TypeButtonSprites[12] },
            { "독", TypeButtonSprites[13] },
            { "에스퍼", TypeButtonSprites[14] },
            { "바위", TypeButtonSprites[15] },
            { "강철", TypeButtonSprites[16] },
            { "물", TypeButtonSprites[17] }
        };
    }

    void Start()
    {
        for(int i = 0; i < SelectionButtons.Length; i++)
        {
            int number = i;

            SelectionButtons[i].onClick.AddListener(() => SelectionButtonClicked(number));
        }

        for(int i = 0; i < MoveButtons.Length; i++)
        {
            int number = i;

            MoveButtons[i].onClick.AddListener(() => MoveButtonClicked(number));
        }

        CancelButton.onClick.AddListener(CancelButtonClicked);

        BagReturnButton.onClick.AddListener(BagReturnButtonClicked);

        for(int i = 0; i < ItemCategoryButtons.Length; i++)
        {
            int number = i;

            ItemCategoryButtons[i].onClick.AddListener(() => ItemCategoryButtonClicked(number));
        }

        PokemonReturnButton.onClick.AddListener(PokemonReturnButtonClicked);

        for(int i = 0; i < MoveInfoButtons.Length; i++)
        {
            int number = i;

            MoveInfoButtons[i].onClick.AddListener(() => MoveInfoButtonClicked(number));
        }
    }

    void Update()
    {
        if (isPokemonSummaryOpen && Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            switch (touch.phase)
            {
                case TouchPhase.Began:
                    {
                        fingerDownPos = touch.position;
                        isSwiping = true;

                        break;
                    }
                case TouchPhase.Moved:
                    {
                        break;
                    }
                case TouchPhase.Ended:
                    {
                        fingerUpPos = touch.position;
                        CheckSwipe();
                        isSwiping = false;

                        break;
                    }
            }
        }

        if(GameManager.instance.Maintenance)
        {
            Sprite Brigette = Resources.Load<Sprite>("NPCs/Brigette");
            NPCImage.sprite= Brigette;
        }
    }

    // 터치를 사용한 스와이핑
    void CheckSwipe()
    {
        float swipeDistanceX = Mathf.Abs(fingerDownPos.x - fingerUpPos.x);

        swipeX = (fingerDownPos.x - fingerUpPos.x);

        if(isSwiping && swipeDistanceX > swipeThreshold)
        {
            // 오른쪽으로 이동
            if(swipeX > 0)
            {
                Debug.Log("Right Swipe");

                switch(pokemonSummaryIndex)
                {
                    case 0:
                        {
                            PokemonSummaries[pokemonSummaryIndex].SetActive(false);
                            ++pokemonSummaryIndex;
                            PokemonSummaries[pokemonSummaryIndex].SetActive(true);
                            break;
                        }
                    case 1:
                        {
                            PokemonSummaries[pokemonSummaryIndex].SetActive(false);
                            ++pokemonSummaryIndex;
                            PokemonSummaries[pokemonSummaryIndex].SetActive(true);

                            break;
                        }                    
                }
            }
            // 왼쪽으로 이동
            else
            {
                Debug.Log("Left Swipe");

                switch (pokemonSummaryIndex)
                {
                    case 1:
                        {
                            PokemonSummaries[pokemonSummaryIndex].SetActive(false);
                            --pokemonSummaryIndex;
                            PokemonSummaries[pokemonSummaryIndex].SetActive(true);
                            break;
                        }
                    case 2:
                        {
                            PokemonSummaries[pokemonSummaryIndex].SetActive(false);
                            --pokemonSummaryIndex;
                            PokemonSummaries[pokemonSummaryIndex].SetActive(true);

                            if(PokemonMoveInfo.activeSelf)
                            {
                                PokemonMoveInfo.SetActive(false);
                            }

                            break;
                        }
                }
            }
        }
    }

    void MainenanceBeforeBattle()
    {
        if(GameManager.instance.Maintenance)
        {

        }
    }

    // 배틀 시, Fight/Bag/Pokemon 버튼
    void SelectionButtonClicked(int number)
    {
        switch(number)
        {
            case 0:
                SelectionPanel.SetActive(false);
                MovePanel.SetActive(true);

                ChangeMoveButtonTypes();

                break;

            case 1:
                //GameManager.instance.currentPokemon = GameManager.instance.MyPokemons[0]; // 기술 이름 변경확인용
                BagPanel.SetActive(true);
                isItemCategoryOpen = true;

                break;

            case 2:
                //GameManager.instance.currentPokemon = GameManager.instance.MyPokemons[1]; // 기술 이름 변경확인용
                PokemonPanel.SetActive(true);

                if(MyPokemonButtons.Count == 0)
                {
                    CheckMyPokemons();
                }

                break;
        }
    }
    
    // 포켓몬 기술 버튼 사용
    void MoveButtonClicked(int number)
    {
        GameManager.instance.CurrentPokemon.CurrentMovePP[number]--;

        if(GameManager.instance.CurrentPokemon.CurrentMovePP[number] < 0)
        {
            GameManager.instance.CurrentPokemon.CurrentMovePP[number] = 0;
        }

        MoveButtons[number].transform.GetChild(1).GetComponent<TextMeshProUGUI>().text =
                GameManager.instance.CurrentPokemon.CurrentMovePP[number].ToString() + "  " + GameManager.instance.CurrentPokemon.FullMovePP[number].ToString();

        //SelectionPanel.SetActive(false);
        //MovePanel.SetActive(false);
    }

    // 포켓몬 기술창 취소버튼
    void CancelButtonClicked()
    {
        SelectionPanel.SetActive(true);
        MovePanel.SetActive(false);
    }

    // 포켓몬 기술 타입 버튼 변경
    void ChangeMoveButtonTypes()
    {
        for(int i = 0; i < MoveButtons.Length; i++)
        {
            string MoveType = GameManager.instance.CurrentPokemon.MoveType[i];

            if(TypeButtons.TryGetValue(MoveType, out Sprite sprite))
            {
                MoveButtons[i].GetComponent<Image>().sprite = sprite;
            }

            MoveButtons[i].transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = GameManager.instance.CurrentPokemon.MoveName[i];
            MoveButtons[i].transform.GetChild(1).GetComponent<TextMeshProUGUI>().text =
                GameManager.instance.CurrentPokemon.CurrentMovePP[i].ToString() + "  " + GameManager.instance.CurrentPokemon.FullMovePP[i].ToString();
        }
    }

    // 가방에서 배틀로 돌아가기
    void BagReturnButtonClicked()
    {
        if(isItemCategoryOpen)
        {
            BagPanel.SetActive(false);
            BattlePanel.SetActive(true);
        }
        else if(isItemListOpen)
        {
            isItemCategoryOpen = true;
            isItemListOpen = false;

            ItemCategoryPanel.SetActive(true);
            ItemListPanel.SetActive(false);
        }
    }

    // 아이템 카테고리 버튼 클릭 (회복, 상태, 배틀)
    void ItemCategoryButtonClicked(int number)
    {
        isItemCategoryOpen = false;
        isItemListOpen = true;

        ItemCategoryPanel.SetActive(false);
        ItemListPanel.SetActive(true);

        switch (number)
        {
            case 0:
                {
                    ScrollViewBG.sprite = ScrollViewSprites[number];

                    break;
                }
            case 1:
                {
                    ScrollViewBG.sprite = ScrollViewSprites[number];

                    break;
                }
            case 2:
                {
                    ScrollViewBG.sprite = ScrollViewSprites[number];

                    break;
                }
        }
    }

    // 가지고 있는 포켓몬 확인하여 버튼 생성
    void CheckMyPokemons()
    {
        for(int i = 0; i < GameManager.instance.MyPokemons.Count; i++)
        {
            if(i == 0)
            {
                var FPB = Instantiate(FirstPokemonButton);
                FPB.transform.SetParent(PokemonButtons.transform);
                FPB.transform.localScale = Vector3.one;
                MyPokemonButtons.Add(FPB);
                MyPokemonInfos.Add(GameManager.instance.MyPokemons[i]);

                FPB.transform.GetChild(0).GetComponent<Image>().sprite = GameManager.instance.MyPokemons[i].Icon_Regular;
                FPB.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = GameManager.instance.MyPokemons[i].Name;
                FPB.transform.GetChild(3).GetComponent<TextMeshProUGUI>().text = GameManager.instance.MyPokemons[i].HP + "/" + GameManager.instance.MyPokemons[i].HP;
            }
            else
            {
                if (GameManager.instance.MyPokemons[i] != null)
                {
                    var PB = Instantiate(PokemonButton);
                    PB.transform.SetParent(PokemonButtons.transform);
                    PB.transform.localScale = Vector3.one;
                    MyPokemonButtons.Add(PB);
                    MyPokemonInfos.Add(GameManager.instance.MyPokemons[i]);

                    PB.transform.GetChild(0).GetComponent<Image>().sprite = GameManager.instance.MyPokemons[i].Icon_Regular;
                    PB.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = GameManager.instance.MyPokemons[i].Name;
                    PB.transform.GetChild(3).GetComponent<TextMeshProUGUI>().text = GameManager.instance.MyPokemons[i].HP + "/" + GameManager.instance.MyPokemons[i].HP;
                }
                else
                {
                    var EPB = Instantiate(EmptyPokemonButton);
                    EPB.transform.SetParent(PokemonButtons.transform);
                    EPB.transform.localScale = Vector3.one;
                    MyPokemonButtons.Add(EPB);
                }
            }
        }

        UpdateMyPokemonButtons();
    }

    // 가지고 있는 포켓몬 변경이 있을 시
    void UpdateMyPokemonButtons()
    {
        for(int i = 0; i < MyPokemonButtons.Count; i++)
        {
            int number = i;

            MyPokemonButtons[i].onClick.AddListener(() => MyPokemonButtonClicked(number));
        }
    }

    // 가지고 있는 포켓몬 버튼
    void MyPokemonButtonClicked(int number)
    {
        PokemonSprite.SetActive(true);

        ResetPokemonSummaryIndex();

        for(int i = 0; i < PokemonMoveType.Length; i++)
        {
            PokemonMoveType[i] = null;
        }

        PokemonCryAudioSource.PlayOneShot(MyPokemonInfos[number].PokemonCry);
        
        PokemonSummaryImage.sprite = MyPokemonInfos[number].Regular;
        PokemonName.text = MyPokemonInfos[number].Name;
        PokemonIcon = MyPokemonInfos[number].Icon_Regular;

        PokemonNumber.text = MyPokemonInfos[number].PokedexNumber.ToString();
        PokemonInfoName.text = MyPokemonInfos[number].Name;
        OwnerName.text = "배틀팩토리";
        ID.text = "00001";
        PokemonNature.text = MyPokemonInfos[number].Nature;
        PokemonHoldItemName.text = "없음";

        SetPokemonType(number);

        PokemonHP.text = MyPokemonInfos[number].HP.ToString() + "/" + MyPokemonInfos[number].HP.ToString();
        PokemonAttack.text = MyPokemonInfos[number].Attack.ToString();
        PokemonDefense.text = MyPokemonInfos[number].Defense.ToString();
        PokemonSpecialAttack.text = MyPokemonInfos[number].SAttack.ToString();
        PokemonSpecialDefense.text = MyPokemonInfos[number].SDefense.ToString();
        PokemonSpeed.text = MyPokemonInfos[number].Speed.ToString();
        PokemonAbility.text = MyPokemonInfos[number].Ability;

        for(int i = 0; i < 4; i++)
        {
            PokemonMove[i].text = MyPokemonInfos[number].MoveName[i];
            PokemonMovePP[i].text = MyPokemonInfos[number].FullMovePP[i].ToString() + "/" + MyPokemonInfos[number].FullMovePP[i].ToString();
            PokemonMovePower[i] = MyPokemonInfos[number].MovePower[i];
            PokemonMoveAccuracy[i] = MyPokemonInfos[number].MoveAccuracy[i];
            PokemonMoveCategory[i] = MyPokemonInfos[number].MoveCategory[i];

            PokemonMoveType[i] = MyPokemonInfos[number].MoveType[i];
        }

        if (PokemonMoveInfo.activeSelf)
        {
            PokemonMoveInfo.SetActive(false);
        }

        MoveInfoPokemonIcon.sprite = PokemonIcon;

        SetPokemonMoveType(number);
    }

    // 포켓몬 타입 설정
    void SetPokemonType(int number)
    {
        string Type1 = MyPokemonInfos[number].Type1;

        if(PokemonTypes.TryGetValue(Type1, out Sprite sprite1))
        {
            PokemonType1.sprite = sprite1;
            MoveInfoPokemonType1.sprite = sprite1;
        }

        string Type2 = MyPokemonInfos[number].Type2;

        if (MyPokemonInfos[number].Type2 == "")
        {
            PokemonType2.sprite = null;
            MoveInfoPokemonType2.sprite = null;
            PokemonType2.gameObject.SetActive(false);
            MoveInfoPokemonType2.gameObject.SetActive(false);
        }
        else
        {
            PokemonType2.gameObject.SetActive(true);
            MoveInfoPokemonType2.gameObject.SetActive(true);

            if (PokemonTypes.TryGetValue(Type2, out Sprite sprite2))
            {
                PokemonType2.sprite = sprite2;
                MoveInfoPokemonType2.sprite = sprite2;
            }
        }
        
    }

    // 포켓몬 기술 타입 설정
    void SetPokemonMoveType(int number)
    {
        for(int i = 0; i < PokemonMoveType.Length; i++)
        {
            string MoveType = MyPokemonInfos[number].MoveType[i];

            if(PokemonTypes.TryGetValue(MoveType, out Sprite sprite))
            {
                PokemonMoveTypeImage[i].sprite = sprite;
            }
        }
    }

    // 포켓몬 창에서 배틀 창으로 돌아갈 때
    void PokemonReturnButtonClicked()
    {
        PokemonPanel.SetActive(false);
        BattlePanel.SetActive(true);

        ResetPokemonSummary();

        ResetPokemonSummaryIndex();

        PokemonMoveInfo.SetActive(false);
    }

    // 포켓몬 설명 초기화
    void ResetPokemonSummaryIndex()
    {
        pokemonSummaryIndex = 0;

        for (int i = 0; i < PokemonSummaries.Length; i++)
        {
            if (i == 0)
            {
                PokemonSummaries[i].SetActive(true);
            }
            else
            {
                PokemonSummaries[i].SetActive(false);
            }
        }
    }

    // 포켓몬 설명에 있는 모든 설정값 초기화
    void ResetPokemonSummary()
    {
        PokemonSprite.SetActive(false);

        PokemonSummaryImage.sprite = null;
        PokemonName.text = null;

        PokemonNumber.text = null;
        PokemonInfoName.text = null;
        OwnerName.text = null;
        ID.text = null;
        PokemonNature.text = null;
        PokemonHoldItemName.text = null;

        PokemonType1.sprite = null;
        PokemonType2.sprite = null;

        PokemonHP.text = null;
        PokemonAttack.text = null;
        PokemonDefense.text = null;
        PokemonSpecialAttack.text = null;
        PokemonSpecialDefense.text = null;
        PokemonSpeed.text = null;
        PokemonAbility.text = null;

        for (int i = 0; i < 4; i++)
        {
            PokemonMove[i].text = null;
            PokemonMovePP[i].text = null;
        }
    }

    // 포켓몬 기술 버튼 (기술 설명)
    void MoveInfoButtonClicked(int number)
    {
        PokemonMoveInfo.SetActive(true);

        for (int i = 0; i < MoveInfoButtons.Length; i++)
        {
            MovePowerText.text = PokemonMovePower[number].ToString();
            MoveAccuracyText.text = PokemonMoveAccuracy[number].ToString();

            if(PokemonMoveCategory[number] == "물리")
            {
                MoveInfoCategory.sprite = PhysicalCategory;
            }
            else if(PokemonMoveCategory[number] == "특수")
            {
                MoveInfoCategory.sprite = SpecialCategory;
            }
            else
            {
                MoveInfoCategory.sprite = StatusCategory;
            }
        }
    }
}
