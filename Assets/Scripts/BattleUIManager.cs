using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BattleUIManager : MonoBehaviour
{
    public GameObject SelectionPanel;
    public GameObject MovePanel;

    public Image NPCImage;
    public TextMeshProUGUI TextboxText;

    public Button[] MaintenanceButtons = new Button[5];

    [Header("Fight/Bag/Pokemon")]
    public Button[] SelectionButtons = new Button[3];
    public GameObject BattlePanel;
    public GameObject BagPanel;
    public GameObject PartyPokemonPanel;

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

    [Header("Pokemon Panel")]
    private bool isPokemonSummaryOpen = true;
    private int pokemonSummaryIndex = 0;
    public Image PokemonPanelImage;
    public GameObject[] Summaries = new GameObject[4];
    public Sprite[] SummaryImages = new Sprite[4];

    [Header("Party Pokemon")]
    public TextMeshProUGUI PokemonName;
    public GameObject PartyPokemonButtons;
    public Button PartyPokemonButton;
    [SerializeField]
    private List<Button> MyPartyPokemonButtons = new List<Button>();
    public Button CancelPartyPokemonButton;
    public AudioSource PokemonCryAudioSource;
    public Image PartyPokemonSprite;
    public Image PokemonGender;
    public Sprite MaleSprite;
    public Sprite FemaleSprite;
    public GameObject AllContainObjects;

    [Header("Pokemon Status")]
    public TextMeshProUGUI PokedexNumber;
    public TextMeshProUGUI PokemonInfoName;
    public TextMeshProUGUI OT;
    public TextMeshProUGUI ID;
    public TextMeshProUGUI PokemonNature;
    public TextMeshProUGUI PokemonHoldItemName;
    public Image PokemonType1;
    public Image PokemonType2;

    [Header("Pokemon Stats")]
    public TextMeshProUGUI PokemonCurrentHP;
    public TextMeshProUGUI PokemonFullHP;
    public TextMeshProUGUI PokemonAttack;
    public TextMeshProUGUI PokemonDefense;
    public TextMeshProUGUI PokemonSpecialAttack;
    public TextMeshProUGUI PokemonSpecialDefense;
    public TextMeshProUGUI PokemonSpeed;
    public TextMeshProUGUI PokemonAbility;

    [Header("Pokemon Moves")]
    public TextMeshProUGUI[] PokemonMoveName = new TextMeshProUGUI[4];
    public TextMeshProUGUI[] PokemonMoveCurrentPP = new TextMeshProUGUI[4];
    public TextMeshProUGUI[] PokemonMoveFullPP = new TextMeshProUGUI[4];
    private string[] PokemonMoveType = new string[4];
    public Image[] PokemonMoveTypeImage = new Image[4];
    private int[] PokemonMovePower = new int[4];
    private int[] PokemonMoveAccuracy = new int[4];
    private string[] PokemonMoveCategory = new string[4];
    public Button[] SummaryMoveButtons = new Button[4];

    [Header("Pokemon Move Info")]
    public TextMeshProUGUI[] PokemonMoveInfoName = new TextMeshProUGUI[4];
    public TextMeshProUGUI[] PokemonMoveInfoCurrentPP = new TextMeshProUGUI[4];
    public TextMeshProUGUI[] PokemonMoveInfoFullPP = new TextMeshProUGUI[4];
    public TextMeshProUGUI MovePowerText;
    public TextMeshProUGUI MoveAccuracyText;
    public Sprite PhysicalCategory;
    public Sprite SpecialCategory;
    public Sprite StatusCategory;
    public Image MoveInfoCategory;
    public Image MoveInfoPokemonIcon;
    public Image MoveInfoPokemonType1;
    public Image MoveInfoPokemonType2;
    public Button[] MoveInfoButtons = new Button[4];
    public Image[] PokemonMoveInfoTypeImage = new Image[4];

    // Touch Swipe
    private Vector3 fingerDownPos;
    private Vector3 fingerUpPos;
    private bool isSwiping;
    private float swipeX;
    private float swipeThreshold = 50.0f;
    private bool canSwipe;

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
        MyPokemonImage.sprite = GameManager.instance.MyPokemons[0].Regular_Back;

        for(int i = 0; i < MaintenanceButtons.Length; i++)
        {
            int number = i;

            MaintenanceButtons[i].onClick.AddListener(() => MaintenanceButtonClicked(number));
        }

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

        CancelPartyPokemonButton.onClick.AddListener(PokemonReturnButtonClicked);

        for(int i = 0; i < SummaryMoveButtons.Length; i++)
        {
            int number = i;

            SummaryMoveButtons[i].onClick.AddListener(() => SummaryMoveButtonClicked(number));
        }

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

        if (GameManager.instance.Maintenance)
        {
            Sprite Brigette = Resources.Load<Sprite>("NPCs/Brigette");
            NPCImage.sprite= Brigette;

            TextboxText.text = "무엇을 도와드릴까요?";
        }
    }

    // 터치를 사용한 스와이핑
    void CheckSwipe()
    {
        float swipeDistanceX = Mathf.Abs(fingerDownPos.x - fingerUpPos.x);

        swipeX = (fingerDownPos.x - fingerUpPos.x);

        if(isSwiping && swipeDistanceX > swipeThreshold && canSwipe)
        {
            // 오른쪽 스와이프
            if(swipeX > 0)
            {
                switch(pokemonSummaryIndex)
                {
                    case 0:
                    case 1:
                        ++pokemonSummaryIndex;
                        PokemonPanelImage.sprite = SummaryImages[pokemonSummaryIndex];

                        break;
                }
            }
            // 왼쪽 스와이프
            else
            {
                switch (pokemonSummaryIndex)
                {
                    case 1:
                    case 2:
                        --pokemonSummaryIndex;
                        PokemonPanelImage.sprite = SummaryImages[pokemonSummaryIndex];

                        break;
                }
            }

            switch(pokemonSummaryIndex)
            {
                case 0:
                    Summaries[0].SetActive(true);
                    Summaries[1].SetActive(false);
                    Summaries[2].SetActive(false);

                    break;

                case 1:
                    Summaries[0].SetActive(false);
                    Summaries[1].SetActive(true);
                    Summaries[2].SetActive(false);

                    break;

                case 2:
                    Summaries[0].SetActive(false);
                    Summaries[1].SetActive(false);
                    Summaries[2].SetActive(true);

                    break;
            }
        }
    }

    void MaintenanceButtonClicked(int number)
    {
        switch(number)
        {
            case 0:
                {
                    PartyPokemonPanel.SetActive(true);

                    SetImageColor(PartyPokemonSprite, 0);

                    CheckMyPokemons();

                    break;
                }

            case 1:
                {
                    BagPanel.SetActive(true);

                    break;
                }

            case 2:
                {
                    break;
                }

            case 3:
                {
                    break;
                }

            case 4:
                {
                    break;
                }
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
                PartyPokemonPanel.SetActive(true);

                if(MyPartyPokemonButtons.Count == 0)
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
        if(MyPartyPokemonButtons.Count == 0)
        {
            for (int i = 0; i < GameManager.instance.MyPokemons.Count; i++)
            {
                var FPB = Instantiate(PartyPokemonButton);
                FPB.transform.SetParent(PartyPokemonButtons.transform);
                FPB.transform.localScale = Vector3.one;
                MyPartyPokemonButtons.Add(FPB);

                FPB.transform.GetChild(0).GetComponent<Image>().sprite = GameManager.instance.MyPokemons[i].Icon_Regular;
                FPB.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = GameManager.instance.MyPokemons[i].Name;
                FPB.transform.GetChild(3).GetComponent<TextMeshProUGUI>().text = GameManager.instance.MyPokemons[i].HP.ToString();
                FPB.transform.GetChild(4).GetComponent<TextMeshProUGUI>().text = "/" + GameManager.instance.MyPokemons[i].HP;

                if (GameManager.instance.MyPokemons[i].Genderless)
                {
                    SetImageColor(FPB.transform.GetChild(5).GetComponent<Image>(), 0);
                }
                else
                {
                    SetImageColor(FPB.transform.GetChild(5).GetComponent<Image>(), 1);

                    if (GameManager.instance.MyPokemons[i].Gender == "Male")
                    {
                        FPB.transform.GetChild(5).GetComponent<Image>().sprite = MaleSprite;
                    }
                    else
                    {
                        FPB.transform.GetChild(5).GetComponent<Image>().sprite = FemaleSprite;
                    }
                }

                SetImageColor(FPB.transform.GetChild(6).GetComponent<Image>(), 0);
            }

            UpdateMyPokemonButtons();
        }
    }

    // 가지고 있는 포켓몬 변경이 있을 시
    void UpdateMyPokemonButtons()
    {
        for(int i = 0; i < MyPartyPokemonButtons.Count; i++)
        {
            int number = i;

            MyPartyPokemonButtons[i].onClick.AddListener(() => MyPokemonButtonClicked(number));
        }
    }

    // 가지고 있는 포켓몬 버튼
    void MyPokemonButtonClicked(int number)
    {
        if(!isItemCategoryOpen)
        {
            canSwipe = true;

            AllContainObjects.SetActive(true);

            SetImageColor(PartyPokemonSprite, 1);

            pokemonSummaryIndex = 0;
            PokemonPanelImage.sprite = SummaryImages[0];

            Summaries[0].SetActive(true);
            Summaries[1].SetActive(false);
            Summaries[2].SetActive(false);
            Summaries[3].SetActive(false);

            PokemonCryAudioSource.PlayOneShot(GameManager.instance.MyPokemons[number].PokemonCry);

            PartyPokemonSprite.sprite = GameManager.instance.MyPokemons[number].Regular;
            PokemonName.text = GameManager.instance.MyPokemons[number].Name;

            PokedexNumber.text = GameManager.instance.MyPokemons[number].PokedexNumber.ToString();
            PokemonInfoName.text = GameManager.instance.MyPokemons[number].Name;
            OT.text = "배틀팩토리";
            ID.text = "00001";
            PokemonNature.text = GameManager.instance.MyPokemons[number].Nature;
            PokemonHoldItemName.text = "없음";

            for (int i = 0; i < PokemonMoveType.Length; i++)
            {
                PokemonMoveType[i] = null;
            }

            SetPokemonType(number);

            PokemonCurrentHP.text = GameManager.instance.MyPokemons[number].HP.ToString();
            PokemonFullHP.text = "/" + GameManager.instance.MyPokemons[number].HP.ToString();
            PokemonAttack.text = GameManager.instance.MyPokemons[number].Attack.ToString();
            PokemonDefense.text = GameManager.instance.MyPokemons[number].Defense.ToString();
            PokemonSpecialAttack.text = GameManager.instance.MyPokemons[number].SAttack.ToString();
            PokemonSpecialDefense.text = GameManager.instance.MyPokemons[number].SDefense.ToString();
            PokemonSpeed.text = GameManager.instance.MyPokemons[number].Speed.ToString();
            PokemonAbility.text = GameManager.instance.MyPokemons[number].Ability;

            for (int i = 0; i < 4; i++)
            {
                PokemonMoveName[i].text = GameManager.instance.MyPokemons[number].MoveName[i];
                PokemonMoveCurrentPP[i].text = GameManager.instance.MyPokemons[number].FullMovePP[i].ToString();
                PokemonMoveFullPP[i].text = "/" + GameManager.instance.MyPokemons[number].FullMovePP[i];
                PokemonMoveInfoName[i].text = GameManager.instance.MyPokemons[number].MoveName[i];
                PokemonMoveInfoCurrentPP[i].text = GameManager.instance.MyPokemons[number].FullMovePP[i].ToString();
                PokemonMoveInfoFullPP[i].text = "/" + GameManager.instance.MyPokemons[number].FullMovePP[i];
                PokemonMovePower[i] = GameManager.instance.MyPokemons[number].MovePower[i];
                PokemonMoveAccuracy[i] = GameManager.instance.MyPokemons[number].MoveAccuracy[i];
                PokemonMoveCategory[i] = GameManager.instance.MyPokemons[number].MoveCategory[i];

                PokemonMoveType[i] = GameManager.instance.MyPokemons[number].MoveType[i];
            }

            MoveInfoPokemonIcon.sprite = GameManager.instance.MyPokemons[number].Icon_Regular;

            SetPokemonMoveType(number);
        }
        else
        {
            Debug.Log("ABC");
        }
    }

    // 포켓몬 타입 설정
    void SetPokemonType(int number)
    {
        string Type1 = GameManager.instance.MyPokemons[number].Type1;

        if(PokemonTypes.TryGetValue(Type1, out Sprite sprite1))
        {
            PokemonType1.sprite = sprite1;
            MoveInfoPokemonType1.sprite = sprite1;
        }

        string Type2 = GameManager.instance.MyPokemons[number].Type2;

        if (GameManager.instance.MyPokemons[number].Type2 == "")
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
            string MoveType = GameManager.instance.MyPokemons[number].MoveType[i];

            if(PokemonTypes.TryGetValue(MoveType, out Sprite sprite))
            {
                PokemonMoveTypeImage[i].sprite = sprite;
                PokemonMoveInfoTypeImage[i].sprite = sprite;
            }
        }
    }

    // 포켓몬 창에서 배틀 창으로 돌아갈 때
    void PokemonReturnButtonClicked()
    {
        PartyPokemonPanel.SetActive(false);
        BattlePanel.SetActive(true);

        canSwipe = false;

        ResetPokemonSummary();
    }

    // 포켓몬 설명에 있는 모든 설정값 초기화
    void ResetPokemonSummary()
    {
        AllContainObjects.SetActive(false);

        PokemonPanelImage.sprite = SummaryImages[0];

        for(int i = 0; i < Summaries.Length; i++)
        {
            Summaries[i].SetActive(false);
        }

        PokemonName.text = null;

        PokedexNumber.text = null;
        PokemonInfoName.text = null;
        OT.text = null;
        ID.text = null;
        PokemonNature.text = null;
        PokemonHoldItemName.text = null;

        PokemonType1.sprite = null;
        PokemonType2.sprite = null;

        PokemonCurrentHP.text = null;
        PokemonFullHP.text = null;
        PokemonAttack.text = null;
        PokemonDefense.text = null;
        PokemonSpecialAttack.text = null;
        PokemonSpecialDefense.text = null;
        PokemonSpeed.text = null;
        PokemonAbility.text = null;

        for (int i = 0; i < 4; i++)
        {
            PokemonMoveName[i].text = null;
            PokemonMoveCurrentPP[i].text = null;
            PokemonMoveFullPP[i].text = null;
            PokemonMoveInfoName[i].text = null;
            PokemonMoveInfoCurrentPP[i].text = null;
            PokemonMoveInfoFullPP[i].text = null;
        }
    }

    // 포켓몬 기술 버튼
    void SummaryMoveButtonClicked(int number)
    {
        canSwipe = false;

        AllContainObjects.SetActive(false);

        PokemonPanelImage.sprite = SummaryImages[3];

        Summaries[0].SetActive(false);
        Summaries[1].SetActive(false);
        Summaries[2].SetActive(false);
        Summaries[3].SetActive(true);

        SetMoveInfo(number);
    }

    // 포켓몬 기술 정보 버튼
    void MoveInfoButtonClicked(int number)
    {
        SetMoveInfo(number);
    }

    void SetMoveInfo(int number)
    {
        for (int i = 0; i < SummaryMoveButtons.Length; i++)
        {
            if(PokemonMovePower[number] == 0)
            {
                MovePowerText.text = "---";
            }
            else
            {
                MovePowerText.text = PokemonMovePower[number].ToString();
            }

            MoveAccuracyText.text = PokemonMoveAccuracy[number].ToString();

            if (PokemonMoveCategory[number] == "물리")
            {
                MoveInfoCategory.sprite = PhysicalCategory;
            }
            else if (PokemonMoveCategory[number] == "특수")
            {
                MoveInfoCategory.sprite = SpecialCategory;
            }
            else
            {
                MoveInfoCategory.sprite = StatusCategory;
            }
        }
    }

    // 이미지 알파값 설정
    void SetImageColor(Image image, float alpha)
    {
        Color color = image.color;
        color.a = alpha;
        image.color = color;
    }
}
