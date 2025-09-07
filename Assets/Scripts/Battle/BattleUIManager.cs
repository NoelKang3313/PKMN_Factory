using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using TMPro;
using System.Collections;

public class BattleUIManager : MonoBehaviour
{
    private AudioSource UIAudioSource;

    public Image FadeTransition;
    private float fadeDuration = 1.0f;
    private Coroutine currentCoroutine;

    public GameObject BattleMainPanel;
    public GameObject SelectionPanel;   // 싸우다, 가방, 포켓몬 버튼 묶음
    public GameObject MovePanel;    // 기술 버튼 (기술 버튼 4개, 메가진화 버튼, 취소 버튼) 묶음

    public Image PlayerImage;   // Player 이미지
    public Image NPCImage;  // NPC 이미지
    public Animator NPCAnimator;
    public TextMeshProUGUI TextboxText; //  텍스트박스 텍스트
    [SerializeField] private bool isTrainerEnter;   // 배틀 시 트레이너 입장했는지 확인

    public GameObject MaintenancePanel;
    public GameObject BattlePanel;
    public GameObject BattleSelections;

    public Animator PlayerPokeballUIAnimator;
    public Animator OpponentPokeballUIAnimator;
    public Button TextboxButton;
    public Image PokeballPrefab;
    private Image InstantiatedPokeball;
    private Animator InstantiatedPokeballAnimator;
    private bool isTrainerAppear;

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
    public Button BagItemButton;

    public List<Button> BagHoldItemButtons = new List<Button>();
    public List<Button> BagHealItemButtons = new List<Button>();
    public List<Button> BagBerryButtons = new List<Button>();
    public List<Button> BagKeyItemButtons = new List<Button>();

    public GameObject BagInfoPanel;
    public GameObject ItemInfo;
    public Image ItemInfoImage;
    public TextMeshProUGUI ItemInfoName;
    public TextMeshProUGUI ItemInfoAmount;
    public TextMeshProUGUI ItemInfoDescription;
    public Button BagItemUseButton;
    public Button BagItemCancelButton;

    private bool isPartyPokemonButtonClicked;
    private bool isBagButtonClicked;
    public GameObject PartyPokemonViewerPanel;

    [Header("Shop")]
    public TextMeshProUGUI MoneyText;
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

    [Header("Hold")]
    public Button[] HoldButtons = new Button[8];
    public GameObject HoldItemScrollview;
    public GameObject[] HoldItemContents = new GameObject[8];
    public Button[] HoldItemButtons = new Button[11];
    public Item[] HoldItemInfos = new Item[11];
    public Button[] BattleItemButtons = new Button[11];
    public Item[] BattleItemInfos = new Item[11];
    public Button[] TypeBoostButtons = new Button[18];
    public Item[] TypeBoostInfos = new Item[18];
    public Button[] PlateButtons = new Button[19];
    public Item[] PlateInfos = new Item[19];
    public Button[] DriveButtons = new Button[4];
    public Item[] DriveInfos = new Item[4];
    public Button[] MegaStoneButtons = new Button[46];
    public Item[] MegaStoneInfos = new Item[46];
    public Button[] MemoryButtons = new Button[17];
    public Item[] MemoryInfos = new Item[17];
    public Button[] MaskButtons = new Button[4];
    public Item[] MaskInfos = new Item[4];

    [Header("Heal")]
    public Button[] HealButtons = new Button[11];
    public Item[] HealInfos = new Item[11];

    [Header("Berries")]
    public Button[] BerryButtons = new Button[10];
    public Item[] BerryInfos = new Item[10];

    [Header("Key")]
    public Button[] KeyButtons = new Button[29];
    public Item[] KeyInfos = new Item[29];

    [Header("Party Pokemon Panel")]
    private bool isPokemonSummaryOpen = true;
    [SerializeField] private int pokemonSummaryIndex = 0;
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

    public GameObject PokemonSelectedPanel;
    public Button[] PokemonSelectedButtons = new Button[4];
    [SerializeField] private int selectedPokemonIndex;
    private Button SelectedPokemonButton;
    public Sprite PartyPokemonButtonSprite;
    public Sprite SelectedPokemonButtonSprite;
    [SerializeField] private bool isSwitchButtonClicked;

    [SerializeField] private int switchIndexA;
    [SerializeField] private int switchIndexB;

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
        FadeIn();
        
        SetBattleMaintenance();

        InstantiateMyPokemons();

        MyPokemonImage.sprite = GameManager.instance.MyPokemons[0].Regular_Back;

        TextboxButton.onClick.AddListener(TextboxButtonClicked);

        SetButtons(MaintenanceButtons, MaintenanceButtonClicked);
        SetButtons(SelectionButtons, SelectionButtonClicked);
        SetButtons(BagCategoryButtons, BagCategoryButtonClicked);

        BagReturnButton.onClick.AddListener(BagReturnButtonClicked);

        SetButtons(MoveButtons, MoveButtonClicked);

        CancelButton.onClick.AddListener(CancelButtonClicked);

        CancelPartyPokemonButton.onClick.AddListener(PokemonCancelButtonClicked);

        SetButtons(SummaryMoveButtons, SummaryMoveButtonClicked);
        SetButtons(MoveInfoButtons, MoveInfoButtonClicked);
        SetButtons(ItemCategoryButtons, ItemCategoryButtonClicked);

        ItemPurchaseButton.onClick.AddListener(ItemPurchaseButtonClicked);
        ItemCancelButton.onClick.AddListener(ItemCancelButtonClicked);
        ShopReturnButton.onClick.AddListener(ShopReturnButtonClicked);

        BagItemUseButton.onClick.AddListener(BagItemUseButtonClicked);
        BagItemCancelButton.onClick.AddListener(BagItemCancelButtonClicked);

        SetButtons(HoldButtons, HoldButtonClicked);
        SetButtons(HoldItemButtons, HoldItemButtonClicked);
        SetButtons(BattleItemButtons, BattleItemButtonClicked);
        SetButtons(TypeBoostButtons, TypeBoostButtonClicked);
        SetButtons(PlateButtons, PlateButtonClicked);
        SetButtons(DriveButtons, DriveButtonClicked);
        SetButtons(MegaStoneButtons, MegaStoneButtonClicked);
        SetButtons(MemoryButtons, MemoryButtonClicked);
        SetButtons(MaskButtons, MaskButtonClicked);
        SetButtons(HealButtons, HealButtonClicked);
        SetButtons(BerryButtons, BerryButtonClicked);
        SetButtons(KeyButtons, KeyButtonClicked);

        SetButtons(PokemonSelectedButtons, PokemonSelectedButtonClicked);

        for(int i = 0; i < MyPartyPokemonButtons.Count; i++)
        {
            Button btn = MyPartyPokemonButtons[i];
            btn.onClick.AddListener(() => MyPartyPokemonButtonClicked(btn));
        }
    }

    // 버튼 람다식 설정
    void SetButtons(Button[] buttons, UnityAction<int> buttonName)
    {
        for (int i = 0; i < buttons.Length; i++)
        {
            int number = i;

            buttons[i].onClick.AddListener(() => buttonName(number));
        }
    }

    void SetListButtons(List<Button> buttons, UnityAction<int> buttonName)
    {
        for (int i = 0; i < buttons.Count; i++)
        {
            int number = i;

            buttons[i].onClick.AddListener(() => buttonName(number));
        }
    }

    void Update()
    {
        SetEffectsByFade();

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

        if (isSwiping && swipeDistanceX > swipeThreshold && canSwipe)
        {
            // 오른쪽 스와이프
            if (swipeX > 0)
            {
                switch (pokemonSummaryIndex)
                {
                    case 0:
                    case 1:
                        UIAudioSource.Play();
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
                        UIAudioSource.Play();

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

            switch (pokemonSummaryIndex)
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
                    if (isMoveInfoOpen)
                    {
                        break;
                    }
                    else
                    {
                        Summaries[0].SetActive(false);
                        Summaries[1].SetActive(false);
                        Summaries[2].SetActive(true);
                    }

                    break;
            }
        }
    }

    void SetEffectsByFade()
    {
        float alpha = FadeTransition.color.a;

        if(Mathf.Approximately(alpha,0f) && !GameManager.instance.BattleStart)
        {
            FadeTransition.gameObject.SetActive(false);

            NPCAnimator.SetBool("isAppear", true);
        }
    }

    // 배틀씬 애니메이션
    void PokemonBattleAnimation()
    {
        if(NPCAnimator.GetCurrentAnimatorStateInfo(0).IsName("-NPC_Move") &&
            NPCAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime>= 1.0f &&
            GameManager.instance.BattleStart)
        {
            NPCAnimator.SetBool("isAppear", true);

            int NPCRandom = Random.Range(0, GameManager.instance.NPC.Count);
            NPCImage.sprite = GameManager.instance.NPC[NPCRandom].Sprite;
            GameManager.instance.ActiveTrainer = GameManager.instance.NPC[NPCRandom];
            isTrainerEnter = true;

            PlayerPokeballUIAnimator.SetBool("isActive", true);
            OpponentPokeballUIAnimator.SetBool("isActive", true);

            GameManager.instance.CheckAudioSource(GameManager.instance.TrainerBattleClip);
        }
        else if(NPCAnimator.GetCurrentAnimatorStateInfo(0).IsName("NPC_Move") &&
            NPCAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f &&
            GameManager.instance.BattleStart && NPCAnimator.GetBool("isAppear"))
        {
            TextboxText.text = GameManager.instance.CheckTrainerName(GameManager.instance.ActiveTrainer).ToString()
                + "가 승부를 걸어왔다!";

            if (isTrainerEnter)
            {
                for (int i = 0; i < 3; i++)
                {
                    int pokemonRandom = Random.Range(0, GameManager.instance.PokemonLists.Count);
                    GameManager.instance.ActiveTrainer.Pokemon[i] = GameManager.instance.PokemonLists[pokemonRandom];
                }

                isTrainerEnter = false;
            }

            TextboxButton.interactable = true;
        }

        if (InstantiatedPokeball != null && InstantiatedPokeballAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f)
        {
            Destroy(InstantiatedPokeball);
        }
    }

    void MaintenanceButtonClicked(int number)
    {
        UIAudioSource.Play();

        TextboxText.text = "무엇을 도와드릴까요?";

        switch (number)
        {
            case 0:
                {
                    isPartyPokemonButtonClicked = true;
                    isBagButtonClicked = false;

                    PartyPokemonPanel.SetActive(true);
                    PartyPokemonViewerPanel.SetActive(true);

                    SetImageColor(PartyPokemonSprite, 0);

                    break;
                }

            case 1:
                {
                    isPartyPokemonButtonClicked = false;
                    isBagButtonClicked = true;

                    BagPanel.SetActive(true);
                    PartyPokemonViewerPanel.SetActive(false);

                    SetBagImage();

                    ResetBagUI();

                    SetListButtons(BagHoldItemButtons, BagHoldItemButtonClicked);
                    SetListButtons(BagHealItemButtons, BagHealItemButtonClicked);
                    SetListButtons(BagBerryButtons, BagBerryButtonClicked);
                    SetListButtons(BagKeyItemButtons, BagKeyItemButtonClicked);

                    break;
                }

            case 2:
                {
                    ShopPanel.SetActive(true);

                    MoneyText.text = GameManager.instance.Money.ToString("N0") + "원";

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
                        FadeOut();
                    }

                    break;
                }

            case 4:
                {
                    GameManager.instance.BattleStart = true;

                    NPCAnimator.SetBool("isAppear", false);

                    MaintenancePanel.SetActive(false);
                    BattlePanel.SetActive(true);

                    TextboxText.text = "";

                    break;
                }
        }
    }

    void TextboxButtonClicked()
    {
        NPCAnimator.SetBool("isAppear", false);

        TextboxText.text = GameManager.instance.CheckTrainerName(GameManager.instance.ActiveTrainer).ToString()
            + "가 " + GameManager.instance.ActiveTrainer.Pokemon[0].Name + "를 내보냈다!";

        PlayerPokeballUIAnimator.SetBool("isActive", false);
        OpponentPokeballUIAnimator.SetBool("isActive", false);

        InstantiatedPokeball = Instantiate(PokeballPrefab);
        InstantiatedPokeball.transform.SetParent(BattleMainPanel.transform);
        InstantiatedPokeball.GetComponent<RectTransform>().anchoredPosition = new Vector2(-30, 300);

        InstantiatedPokeballAnimator = InstantiatedPokeball.GetComponent<Animator>();
    }

    void BagHoldItemButtonClicked(int number)
    {
        ItemInfo.SetActive(true);

        ItemInfoImage.sprite = GameManager.instance.HoldItems[number].ItemSprite;
        ItemInfoName.text = GameManager.instance.HoldItems[number].ItemName;
        ItemInfoAmount.text = "X" + GameManager.instance.HoldItems[number].ItemAmount;
        ItemInfoDescription.text = GameManager.instance.HoldItems[number].ItemDescription;
    }

    void BagHealItemButtonClicked(int number)
    {
        ItemInfo.SetActive(true);

        ItemInfoImage.sprite = GameManager.instance.HealItems[number].ItemSprite;
        ItemInfoName.text = GameManager.instance.HealItems[number].ItemName;
        ItemInfoAmount.text = "X" + GameManager.instance.HealItems[number].ItemAmount;
        ItemInfoDescription.text = GameManager.instance.HealItems[number].ItemDescription;
    }

    void BagBerryButtonClicked(int number)
    {
        ItemInfo.SetActive(true);

        ItemInfoImage.sprite = GameManager.instance.Berries[number].ItemSprite;
        ItemInfoName.text = GameManager.instance.Berries[number].ItemName;
        ItemInfoAmount.text = "X" + GameManager.instance.Berries[number].ItemAmount;
        ItemInfoDescription.text = GameManager.instance.Berries[number].ItemDescription;
    }

    void BagKeyItemButtonClicked(int number)
    {
        ItemInfo.SetActive(true);

        ItemInfoImage.sprite = GameManager.instance.KeyItems[number].ItemSprite;
        ItemInfoName.text = GameManager.instance.KeyItems[number].ItemName;
        ItemInfoAmount.text = "X" + GameManager.instance.KeyItems[number].ItemAmount;
        ItemInfoDescription.text = GameManager.instance.KeyItems[number].ItemDescription;
    }

    void BagItemUseButtonClicked()
    {
        BagInfoPanel.SetActive(false);
        PartyPokemonViewerPanel.SetActive(true);
    }

    void BagItemCancelButtonClicked()
    {
        BagInfoPanel.SetActive(true);
        ItemInfo.SetActive(false);
        PartyPokemonViewerPanel.SetActive(false);
    }

    void SetBagImage()
    {
        for (int i = 0; i < BagUIImages.Length; i++)
        {
            BagUIImages[i].sprite = MaleBagUISprites[i];
        }
    }

    void SetBagUI(int index)
    {
        for (int i = 0; i < 4; i++)
        {
            if (index == i)
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
        for (int i = 0; i < 4; i++)
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

        BagInfoPanel.SetActive(true);
        ItemInfo.SetActive(false);

        BagPanel.SetActive(false);
        PartyPokemonViewerPanel.SetActive(false);
    }

    void SetShopUI(int index)
    {
        for (int i = 0; i < 4; i++)
        {
            if (index == i)
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
        for (int i = 0; i < 4; i++)
        {
            ShopItemCategoryContents[i].SetActive(false);
            PurchaseCancelButton.SetActive(false);
            ItemCategoryButtons[i].GetComponent<Image>().sprite = ItemCategoryButtonDeselectedSprite[i];
        }
    }

    void ItemCategoryButtonClicked(int number)
    {
        UIAudioSource.Play();

        ResetShopItemInfo();

        SetShopUI(number);
        ShopScrollRect.content = ShopItemCategoryContents[number].GetComponent<RectTransform>();

        if (number == 1 || number == 2 || number == 3)
        {
            for (int i = 0; i < HoldItemContents.Length; i++)
            {
                HoldItemContents[i].SetActive(false);
            }

            HoldItemScrollview.gameObject.SetActive(false);
        }

        ShopItemCategoryContents[number].GetComponent<RectTransform>().anchoredPosition =
                        new Vector2(ShopItemCategoryContents[number].GetComponent<RectTransform>().anchoredPosition.x, 0);
    }

    // 상점 아이템 구매 버튼
    void ItemPurchaseButtonClicked()
    {
        UIAudioSource.Play();

        if (GameManager.instance.Money >= shopSelectedItem.ItemPrice)
        {
            GameManager.instance.Money -= shopSelectedItem.ItemPrice;

            MoneyText.text = GameManager.instance.Money.ToString("N0") + "원";

            if (shopSelectedItem.ItemType == "도구")
            {
                GameManager.instance.HoldItems.Add(shopSelectedItem);
                shopSelectedItem.ItemAmount++;
                CheckItemLists(GameManager.instance.HoldItems, BagItemContents[0], shopSelectedItem, BagHoldItemButtons);
            }
            else if (shopSelectedItem.ItemType == "회복")
            {
                GameManager.instance.HealItems.Add(shopSelectedItem);
                shopSelectedItem.ItemAmount++;
                CheckItemLists(GameManager.instance.HealItems, BagItemContents[1], shopSelectedItem, BagHealItemButtons);
            }
            else if (shopSelectedItem.ItemType == "열매")
            {
                GameManager.instance.Berries.Add(shopSelectedItem);
                shopSelectedItem.ItemAmount++;
                CheckItemLists(GameManager.instance.Berries, BagItemContents[2], shopSelectedItem, BagBerryButtons);
            }
            else if (shopSelectedItem.ItemType == "중요")
            {
                GameManager.instance.KeyItems.Add(shopSelectedItem);
                shopSelectedItem.ItemAmount++;
                CheckItemLists(GameManager.instance.KeyItems, BagItemContents[3], shopSelectedItem, BagKeyItemButtons);
            }
        }

        ResetShopItemInfo();
    }

    // 아이템 중복확인 및 가방 아이템 버튼 생성
    void CheckItemLists(List<Item> itemList, GameObject bagItemContent, Item purchasedItem, List<Button> bagItemButtonList)
    {
        Button itemButton = Instantiate(BagItemButton);
        itemButton.transform.SetParent(bagItemContent.transform);
        itemButton.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = purchasedItem.ItemName;
        bagItemButtonList.Add(itemButton);

        for (int i = 0; i < itemList.Count; i++)
        {
            for (int j = i + 1; j < itemList.Count; j++)
            {
                if (itemList[i] == itemList[j])
                {
                    itemList.RemoveAt(j);
                    Destroy(bagItemButtonList[j].gameObject);
                    bagItemButtonList.RemoveAt(j);
                }
            }
        }
    }

    // 상점 아이템 구매취소 버튼
    void ItemCancelButtonClicked()
    {
        UIAudioSource.Play();

        ResetShopItemInfo();
    }

    // 상점 UI 초기화
    void ResetShopItemInfo()
    {
        SetImageColor(ShopItemImage, 0f);
        PurchaseCancelButton.SetActive(false);
        ShopItemDescription.text = "";
        ShopItemPriceText.gameObject.SetActive(false);
        ShopItemPrice.text = "";

        shopSelectedItem = null;
    }

    // 상점 뒤로가기 버튼
    void ShopReturnButtonClicked()
    {
        UIAudioSource.Play();

        ShopPanel.SetActive(false);

        SetImageColor(ShopItemImage, 0f);
        ShopItemDescription.text = "";
        ShopItemPriceText.gameObject.SetActive(false);
        ShopItemPrice.text = "";
    }

    void HoldButtonClicked(int number)
    {
        UIAudioSource.Play();

        HoldItemScrollview.gameObject.SetActive(true);
        HoldItemScrollview.GetComponent<ScrollRect>().content = HoldItemContents[number].GetComponent<RectTransform>();

        for (int i = 0; i < HoldItemContents.Length; i++)
        {
            if (i == number)
            {
                HoldItemContents[i].SetActive(true);
            }
            else
            {
                HoldItemContents[i].SetActive(false);
            }
        }

        HoldItemContents[number].GetComponent<RectTransform>().anchoredPosition =
            new Vector2(HoldItemContents[number].GetComponent<RectTransform>().anchoredPosition.x, 0);
    }

    // 상점 버튼 설정
    void SetShopButton(int number, Item[] info)
    {
        UIAudioSource.Play();

        SetImageColor(ShopItemImage, 1f);

        ShopItemImage.sprite = info[number].ItemSprite;
        ShopItemDescription.text = info[number].ItemDescription;
        ShopItemPriceText.gameObject.SetActive(true);
        ShopItemPriceText.text = info[number].ItemPrice.ToString("N0") + "원";
        shopSelectedItem = info[number];

        PurchaseCancelButton.SetActive(true);
    }

    void HoldItemButtonClicked(int number)
    {
        SetShopButton(number, HoldItemInfos);
    }

    void BattleItemButtonClicked(int number)
    {
        SetShopButton(number, BattleItemInfos);
    }

    void TypeBoostButtonClicked(int number)
    {
        SetShopButton(number, TypeBoostInfos);
    }

    void PlateButtonClicked(int number)
    {
        SetShopButton(number, PlateInfos);
    }

    void DriveButtonClicked(int number)
    {
        SetShopButton(number, DriveInfos);
    }

    void MegaStoneButtonClicked(int number)
    {
        SetShopButton(number, MegaStoneInfos);
    }

    void MemoryButtonClicked(int number)
    {
        SetShopButton(number, MemoryInfos);
    }

    void MaskButtonClicked(int number)
    {
        SetShopButton(number, MaskInfos);
    }

    void HealButtonClicked(int number)
    {
        SetShopButton(number, HealInfos);
    }

    void BerryButtonClicked(int number)
    {
        SetShopButton(number, BerryInfos);
    }

    void KeyButtonClicked(int number)
    {
        SetShopButton(number, KeyInfos);
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

        if (GameManager.instance.CurrentPokemon.CurrentMovePP[number] < 0)
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

            if (TypeButtons.TryGetValue(MoveType, out Sprite sprite))
            {
                MoveButtons[i].GetComponent<Image>().sprite = sprite;
            }

            MoveButtons[i].transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = GameManager.instance.CurrentPokemon.MoveName[i];
            MoveButtons[i].transform.GetChild(1).GetComponent<TextMeshProUGUI>().text =
                GameManager.instance.CurrentPokemon.CurrentMovePP[i].ToString() + "  " + GameManager.instance.CurrentPokemon.FullMovePP[i].ToString();
        }
    }

    // 가지고 있는 포켓몬 확인하여 버튼 생성
    void InstantiateMyPokemons()
    {
        if (MyPartyPokemonButtons.Count == 0)
        {
            for (int i = 0; i < GameManager.instance.MyPokemons.Count; i++)
            {
                Button partyPokemonButton = Instantiate(PartyPokemonButton);

                partyPokemonButton.name = GameManager.instance.MyPokemons[i].Name;

                partyPokemonButton.transform.SetParent(PartyPokemonButtons.transform);

                partyPokemonButton.transform.localScale = Vector3.one;

                MyPartyPokemonButtons.Add(partyPokemonButton);

                partyPokemonButton.transform.GetChild(0).GetComponent<Image>().sprite = GameManager.instance.MyPokemons[i].Icon_Regular;

                partyPokemonButton.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = GameManager.instance.MyPokemons[i].Name;

                partyPokemonButton.transform.GetChild(3).GetComponent<TextMeshProUGUI>().text = GameManager.instance.MyPokemons[i].HP.ToString();

                partyPokemonButton.transform.GetChild(4).GetComponent<TextMeshProUGUI>().text = "/" + GameManager.instance.MyPokemons[i].HP;

                if (GameManager.instance.MyPokemons[i].Genderless)
                {
                    SetImageColor(partyPokemonButton.transform.GetChild(5).GetComponent<Image>(), 0);
                }
                else
                {
                    SetImageColor(partyPokemonButton.transform.GetChild(5).GetComponent<Image>(), 1);

                    if (GameManager.instance.MyPokemons[i].Gender == "Male")
                    {
                        partyPokemonButton.transform.GetChild(5).GetComponent<Image>().sprite = MaleSprite;
                    }
                    else
                    {
                        partyPokemonButton.transform.GetChild(5).GetComponent<Image>().sprite = FemaleSprite;
                    }
                }

                SetImageColor(partyPokemonButton.transform.GetChild(6).GetComponent<Image>(), 0);
            }
        }

        //UpdateMyPokemonButtons();
    }

    //// 가지고 있는 포켓몬 변경이 있을 시
    //void UpdateMyPokemonButtons()
    //{
    //    //SetListButtons(MyPartyPokemonButtons, MyPokemonButtonClicked);
    //    SetListButtons(MyBagPokemonButtons, MyBagPokemonButtonClicked);
    //}

    void PokemonSelectedButtonClicked(int number)
    {
        UIAudioSource.Play();

        switch (number)
        {
            case 0:
                {
                    PokemonSelectedPanel.SetActive(false);

                    canSwipe = true;

                    isMoveInfoOpen = false;

                    AllContainObjects.SetActive(true);

                    SetImageColor(PartyPokemonSprite, 1);

                    pokemonSummaryIndex = 0;
                    PokemonPanelImage.sprite = SummaryImages[0];

                    Summaries[0].SetActive(true);
                    Summaries[1].SetActive(false);
                    Summaries[2].SetActive(false);
                    Summaries[3].SetActive(false);

                    PokemonCryAudioSource.PlayOneShot(GameManager.instance.MyPokemons[selectedPokemonIndex].PokemonCry);

                    PartyPokemonSprite.sprite = GameManager.instance.MyPokemons[selectedPokemonIndex].Regular;
                    PokemonName.text = GameManager.instance.MyPokemons[selectedPokemonIndex].Name;

                    PokedexNumber.text = GameManager.instance.MyPokemons[selectedPokemonIndex].PokedexNumber.ToString();
                    PokemonInfoName.text = GameManager.instance.MyPokemons[selectedPokemonIndex].Name;
                    OT.text = "배틀팩토리";
                    ID.text = "00001";
                    PokemonNature.text = GameManager.instance.MyPokemons[selectedPokemonIndex].Nature;
                    PokemonHoldItemName.text = "없음";

                    if (GameManager.instance.MyPokemons[selectedPokemonIndex].Genderless)
                    {
                        SetImageColor(PokemonGender, 0f);
                    }
                    else
                    {
                        SetImageColor(PokemonGender, 1f);

                        if (GameManager.instance.MyPokemons[selectedPokemonIndex].Gender == "Male")
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

                    SetPokemonType(selectedPokemonIndex);

                    PokemonCurrentHP.text = GameManager.instance.MyPokemons[selectedPokemonIndex].HP.ToString();
                    PokemonFullHP.text = "/" + GameManager.instance.MyPokemons[selectedPokemonIndex].HP.ToString();
                    PokemonAttack.text = GameManager.instance.MyPokemons[selectedPokemonIndex].Attack.ToString();
                    PokemonDefense.text = GameManager.instance.MyPokemons[selectedPokemonIndex].Defense.ToString();
                    PokemonSpecialAttack.text = GameManager.instance.MyPokemons[selectedPokemonIndex].SAttack.ToString();
                    PokemonSpecialDefense.text = GameManager.instance.MyPokemons[selectedPokemonIndex].SDefense.ToString();
                    PokemonSpeed.text = GameManager.instance.MyPokemons[selectedPokemonIndex].Speed.ToString();
                    PokemonAbility.text = GameManager.instance.MyPokemons[selectedPokemonIndex].Ability;
                    PokemonAbilityDescription.text = GameManager.instance.SetPokemonAbility(selectedPokemonIndex, GameManager.instance.MyPokemons);

                    for (int i = 0; i < 4; i++)
                    {
                        PokemonMoveName[i].text = GameManager.instance.MyPokemons[selectedPokemonIndex].MoveName[i];
                        PokemonMoveCurrentPP[i].text = GameManager.instance.MyPokemons[selectedPokemonIndex].FullMovePP[i].ToString();
                        PokemonMoveFullPP[i].text = "/" + GameManager.instance.MyPokemons[selectedPokemonIndex].FullMovePP[i];
                        PokemonMoveInfoName[i].text = GameManager.instance.MyPokemons[selectedPokemonIndex].MoveName[i];
                        PokemonMoveInfoCurrentPP[i].text = GameManager.instance.MyPokemons[selectedPokemonIndex].FullMovePP[i].ToString();
                        PokemonMoveInfoFullPP[i].text = "/" + GameManager.instance.MyPokemons[selectedPokemonIndex].FullMovePP[i];
                        PokemonMovePower[i] = GameManager.instance.MyPokemons[selectedPokemonIndex].MovePower[i];
                        PokemonMoveAccuracy[i] = GameManager.instance.MyPokemons[selectedPokemonIndex].MoveAccuracy[i];
                        PokemonMoveCategory[i] = GameManager.instance.MyPokemons[selectedPokemonIndex].MoveCategory[i];

                        PokemonMoveType[i] = GameManager.instance.MyPokemons[selectedPokemonIndex].MoveType[i];
                    }

                    MoveInfoPokemonIcon.sprite = GameManager.instance.MyPokemons[selectedPokemonIndex].Icon_Regular;

                    SetPokemonMoveType(selectedPokemonIndex);

                    break;
                }

            case 1:
                {
                    switchIndexA = selectedPokemonIndex;

                    SelectedPokemonButton = MyPartyPokemonButtons[selectedPokemonIndex];
                    SelectedPokemonButton.GetComponent<Image>().sprite = SelectedPokemonButtonSprite;

                    isSwitchButtonClicked = true;

                    PokemonSelectedPanel.SetActive(false);

                    break;
                }

            case 2:
                {


                    break;
                }

            case 3:
                {
                    PokemonSelectedPanel.SetActive(false);

                    break;
                }
        }
    }

    void MyPartyPokemonButtonClicked(Button button)
    {
        if(isPartyPokemonButtonClicked && !isBagButtonClicked)
        {
            int currentIndex = button.transform.GetSiblingIndex();

            if (!isSwitchButtonClicked)
            {
                selectedPokemonIndex = currentIndex; // 현재 위치 기준 index
                UIAudioSource.Play();
                PokemonSelectedPanel.SetActive(true);
            }
            else
            {
                switchIndexB = currentIndex; // 현재 위치 기준 index

                // 버튼 위치 바꾸기
                SelectedPokemonButton.transform.SetSiblingIndex(switchIndexB);
                button.transform.SetSiblingIndex(switchIndexA);

                // 내부 데이터 갱신
                SwitchMyPokemonPosition(switchIndexA, switchIndexB);

                SelectedPokemonButton.GetComponent<Image>().sprite = PartyPokemonButtonSprite;

                isSwitchButtonClicked = false;
            }
        }
        else if(!isPartyPokemonButtonClicked && isBagButtonClicked)
        {
            Debug.Log("Bag Party Pokemon Button Clicked");
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
    void PokemonCancelButtonClicked()
    {
        UIAudioSource.Play();

        if (isPartyPokemonButtonClicked && !isBagButtonClicked)
        {
            PartyPokemonPanel.SetActive(false);
            PartyPokemonViewerPanel.SetActive(false);

            ResetPokemonSummary();

            canSwipe = false;
        }
        else if(!isPartyPokemonButtonClicked && isBagButtonClicked)
        {
            BagInfoPanel.SetActive(true);
            ItemInfo.SetActive(false);
            PartyPokemonViewerPanel.SetActive(false);   
        }
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
            if (GameManager.instance.MyPokemons[selectedPokemonIndex].MoveName[number] == GameManager.instance.PokemonMove[i]["MoveName"].ToString())
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

    void SwitchMyPokemonPosition(int indexA, int indexB)
    {
        Pokemon temp = GameManager.instance.MyPokemons[indexA];
        GameManager.instance.MyPokemons[indexA] = GameManager.instance.MyPokemons[indexB];
        GameManager.instance.MyPokemons[indexB] = temp;

        Button btn = MyPartyPokemonButtons[indexA];
        MyPartyPokemonButtons[indexA] = MyPartyPokemonButtons[indexB];
        MyPartyPokemonButtons[indexB] = btn;
    }

    void FadeIn()
    {
        if (currentCoroutine != null)
        {
            StopCoroutine(currentCoroutine);
        }

        StartCoroutine(Fade(1f, 0f));
    }

    void FadeOut()
    {
        if (currentCoroutine != null)
        {
            StopCoroutine(currentCoroutine);
        }

        StartCoroutine(Fade(0f, 1f));
    }

    IEnumerator Fade(float start, float end)
    {
        float currentTime = 0f;
        Color color = FadeTransition.color;

        while (currentTime < fadeDuration)
        {
            currentTime += Time.deltaTime;
            float t = currentTime / fadeDuration;
            color.a = Mathf.Lerp(start, end, t);
            FadeTransition.color = color;
            yield return null;
        }

        color.a = end;
        FadeTransition.color = color;
    }
}
