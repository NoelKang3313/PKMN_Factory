using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class FactoryUIManager : MonoBehaviour
{
    public Animator FadeAnimator;

    public GameObject[] Pokeballs = new GameObject[6];  // 몬스터볼 게임오브젝트
    public Button[] PokeballButtons = new Button[6];    // 몬스터볼 버튼
    private bool isPokeballButtonSelected;  // 몬스터볼 버튼 클릭확인
    private int pokeballNumber; // 몬스터볼 위치 (포켓몬 정보를 불러오기 위한 요소)
    private int selectedPokemonNumber;  // 선택한 포켓몬 수

    public Image PokemonImage;  // 포켓몬 이미지
    public TextMeshProUGUI PokemonName; // 포켓몬 이름
    public TextMeshProUGUI TextboxText; // 텍스트박스 텍스트
    public Image GenderImage;   // 성별 이미지
    public Sprite MaleImage;    // 남성 이미지
    public Sprite FemaleImage;  // 여성 이미지

    public GameObject ButtonPanel;

    [Header("Selection Buttons")]
    public GameObject SelectionButtons;
    public Button SummaryButton;
    public Button RentButton;
    public TextMeshProUGUI RentButtonText;
    public Button ReturnButton;
    public Sprite BlueButton;
    public Sprite RedButton;

    [Header("Selection Over Buttons")]
    public GameObject SelectionOverButtons;
    public Button ConfirmButton;
    [SerializeField]
    private bool selectionComplete; // 포켓몬 최종선택
    public Button CancelButton;

    [Header("Factory Animators")]
    public GameObject PokemonInfo;
    private Animator PokemonInfoAnimator;
    public Sprite PokemonInfoSprite;
    public Sprite SelectedPokemonInfoSprite;    
    private bool isSelectionOver;   // 포켓몬 3마리 선택되었을 때

    public Image[] SelectedPokemonImages = new Image[3];    // 선택한 포켓몬 이미지

    [Header("Pokemon Summary")]
    public GameObject PokemonSummaryPanel;
    private bool isPokemonSummaryOpen;
    private int pokemonSummaryIndex = 0;
    public Image PokemonPanelImage;
    public GameObject[] Summaries = new GameObject[4];
    public Sprite[] SummaryImages = new Sprite[4];
    public AudioSource PokemonCryAudioSource;
    public GameObject AllContainObjects;
    public TextMeshProUGUI PokemonSummaryName;
    public Image PokemonSummaryImage;
    public Image PokemonSummaryGenderImage;
    public TextMeshProUGUI PokemonHoldItemName;
    public Image HoldItemImage;
    public Button SummaryReturnButton;

    [Header("Pokemon Status")]
    public TextMeshProUGUI PokedexNumber;
    public TextMeshProUGUI PokemonInfoName;
    public TextMeshProUGUI OT;
    public TextMeshProUGUI ID;
    public TextMeshProUGUI PokemonNature;
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

    [Header("Pokemon Move Infos")]
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

    //[Header("Pokemon Info")]
    //public TextMeshProUGUI PokemonNumber;
    //public TextMeshProUGUI PokemonInfoName;
    //public TextMeshProUGUI OwnerName;
    //public TextMeshProUGUI ID;
    //public TextMeshProUGUI PokemonNature;
    //public TextMeshProUGUI PokemonHoldItemName;
    //public Image PokemonType1;
    //public Image PokemonType2;

    //[Header("Pokemon Skills")]
    //public TextMeshProUGUI PokemonHP;
    //public TextMeshProUGUI PokemonAttack;
    //public TextMeshProUGUI PokemonDefense;
    //public TextMeshProUGUI PokemonSpecialAttack;
    //public TextMeshProUGUI PokemonSpecialDefense;
    //public TextMeshProUGUI PokemonSpeed;
    //public TextMeshProUGUI PokemonAbility;
    //public TextMeshProUGUI PokemonAbilityInfoText;

    //[Header("Pokemon Moves")]
    //public TextMeshProUGUI[] PokemonMove = new TextMeshProUGUI[4];
    //public TextMeshProUGUI[] PokemonMovePP = new TextMeshProUGUI[4];
    //private string[] PokemonMoveType = new string[4];
    //public Image[] PokemonMoveTypeImage = new Image[4];
    //private int[] PokemonMovePower = new int[4];
    //private int[] PokemonMoveAccuracy = new int[4];
    //private string[] PokemonMoveCategory = new string[4];

    //public Button[] MoveInfoButtons = new Button[4];

    //[Header("Pokemon Move Info")]
    //public TextMeshProUGUI MovePowerText;
    //public TextMeshProUGUI MoveAccuracyText;
    //public Sprite PhysicalCategory;
    //public Sprite SpecialCategory;
    //public Sprite StatusCategory;
    //public Image MoveInfoCategory;
    //public Image MoveInfoPokemonIcon;
    //public Image MoveInfoPokemonType1;
    //public Image MoveInfoPokemonType2;

    // Touch Swipe
    private Vector3 fingerDownPos;
    private Vector3 fingerUpPos;
    private bool isSwiping;
    private float swipeX;
    private float swipeThreshold = 50.0f;

    [Header("Type Images")]
    public Sprite[] PokemonTypeSprites = new Sprite[18];
    private Dictionary<string, Sprite> PokemonTypes;

    void Awake()
    {
        TextboxText.text = "1번째 포켓몬을 선택하세요.";

        PokemonInfoAnimator = PokemonInfo.GetComponent<Animator>();
        PokemonInfoAnimator.enabled = false;

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
        for (int i = 0; i < PokeballButtons.Length; i++)
        {
            int number = i;

            PokeballButtons[i].onClick.AddListener(() => PokeballButtonClicked(number));
        }

        SummaryButton.onClick.AddListener(SummaryButtonClicked);
        RentButton.onClick.AddListener(RentButtonClicked);
        ReturnButton.onClick.AddListener(ReturnButtonClicked);

        ConfirmButton.onClick.AddListener(ConfirmButtonClicked);
        CancelButton.onClick.AddListener(CancelButtonClicked);

        SummaryReturnButton.onClick.AddListener(SummaryReturnButtonClicked);

        for (int i = 0; i < MoveInfoButtons.Length; i++)
        {
            int number = i;

            MoveInfoButtons[i].onClick.AddListener(() => MoveInfoButtonClicked(number));
        }

        for (int i = 0; i < SummaryMoveButtons.Length; i++)
        {
            int number = i;

            SummaryMoveButtons[i].onClick.AddListener(() => SummaryMoveButtonClicked(number));
        }

        for (int i = 0; i < MoveInfoButtons.Length; i++)
        {
            int number = i;

            MoveInfoButtons[i].onClick.AddListener(() => MoveInfoButtonClicked(number));
        }
    }

    void Update()
    {
        PokemonInfoAnimation();

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

        CheckMyPokemons();
    }

    void PokemonInfoAnimation()
    {
        if (FadeAnimator.GetCurrentAnimatorStateInfo(0).IsName("Fade In") &&
            FadeAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f)
        {
            PokemonInfoAnimator.enabled = true;
        }

        if (PokemonInfoAnimator.GetCurrentAnimatorStateInfo(0).IsName("Factory Info OFF") &&
            PokemonInfoAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f)
        {
            if (isSelectionOver)
            {
                PokemonInfo.GetComponent<RectTransform>().sizeDelta = new Vector2(1600, 0);
                PokemonInfo.GetComponent<Image>().sprite = SelectedPokemonInfoSprite;

                ButtonPanel.SetActive(true);
                SelectionButtons.SetActive(false);
                SelectionOverButtons.SetActive(true);
            }
            else
            {
                PokemonInfo.GetComponent<RectTransform>().sizeDelta = new Vector2(1000, 0);
                PokemonInfo.GetComponent<Image>().sprite = PokemonInfoSprite;
            }

            PokemonInfoAnimator.SetBool("isActive", false);

            if(selectionComplete)
            {
                FadeAnimator.Play("Fade Out");
            }
        }

        if (selectionComplete)
        {
            PokemonInfoAnimator.SetBool("isActive", true);

            ButtonPanel.SetActive(false);
            SelectionButtons.SetActive(false);
            SelectionOverButtons.SetActive(false);
        }

        if (PokemonInfoAnimator.GetCurrentAnimatorStateInfo(0).IsName("Factory Info ON") &&
            PokemonInfoAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f)
        {
            if (isSelectionOver)
            {
                for (int i = 0; i < SelectedPokemonImages.Length; i++)
                {
                    if(!selectionComplete)
                    {
                        SetImageColor(SelectedPokemonImages[i], 1f);
                        SelectedPokemonImages[i].sprite = GameManager.instance.MyPokemons[i].Regular;
                    }
                    else
                    {
                        SetImageColor(SelectedPokemonImages[i], 0f);
                    }
                }
            }
        }

        if(FadeAnimator.GetCurrentAnimatorStateInfo(0).IsName("Fade Out") &&
            FadeAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f)
        {
            GameManager.instance.Maintenance = true;

            SceneManager.LoadScene("Battle Scene");
        }
    }

    // 터치를 사용한 스와이핑
    void CheckSwipe()
    {
        float swipeDistanceX = Mathf.Abs(fingerDownPos.x - fingerUpPos.x);

        swipeX = (fingerDownPos.x - fingerUpPos.x);

        if (isSwiping && swipeDistanceX > swipeThreshold)
        {
            // 오른쪽으로 이동
            if (swipeX > 0)
            {
                //Debug.Log("Right Swipe");

                switch (pokemonSummaryIndex)
                {
                    case 0:
                    case 1:
                        {
                            ++pokemonSummaryIndex;
                            PokemonPanelImage.sprite = SummaryImages[pokemonSummaryIndex];

                            break;
                        }
                }
            }
            // 왼쪽으로 이동
            else
            {
                //Debug.Log("Left Swipe");

                switch (pokemonSummaryIndex)
                {
                    case 1:
                    case 2:
                        if(isMoveInfoOpen)
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
                    Summaries[0].SetActive(false);
                    Summaries[1].SetActive(false);
                    Summaries[2].SetActive(true);

                    break;
            }
        }
    }

    // 몬스터볼 버튼
    void PokeballButtonClicked(int number)
    {
        PokemonImage.sprite = GameManager.instance.RandomPokemon[number].Regular;
        PokemonName.text = GameManager.instance.RandomPokemon[number].Name;

        SetImageColor(PokemonImage, 1f);

        SetPokemonGender(number);

        ButtonPanel.SetActive(true);
        SelectionButtons.SetActive(true);
        SelectionOverButtons.SetActive(false);

        pokeballNumber = number;

        if (!isPokeballButtonSelected)
        {
            isPokeballButtonSelected = true;

            SetImageColor(PokeballButtons[number].image, 1f);
        }

        LockButtonEnable();

        if(GameManager.instance.RandomPokemonSelected[number])
        {
            RentButton.GetComponent<Image>().sprite = RedButton;
            RentButtonText.text = "Release";
        }
        else
        {
            RentButton.GetComponent<Image>().sprite = BlueButton;
            RentButtonText.text = "Rent";
        }
    }

    // 상세보기 버튼
    void SummaryButtonClicked()
    {
        isPokemonSummaryOpen = true;

        PokemonSummaryPanel.SetActive(true);

        SetImageColor(PokeballButtons[pokeballNumber].image, 0f);

        for (int i = 0; i < PokemonMoveType.Length; i++)
        {
            PokemonMoveType[i] = null;
        }

        PokemonCryAudioSource.PlayOneShot(GameManager.instance.RandomPokemon[pokeballNumber].PokemonCry);

        PokemonSummaryImage.sprite = GameManager.instance.RandomPokemon[pokeballNumber].Regular;
        PokemonSummaryName.text = GameManager.instance.RandomPokemon[pokeballNumber].Name;

        if(GameManager.instance.RandomPokemon[pokeballNumber].Genderless)
        {
            SetImageColor(PokemonSummaryGenderImage, 0f);
            PokemonSummaryGenderImage.sprite = null;
        }
        else
        {
            SetImageColor(PokemonSummaryGenderImage, 1f);

            SetPokemonGender(pokeballNumber);
        }

        PokedexNumber.text = GameManager.instance.RandomPokemon[pokeballNumber].PokedexNumber.ToString();
        PokemonInfoName.text = GameManager.instance.RandomPokemon[pokeballNumber].Name;
        OT.text = "배틀팩토리";
        ID.text = "00001";
        PokemonNature.text = GameManager.instance.RandomPokemon[pokeballNumber].Nature;
        PokemonHoldItemName.text = "없음";

        SetPokemonType(pokeballNumber);

        PokemonCurrentHP.text = GameManager.instance.RandomPokemon[pokeballNumber].HP.ToString();
        PokemonFullHP.text = "/" + GameManager.instance.RandomPokemon[pokeballNumber].HP.ToString();
        PokemonAttack.text = GameManager.instance.RandomPokemon[pokeballNumber].Attack.ToString();
        PokemonDefense.text = GameManager.instance.RandomPokemon[pokeballNumber].Defense.ToString();
        PokemonSpecialAttack.text = GameManager.instance.RandomPokemon[pokeballNumber].SAttack.ToString();
        PokemonSpecialDefense.text = GameManager.instance.RandomPokemon[pokeballNumber].SDefense.ToString();
        PokemonSpeed.text = GameManager.instance.RandomPokemon[pokeballNumber].Speed.ToString();
        PokemonAbility.text = GameManager.instance.RandomPokemon[pokeballNumber].Ability;
        PokemonAbilityDescription.text = GameManager.instance.AbilityDescription[pokeballNumber];

        for (int i = 0; i < 4; i++)
        {
            PokemonMoveName[i].text = GameManager.instance.RandomPokemon[pokeballNumber].MoveName[i];
            PokemonMoveCurrentPP[i].text = GameManager.instance.RandomPokemon[pokeballNumber].FullMovePP[i].ToString();
            PokemonMoveFullPP[i].text = "/" + GameManager.instance.RandomPokemon[pokeballNumber].FullMovePP[i];
            PokemonMoveInfoName[i].text = GameManager.instance.RandomPokemon[pokeballNumber].MoveName[i];
            PokemonMoveInfoCurrentPP[i].text = GameManager.instance.RandomPokemon[pokeballNumber].FullMovePP[i].ToString();
            PokemonMoveInfoFullPP[i].text = "/" + GameManager.instance.RandomPokemon[pokeballNumber].FullMovePP[i];
            PokemonMovePower[i] = GameManager.instance.RandomPokemon[pokeballNumber].MovePower[i];
            PokemonMoveAccuracy[i] = GameManager.instance.RandomPokemon[pokeballNumber].MoveAccuracy[i];
            PokemonMoveCategory[i] = GameManager.instance.RandomPokemon[pokeballNumber].MoveCategory[i];

            PokemonMoveType[i] = GameManager.instance.RandomPokemon[pokeballNumber].MoveType[i];
        }

        MoveInfoPokemonIcon.sprite = GameManager.instance.RandomPokemon[pokeballNumber].Icon_Regular;

        SetPokemonMoveType(pokeballNumber);

        ButtonPanel.SetActive(false);
    }

    // 대여 버튼
    void RentButtonClicked()
    {
        if(!GameManager.instance.RandomPokemonSelected[pokeballNumber])
        {
            selectedPokemonNumber++;

            GameManager.instance.MyPokemons.Add(GameManager.instance.RandomPokemon[pokeballNumber]);
            GameManager.instance.RandomPokemonSelected[pokeballNumber] = true;

            PokemonImage.sprite = null;
            PokemonName.text = null;

            SetImageColor(PokemonImage, 0f);
            SetImageColor(GenderImage, 0f);

            ButtonPanel.SetActive(false);
            SelectionButtons.SetActive(false);
            SelectionOverButtons.SetActive(false);

            isPokeballButtonSelected = false;

            SetImageColor(PokeballButtons[pokeballNumber].image, 0f);

            Pokeballs[pokeballNumber].GetComponent<Animator>().SetBool("Rented", true);
            Pokeballs[pokeballNumber].GetComponent<Animator>().SetBool("PokeballAction", true);

            ResetButtonEnable();

            UpdateTextboxText();
        }
        else
        {
            selectedPokemonNumber--;

            GameManager.instance.RandomPokemonSelected[pokeballNumber] = false;
            GameManager.instance.MyPokemons.Remove(GameManager.instance.RandomPokemon[pokeballNumber]);

            PokemonImage.sprite = null;
            PokemonName.text = null;

            SetImageColor(PokemonImage, 0f);
            SetImageColor(GenderImage, 0f);

            ButtonPanel.SetActive(false);
            SelectionButtons.SetActive(false);
            SelectionOverButtons.SetActive(false);

            isPokeballButtonSelected = false;

            SetImageColor(PokeballButtons[pokeballNumber].image, 0f);

            Pokeballs[pokeballNumber].GetComponent<Animator>().SetBool("Rented", false);
            Pokeballs[pokeballNumber].GetComponent<Animator>().SetBool("PokeballAction", false);

            ResetButtonEnable();

            UpdateTextboxText();
        }
    }

    // 취소 버튼
    void ReturnButtonClicked()
    {
        PokemonImage.sprite = null;
        PokemonName.text = null;

        SetImageColor(PokemonImage, 0f);
        SetImageColor(GenderImage, 0f);

        ButtonPanel.SetActive(false);

        isPokeballButtonSelected = false;

        SetImageColor(PokeballButtons[pokeballNumber].image, 0f);

        ResetButtonEnable();
    }

    // 최종 선택 확정 버튼
    void ConfirmButtonClicked()
    {
        for(int i = 0; i < GameManager.instance.MyPokemons.Count; i++)
        {
            for(int j = 0; j < GameManager.instance.RandomPokemon.Count; j++)
            {
                if(GameManager.instance.MyPokemons[i].Name == GameManager.instance.RandomPokemon[j].Name)
                {
                    GameManager.instance.RandomPokemon.Remove(GameManager.instance.MyPokemons[i]);

                    break;
                }
            }
        }

        for(int i = 0; i < GameManager.instance.RandomPokemon.Count; i++)
        {
            GameManager.instance.PokemonLists.Add(GameManager.instance.RandomPokemon[i]);
        }

        GameManager.instance.RandomPokemon.RemoveRange(0, GameManager.instance.RandomPokemon.Count);

        for(int i = 0; i < GameManager.instance.RandomPokemonSelected.Length; i++)
        {
            GameManager.instance.RandomPokemonSelected[i] = false;
        }

        selectionComplete = true;

        SelectionOverButtons.SetActive(false);
    }

    // 최종 선택 취소 버튼
    void CancelButtonClicked()
    {
        ButtonPanel.SetActive(false);
        SelectionButtons.SetActive(false);
        SelectionOverButtons.SetActive(false);

        Pokeballs[pokeballNumber].GetComponent<Animator>().SetBool("Rented", false);
        Pokeballs[pokeballNumber].GetComponent<Animator>().SetBool("PokeballAction", false);
        GameManager.instance.MyPokemons.RemoveAt(GameManager.instance.MyPokemons.Count - 1);
        GameManager.instance.RandomPokemonSelected[pokeballNumber] = false;
        selectedPokemonNumber--;

        for(int i = 0; i < SelectedPokemonImages.Length; i++)
        {
            SetImageColor(SelectedPokemonImages[i], 0f);
            SelectedPokemonImages[i].sprite = null;
        }

        PokemonInfoAnimator.SetBool("isActive", true);

        isSelectionOver = false;

        UpdateTextboxText();
        ResetButtonEnable();
    }

    // 상세보기 나가기 버튼
    void SummaryReturnButtonClicked()
    {
        isPokemonSummaryOpen = false;

        PokemonSummaryPanel.SetActive(false);

        AllContainObjects.SetActive(true);

        isMoveInfoOpen = false;

        pokemonSummaryIndex = 0;
        PokemonPanelImage.sprite = SummaryImages[0];

        for (int i = 0; i < Summaries.Length; i++)
        {
            Summaries[i].SetActive(false);
        }

        ResetPokemonSummary();

        SetImageColor(PokeballButtons[pokeballNumber].image, 1f);

        ButtonPanel.SetActive(true);
    }

    // 포켓몬 기술 버튼
    void SummaryMoveButtonClicked(int number)
    {
        AllContainObjects.SetActive(false);

        PokemonPanelImage.sprite = SummaryImages[3];

        isMoveInfoOpen = true;

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
        }
    }

    // 포켓몬 설명에 있는 모든 설정값 초기화
    void ResetPokemonSummary()
    {
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

    // 포켓몬 갯수 텍스트 업데이트
    void UpdateTextboxText()
    {
        if(GameManager.instance.FactoryPokemonSelection)
        {
            if (selectedPokemonNumber < 3)
            {
                TextboxText.text = (GameManager.instance.MyPokemons.Count + 1).ToString() + "번째 포켓몬을 선택하세요.";
            }
            else
            {
                TextboxText.text = ("선택하신 " + GameManager.instance.MyPokemons.Count + "마리 포켓몬으로 가시겠습니까?");
            }
        }
    }

    // 취소 버튼 클릭 시, 모든 몬스터볼 버튼 활성
    void ResetButtonEnable()
    {
        for (int i = 0; i < PokeballButtons.Length; i++)
        {
            PokeballButtons[i].GetComponent<Button>().enabled = true;
        }
    }

    // 모든 포켓몬 선택 시, 몬스터볼 버튼 비활성
    void LockButtonEnable()
    {
        for (int i = 0; i < PokeballButtons.Length; i++)
        {
            PokeballButtons[i].GetComponent<Button>().enabled = false;
        }
    }

    // 이미지 알파값 설정
    void SetImageColor(Image image, float alpha)
    {
        Color color = image.color;
        color.a = alpha;
        image.color = color;
    }

    // 선택가능한 포켓몬(보유 포켓몬) 특정 마리 수 도달 시 실행
    void CheckMyPokemons()
    {
        if(GameManager.instance.FactoryPokemonSelection)
        {
            if (selectedPokemonNumber == 3)
            {
                SelectionButtons.SetActive(false);

                LockButtonEnable();

                if(!isSelectionOver)
                {
                    isSelectionOver = true;

                    PokemonInfoAnimator.SetBool("isActive", true);
                }
            }
        }
    }

    // 포켓몬 성별 이미지 설정
    void SetPokemonGender(int number)
    {
        if (GameManager.instance.RandomPokemon[number].Genderless)
        {
            SetImageColor(GenderImage, 0f);
        }
        else
        {
            SetImageColor(GenderImage, 1f);

            if (GameManager.instance.RandomPokemon[number].Gender == "Male")
            {
                GenderImage.sprite = MaleImage;
                PokemonSummaryGenderImage.sprite = MaleImage;
            }
            else
            {
                GenderImage.sprite = FemaleImage;
                PokemonSummaryGenderImage.sprite = FemaleImage;
            }
        }
    }

    // 포켓몬 타입 설정
    void SetPokemonType(int number)
    {
        string Type1 = GameManager.instance.RandomPokemon[number].Type1;

        if (PokemonTypes.TryGetValue(Type1, out Sprite sprite1))
        {
            PokemonType1.sprite = sprite1;
            MoveInfoPokemonType1.sprite = sprite1;
        }

        string Type2 = GameManager.instance.RandomPokemon[number].Type2;

        if (GameManager.instance.RandomPokemon[number].Type2 == "")
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
        for (int i = 0; i < PokemonMoveType.Length; i++)
        {
            string MoveType = GameManager.instance.RandomPokemon[number].MoveType[i];

            if (PokemonTypes.TryGetValue(MoveType, out Sprite sprite))
            {
                PokemonMoveTypeImage[i].sprite = sprite;
                PokemonMoveInfoTypeImage[i].sprite = sprite;
            }
        }
    }
}