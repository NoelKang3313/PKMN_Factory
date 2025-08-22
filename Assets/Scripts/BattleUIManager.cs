using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BattleUIManager : MonoBehaviour
{
    private AudioSource UIAudioSource;
    public Animator FadeAnimator;

    public GameObject SelectionPanel;   // 싸우다, 가방, 포켓몬 버튼 묶음
    public GameObject MovePanel;    // 기술 버튼 (기술 버튼 4개, 메가진화 버튼, 취소 버튼) 묶음

    public Image PlayerImage;   // Player 이미지
    public Image NPCImage;  // NPC 이미지
    public TextMeshProUGUI TextboxText; //  텍스트박스 텍스트

    public GameObject MaintenancePanel;
    public GameObject BattlePanel;
    public GameObject BattleSelections;

    public Button[] MaintenanceButtons = new Button[5]; // 포켓몬, 가방, 상점, 포켓몬 교체, 배틀 시작 버튼

    [Header("Fight/Bag/Pokemon")]
    public Button[] SelectionButtons = new Button[3];   // 싸우다, 가방, 포켓몬 버튼
    public GameObject BagPanel; // 가방 버튼 선택
    public GameObject ShopPanel;    // 샵 버튼 선택
    public GameObject PartyPokemonPanel;    // 포켓몬 버튼 선택

    public Image MyPokemonImage;    // 내 포켓몬 배틀 이미지

    public Button[] MoveButtons = new Button[4];
    public TextMeshProUGUI[] MoveTexts = new TextMeshProUGUI[4];
    public TextMeshProUGUI[] PPTexts = new TextMeshProUGUI[4];

    public Button CancelButton;

    [Header("Bag")]
    public Image[] BagUIImages = new Image[5];
    public Sprite[] MaleBagUISprites = new Sprite[5];
    public Sprite[] FemaleBagUISprites = new Sprite[5];
    public Button[] BagCategoryButtons = new Button[4];
    public ScrollRect BagScrollRect;
    public GameObject[] BagItemContents = new GameObject[4];
    public Animator[] BagCategoryButtonAnimator = new Animator[4];
    public Button BagReturnButton;

    [Header("Shop")]
    public Button[] ItemCategoryButtons = new Button[4];
    public GameObject[] ShopItemCategoryContents = new GameObject[4];
    public ScrollRect ShopScrollRect;
    public Sprite[] ItemCategoryButtonSelectedSprite = new Sprite[4];
    public Sprite[] ItemCategoryButtonDeselectedSprite = new Sprite[4];
    public GameObject PurchaseCancelButton;
    public Image ShopItemImage;
    public TextMeshProUGUI ShopItemDescription;
    public TextMeshProUGUI ShopItemPriceText;
    public TextMeshProUGUI ShopItemPrice;
    public Button ItemPurchaseButton;
    public Button ItemCancelButton;
    public Button ShopReturnButton;
    [SerializeField]
    private Item shopSelectedItem;

    [Header("Berries")]
    public Button[] BerryButtons = new Button[10];
    public Item[] BerryInfos = new Item[10];

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
    private int pokemonNumber;

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
    public TextMeshProUGUI PokemonAbilityDescription;

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
    private bool isMoveInfoOpen;
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
    public TextMeshProUGUI MoveDescription;

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
        UIAudioSource = GetComponent<AudioSource>();

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
        SetBattleMaintenance();

        CheckMyPokemons();

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

        for(int i = 0; i < BagCategoryButtons.Length; i++)
        {
            int number = i;

            BagCategoryButtons[i].onClick.AddListener(() => BagCategoryButtonClicked(number));
        }

        BagReturnButton.onClick.AddListener(BagReturnButtonClicked);

        for(int i = 0; i < MoveButtons.Length; i++)
        {
            int number = i;

            MoveButtons[i].onClick.AddListener(() => MoveButtonClicked(number));
        }

        CancelButton.onClick.AddListener(CancelButtonClicked);

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

        for(int i = 0; i < ItemCategoryButtons.Length; i++)
        {
            int number = i;

            ItemCategoryButtons[i].onClick.AddListener(() => ItemCategoryButtonClicked(number));
        }

        ItemPurchaseButton.onClick.AddListener(ItemPurchaseButtonClicked);
        ItemCancelButton.onClick.AddListener(ItemCancelButtonClicked);
        ShopReturnButton.onClick.AddListener(ShopReturnButtonClicked);

        for(int i = 0; i < BerryButtons.Length; i++)
        {
            int number = i;

            BerryButtons[i].onClick.AddListener(() => BerryButtonClicked(number));
        }
    }

    void Update()
    {
        PokemonBattleAnimation();

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
    }

    void SetBattleMaintenance()
    {
        Sprite Brigette = Resources.Load<Sprite>("NPCs/Brigette");
        NPCImage.sprite = Brigette;

        TextboxText.text = "무엇을 도와드릴까요?";
    }

    // 터치를 사용한 스와이핑
    void CheckSwipe()
    {
        float swipeDistanceX = Mathf.Abs(fingerDownPos.x - fingerUpPos.x);

        swipeX = (fingerDownPos.x - fingerUpPos.x);

        if(isSwiping && swipeDistanceX > swipeThreshold && canSwipe)
        {
            UIAudioSource.Play();

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
                        if (isMoveInfoOpen)
                        {
                            isMoveInfoOpen = false;

                            AllContainObjects.SetActive(true);

                            --pokemonSummaryIndex;
                            PokemonPanelImage.sprite = SummaryImages[pokemonSummaryIndex];

                            Summaries[2].SetActive(true);
                            Summaries[3].SetActive(false);
                        }
                        else
                        {
                            --pokemonSummaryIndex;
                            PokemonPanelImage.sprite = SummaryImages[pokemonSummaryIndex];
                        }

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

    void PokemonBattleAnimation()
    {
        if (FadeAnimator.GetCurrentAnimatorStateInfo(0).IsName("Fade In") &&
            FadeAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f)
        {
            FadeAnimator.gameObject.SetActive(false);

            PlayerImage.enabled = true;
            NPCImage.enabled = true;
        }

        if (!NPCImage.GetComponent<Animator>().GetBool("isAppear") &&
            NPCImage.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f)
        {
            GameManager.instance.BattleStart = true;
        }
    }

    void MaintenanceButtonClicked(int number)
    {
        UIAudioSource.Play();

        switch(number)
        {
            case 0:
                {
                    PartyPokemonPanel.SetActive(true);

                    SetImageColor(PartyPokemonSprite, 0);

                    TextboxText.text = "무엇을 도와드릴까요?";

                    break;
                }

            case 1:
                {
                    BagPanel.SetActive(true);

                    SetBagImage();

                    ResetBagUI();

                    break;
                }

            case 2:
                {
                    ShopPanel.SetActive(true);

                    ResetShopUI();

                    break;
                }

            case 3:
                {
                    if (GameManager.instance.isFirstBattle)
                    {
                        TextboxText.text = "방금 포켓몬을 선택하셨기에 현재는 포켓몬 교체가 불가합니다.";
                    }
                    else
                    {
                        FadeAnimator.gameObject.SetActive(true);
                        FadeAnimator.Play("Fade Out");
                    }

                    break;
                }

            case 4:
                {
                    NPCImage.GetComponent<Animator>().SetBool("isAppear", false);

                    MaintenancePanel.SetActive(false);
                    BattlePanel.SetActive(true);

                    TextboxText.text = "";

                    break;
                }
        }
    }

    void SetBagImage()
    {
        for(int i = 0; i < BagUIImages.Length; i++)
        {
            BagUIImages[i].sprite = MaleBagUISprites[i];
        }
    }

    void SetBagUI(int index)
    {
        for(int i = 0; i < 4; i++)
        {
            if(index == i)
            {
                BagItemContents[i].SetActive(true);
                BagCategoryButtonAnimator[i].Play("Male");
            }
            else
            {
                BagItemContents[i].SetActive(false);
                BagCategoryButtonAnimator[i].Play("Male Idle");
            }
        }
    }

    void ResetBagUI()
    {
        for(int i = 0; i < 4; i++)
        {
            BagItemContents[i].SetActive(false);
            BagCategoryButtonAnimator[i].Play("Male Idle");
        }
    }

    void BagCategoryButtonClicked(int number)
    {
        UIAudioSource.Play();

        SetBagUI(number);

        BagScrollRect.content = BagItemContents[number].GetComponent<RectTransform>();
    }

    void BagReturnButtonClicked()
    {
        UIAudioSource.Play();

        BagPanel.SetActive(false);
    }

    void SetShopUI(int index)
    {
        for(int i = 0; i < 4; i++)
        {
            if(index == i)
            {
                ShopItemCategoryContents[i].SetActive(true);
                ItemCategoryButtons[i].GetComponent<Image>().sprite = ItemCategoryButtonSelectedSprite[i];
            }
            else
            {
                ShopItemCategoryContents[i].SetActive(false);
                ItemCategoryButtons[i].GetComponent<Image>().sprite = ItemCategoryButtonDeselectedSprite[i];
            }
        }
    }

    void ResetShopUI()
    {
        for(int i = 0; i < 4; i++)
        {
            ShopItemCategoryContents[i].SetActive(false);
            PurchaseCancelButton.SetActive(false);
            ItemCategoryButtons[i].GetComponent<Image>().sprite = ItemCategoryButtonDeselectedSprite[i];
        }
    }

    void ItemCategoryButtonClicked(int number)
    {
        UIAudioSource.Play();

        SetShopUI(number);
        ShopScrollRect.content = ShopItemCategoryContents[number].GetComponent<RectTransform>();
    }

    void ItemPurchaseButtonClicked()
    {
        UIAudioSource.Play();

        Debug.Log("Item Purchase!");

        SetImageColor(ShopItemImage, 0f);
        PurchaseCancelButton.SetActive(false);
        ShopItemDescription.text = "";
        ShopItemPriceText.gameObject.SetActive(false);
        ShopItemPrice.text = "";
    }

    void ItemCancelButtonClicked()
    {
        UIAudioSource.Play();

        PurchaseCancelButton.SetActive(false);

        SetImageColor(ShopItemImage, 0f);
        PurchaseCancelButton.SetActive(false);
        ShopItemDescription.text = "";
        ShopItemPriceText.gameObject.SetActive(false);
        ShopItemPrice.text = "";
    }

    void ShopReturnButtonClicked()
    {
        UIAudioSource.Play();

        ShopPanel.SetActive(false);

        SetImageColor(ShopItemImage, 0f);
        ShopItemDescription.text = "";
        ShopItemPriceText.gameObject.SetActive(false);
        ShopItemPrice.text = "";
    }

    void BerryButtonClicked(int number)
    {
        UIAudioSource.Play();

        SetImageColor(ShopItemImage, 1f);

        ShopItemImage.sprite = BerryInfos[number].ItemSprite;
        ShopItemDescription.text = BerryInfos[number].ItemDescription;
        ShopItemPriceText.gameObject.SetActive(true);
        ShopItemPrice.text = BerryInfos[number].ItemPrice.ToString() + "원";

        PurchaseCancelButton.SetActive(true);

        shopSelectedItem = BerryInfos[number];
    }

    // 배틀 시, Fight/Bag/Pokemon 버튼
    void SelectionButtonClicked(int number)
    {
        UIAudioSource.Play();

        switch (number)
        {
            case 0:
                SelectionPanel.SetActive(false);
                MovePanel.SetActive(true);

                ChangeMoveButtonTypes();

                break;

            case 1:
                //GameManager.instance.currentPokemon = GameManager.instance.MyPokemons[0]; // 기술 이름 변경확인용
                BagPanel.SetActive(true);

                break;

            case 2:
                //GameManager.instance.currentPokemon = GameManager.instance.MyPokemons[1]; // 기술 이름 변경확인용
                PartyPokemonPanel.SetActive(true);

                break;
        }
    }
    
    // 포켓몬 기술 버튼 사용
    void MoveButtonClicked(int number)
    {
        UIAudioSource.Play();

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
        UIAudioSource.Play();

        SelectionPanel.SetActive(true);
        MovePanel.SetActive(false);
    }

    // 포켓몬 기술 타입 버튼 변경
    void ChangeMoveButtonTypes()
    {
        for (int i = 0; i < MoveButtons.Length; i++)
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
        UIAudioSource.Play();

        canSwipe = true;

        pokemonNumber = number;

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

        if (GameManager.instance.MyPokemons[number].Genderless)
        {
            SetImageColor(PokemonGender, 0f);
        }
        else
        {
            SetImageColor(PokemonGender, 1f);

            if (GameManager.instance.MyPokemons[number].Gender == "Male")
            {
                PokemonGender.sprite = MaleSprite;
            }
            else
            {
                PokemonGender.sprite = FemaleSprite;
            }
        }

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
        PokemonAbilityDescription.text = GameManager.instance.SetPokemonAbility(number, GameManager.instance.MyPokemons);

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
        UIAudioSource.Play();

        PartyPokemonPanel.SetActive(false);

        ResetPokemonSummary();

        canSwipe = false;
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
        UIAudioSource.Play();

        isMoveInfoOpen = true;

        AllContainObjects.SetActive(false);

        PokemonPanelImage.sprite = SummaryImages[3];

        Summaries[0].SetActive(false);
        Summaries[1].SetActive(false);
        Summaries[2].SetActive(false);
        Summaries[3].SetActive(true);

        SetMoveInfo(number);
    }

    void SearchMove(int number)
    {
        for (int i = 0; i < GameManager.instance.PokemonMove.Count; i++)
        {
            if (GameManager.instance.MyPokemons[pokemonNumber].MoveName[number] == GameManager.instance.PokemonMove[i]["MoveName"].ToString())
            {
                MoveDescription.text = GameManager.instance.PokemonMove[i]["Description"].ToString();

                break;
            }
        }
    }

    // 포켓몬 기술 정보 버튼
    void MoveInfoButtonClicked(int number)
    {
        UIAudioSource.Play();

        SetMoveInfo(number);
    }

    void SetMoveInfo(int number)
    {
        if (PokemonMovePower[number] == 0)
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

        SearchMove(number);
    }

    // 이미지 알파값 설정
    void SetImageColor(Image image, float alpha)
    {
        Color color = image.color;
        color.a = alpha;
        image.color = color;
    }
}
