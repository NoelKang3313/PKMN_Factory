using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using TMPro;

public class FactoryUIManager : MonoBehaviour
{
    private PokemonManager pokemonManager;
    private FactoryAnimationManager animationManager;

    private AudioSource UIAudioSource;
    public GameObject PokeballButtonPreventPanel;

    public Image FadeTransition;
    private float fadeDuration = 1.0f;
    private Coroutine currentCoroutine;

    public GameObject[] Pokeballs = new GameObject[6];  // 몬스터볼 게임오브젝트
    public Button[] PokeballButtons = new Button[6];    // 몬스터볼 버튼
    private bool isPokeballButtonSelected;  // 몬스터볼 버튼 클릭확인
    private int pokeballNumber; // 몬스터볼 위치 (포켓몬 정보를 불러오기 위한 요소)
    private int selectedPokemonNumber;  // 선택한 포켓몬 수

    public Image TextboxImage;  // 택스트박스 이미지

    public Image PokemonImage;  // 포켓몬 이미지
    public TextMeshProUGUI PokemonName; // 포켓몬 이름
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
    public Sprite PokemonInfoSprite;
    public Sprite SelectedPokemonInfoSprite;    
    private bool isSelectionOver;   // 포켓몬 3마리 선택되었을 때

    public Image[] SelectedPokemonImages = new Image[3];    // 선택한 포켓몬 이미지

    [Header("Pokemon Summary")]
    public GameObject PokemonSummaryPanel;
    private bool isPokemonSummaryOpen;
    [SerializeField]
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
    public TextMeshProUGUI MoveDescription;

    // Touch Swipe
    private Vector3 fingerDownPos;
    private Vector3 fingerUpPos;
    private bool isSwiping;
    private float swipeX;
    private float swipeThreshold = 50.0f;

    // 텍스트 관련
    public TextMeshProUGUI TextboxText; // 텍스트박스 텍스트
    [SerializeField] private bool isTypingFinished;
    [SerializeField] private bool isFactoryStarted;

    void Awake()
    {
        UIAudioSource = GetComponent<AudioSource>();
        pokemonManager = GameObject.FindAnyObjectByType<PokemonManager>();
        animationManager = GameObject.FindAnyObjectByType<FactoryAnimationManager>();

        isFactoryStarted = false;
    }

    void Start()
    {
        FadeIn();

        SetButtons(PokeballButtons, PokeballButtonClicked);

        SummaryButton.onClick.AddListener(SummaryButtonClicked);
        RentButton.onClick.AddListener(RentButtonClicked);
        ReturnButton.onClick.AddListener(ReturnButtonClicked);

        ConfirmButton.onClick.AddListener(ConfirmButtonClicked);
        CancelButton.onClick.AddListener(CancelButtonClicked);

        SummaryReturnButton.onClick.AddListener(SummaryReturnButtonClicked);

        SetButtons(MoveInfoButtons, MoveInfoButtonClicked);
        SetButtons(SummaryMoveButtons, SummaryMoveButtonClicked);
        SetButtons(MoveInfoButtons, MoveInfoButtonClicked);
    }

    void Update()
    {
        SetEffectsByFade();

        PokemonFactoryAnimation();

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

    // UI Manager
    void SetEffectsByFade()
    {
        float alpha = FadeTransition.color.a;

        if (Mathf.Approximately(alpha, 0f))
        {
            animationManager.EnablePokemonInfoAnimator();
        }
        else if (Mathf.Approximately(alpha, 1f))
        {
            SceneManager.LoadScene("Battle Scene");
        }
    }

    // Animation Manager
    // 애니메이션 설정
    void PokemonFactoryAnimation()
    {
        if(animationManager.IsAnimationPlaying(animationManager.PokemonInfoAnimator, "Factory Info ON", 1.0f))
        {
            PokeballButtonPreventPanel.SetActive(false);

            if(!isTypingFinished && !isFactoryStarted)
            {
                isFactoryStarted = true;

                StartCoroutine(Typing((GameManager.instance.MyPokemons.Count + 1).ToString() + "번째 포켓몬을 선택하세요."));
            }

            if (isSelectionOver)
            {
                ButtonPanel.SetActive(true);
                SelectionButtons.SetActive(false);
                SelectionOverButtons.SetActive(true);

                for (int i = 0; i < SelectedPokemonImages.Length; i++)
                {
                    if (!selectionComplete)
                    {
                        SetImageColor(SelectedPokemonImages[i], 1f);

                        if (GameManager.instance.MyPokemons[i].hasGenderDifference)
                        {
                            if (GameManager.instance.MyPokemons[i].Gender == "Male")
                            {
                                SelectedPokemonImages[i].sprite = GameManager.instance.MyPokemons[i].Regular;
                            }
                            else
                            {
                                SelectedPokemonImages[i].sprite = GameManager.instance.MyPokemons[i].Regular_F;
                            }
                        }
                        else
                        {
                            SelectedPokemonImages[i].sprite = GameManager.instance.MyPokemons[i].Regular;
                        }
                    }
                    else
                    {
                        TextboxImage.gameObject.SetActive(false);

                        SetImageColor(SelectedPokemonImages[i], 0f);
                    }
                }
            }

            if (selectionComplete)
            {
                animationManager.SetPokemonInfoAnimator(true);

                ButtonPanel.SetActive(false);
                SelectionButtons.SetActive(false);
                SelectionOverButtons.SetActive(false);
            }
        }

        if(animationManager.IsAnimationPlaying(animationManager.PokemonInfoAnimator, "Factory Info OFF", 1.0f))
        {
            if (!selectionComplete)
            {
                if (isSelectionOver)
                {
                    PokemonInfo.GetComponent<RectTransform>().sizeDelta = new Vector2(1600, 0);
                    PokemonInfo.GetComponent<Image>().sprite = SelectedPokemonInfoSprite;

                    animationManager.PokemonInfoAnimator.SetBool("isActive", false);
                }
                else
                {
                    PokemonInfo.GetComponent<RectTransform>().sizeDelta = new Vector2(1000, 0);
                    PokemonInfo.GetComponent<Image>().sprite = PokemonInfoSprite;

                    animationManager.PokemonInfoAnimator.SetBool("isActive", false);
                }
            }
            else
            {
                FadeOut();
            }
        }
    }

    // UI Manager
    // 터치를 사용한 스와이핑
    void CheckSwipe()
    {
        float swipeDistanceX = Mathf.Abs(fingerDownPos.x - fingerUpPos.x);

        swipeX = (fingerDownPos.x - fingerUpPos.x);

        if (isSwiping && swipeDistanceX > swipeThreshold)
        {
            UIAudioSource.Play();

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
                    if(isMoveInfoOpen)
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

    // UI Manager
    // 몬스터볼 버튼
    void PokeballButtonClicked(int number)
    {
        LockButtonEnable();

        UIAudioSource.Play();

        if(pokemonManager.RandomPokemon[number].hasGenderDifference)
        {
            if(pokemonManager.RandomPokemon[number].Gender == "Male")
            {
                PokemonImage.sprite = pokemonManager.RandomPokemon[number].Regular;
                PokemonName.text = pokemonManager.RandomPokemon[number].Name;
            }
            else
            {
                PokemonImage.sprite = pokemonManager.RandomPokemon[number].Regular_F;
                PokemonName.text = pokemonManager.RandomPokemon[number].Name;
            }
        }
        else
        {
            PokemonImage.sprite = pokemonManager.RandomPokemon[number].Regular;
            PokemonName.text = pokemonManager.RandomPokemon[number].Name;
        }

        SetImageColor(PokemonImage, 1f);

        SetPokemonGenderImage(number);

        ButtonPanel.SetActive(true);
        SelectionButtons.SetActive(true);
        SelectionOverButtons.SetActive(false);

        pokeballNumber = number;

        if (!isPokeballButtonSelected)
        {
            isPokeballButtonSelected = true;

            SetImageColor(PokeballButtons[number].image, 1f);
        }

        if(pokemonManager.RandomPokemonSelected[number])
        {
            RentButton.GetComponent<Image>().sprite = RedButton;
            RentButtonText.text = "대여 취소";
        }
        else
        {
            RentButton.GetComponent<Image>().sprite = BlueButton;
            RentButtonText.text = "대여";
        }
    }

    // UI Manager
    // 상세보기 버튼
    void SummaryButtonClicked()
    {
        UIAudioSource.Play();

        isPokemonSummaryOpen = true;

        PokemonSummaryPanel.SetActive(true);

        SetImageColor(PokeballButtons[pokeballNumber].image, 0f);

        for (int i = 0; i < PokemonMoveType.Length; i++)
        {
            PokemonMoveType[i] = null;
        }

        PokemonCryAudioSource.PlayOneShot(pokemonManager.RandomPokemon[pokeballNumber].PokemonCry);

        if(pokemonManager.RandomPokemon[pokeballNumber].hasGenderDifference)
        {
            if(pokemonManager.RandomPokemon[pokeballNumber].Gender == "Male")
            {
                PokemonSummaryImage.sprite = pokemonManager.RandomPokemon[pokeballNumber].Regular;
                PokemonSummaryName.text = pokemonManager.RandomPokemon[pokeballNumber].Name;
            }
            else
            {
                PokemonSummaryImage.sprite = pokemonManager.RandomPokemon[pokeballNumber].Regular_F;
                PokemonSummaryName.text = pokemonManager.RandomPokemon[pokeballNumber].Name;
            }
        }
        else
        {
            PokemonSummaryImage.sprite = pokemonManager.RandomPokemon[pokeballNumber].Regular;
            PokemonSummaryName.text = pokemonManager.RandomPokemon[pokeballNumber].Name;
        }

        if(pokemonManager.RandomPokemon[pokeballNumber].Genderless)
        {
            SetImageColor(PokemonSummaryGenderImage, 0f);
            PokemonSummaryGenderImage.sprite = null;
        }
        else
        {
            SetImageColor(PokemonSummaryGenderImage, 1f);

            SetPokemonGenderImage(pokeballNumber);
        }

        PokedexNumber.text = pokemonManager.RandomPokemon[pokeballNumber].PokedexNumber.ToString();
        PokemonInfoName.text = pokemonManager.RandomPokemon[pokeballNumber].Name;
        OT.text = "배틀팩토리";
        ID.text = "00001";
        PokemonNature.text = pokemonManager.RandomPokemon[pokeballNumber].Nature;
        PokemonHoldItemName.text = "없음";

        SetPokemonType(pokeballNumber);

        PokemonCurrentHP.text = pokemonManager.RandomPokemon[pokeballNumber].HP.ToString();
        PokemonFullHP.text = "/" + pokemonManager.RandomPokemon[pokeballNumber].HP.ToString();
        PokemonAttack.text = pokemonManager.RandomPokemon[pokeballNumber].Attack.ToString();
        PokemonDefense.text = pokemonManager.RandomPokemon[pokeballNumber].Defense.ToString();
        PokemonSpecialAttack.text = pokemonManager.RandomPokemon[pokeballNumber].SAttack.ToString();
        PokemonSpecialDefense.text = pokemonManager.RandomPokemon[pokeballNumber].SDefense.ToString();
        PokemonSpeed.text = pokemonManager.RandomPokemon[pokeballNumber].Speed.ToString();
        PokemonAbility.text = pokemonManager.RandomPokemon[pokeballNumber].Ability;
        PokemonAbilityDescription.text = GameManager.instance.SetPokemonAbility(pokeballNumber, pokemonManager.RandomPokemon);

        for (int i = 0; i < 4; i++)
        {
            PokemonMoveName[i].text = pokemonManager.RandomPokemon[pokeballNumber].MoveName[i];
            PokemonMoveCurrentPP[i].text = pokemonManager.RandomPokemon[pokeballNumber].FullMovePP[i].ToString();
            PokemonMoveFullPP[i].text = "/" + pokemonManager.RandomPokemon[pokeballNumber].FullMovePP[i];
            PokemonMoveInfoName[i].text = pokemonManager.RandomPokemon[pokeballNumber].MoveName[i];
            PokemonMoveInfoCurrentPP[i].text = pokemonManager.RandomPokemon[pokeballNumber].FullMovePP[i].ToString();
            PokemonMoveInfoFullPP[i].text = "/" + pokemonManager.RandomPokemon[pokeballNumber].FullMovePP[i];
            PokemonMovePower[i] = pokemonManager.RandomPokemon[pokeballNumber].MovePower[i];
            PokemonMoveAccuracy[i] = pokemonManager.RandomPokemon[pokeballNumber].MoveAccuracy[i];
            PokemonMoveCategory[i] = pokemonManager.RandomPokemon[pokeballNumber].MoveCategory[i];

            PokemonMoveType[i] = pokemonManager.RandomPokemon[pokeballNumber].MoveType[i];
        }

        MoveInfoPokemonIcon.sprite = pokemonManager.RandomPokemon[pokeballNumber].Icon_Regular;

        SetPokemonMoveType(pokeballNumber);

        ButtonPanel.SetActive(false);
    }

    // UIManager
    // 대여 버튼
    void RentButtonClicked()
    {
        UIAudioSource.Play();

        if (!pokemonManager.RandomPokemonSelected[pokeballNumber])
        {
            selectedPokemonNumber++;

            GameManager.instance.MyPokemons.Add(pokemonManager.RandomPokemon[pokeballNumber]);
            pokemonManager.RandomPokemonSelected[pokeballNumber] = true;

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

            pokemonManager.RandomPokemonSelected[pokeballNumber] = false;
            GameManager.instance.MyPokemons.Remove(pokemonManager.RandomPokemon[pokeballNumber]);

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

    // UI Manager
    // 취소 버튼
    void ReturnButtonClicked()
    {
        UIAudioSource.Play();

        PokemonImage.sprite = null;
        PokemonName.text = null;

        SetImageColor(PokemonImage, 0f);
        SetImageColor(GenderImage, 0f);

        ButtonPanel.SetActive(false);

        isPokeballButtonSelected = false;

        SetImageColor(PokeballButtons[pokeballNumber].image, 0f);

        ResetButtonEnable();
    }

    // UI Manager
    // 최종 선택 확정 버튼
    void ConfirmButtonClicked()
    {
        UIAudioSource.Play();

        for (int i = 0; i < GameManager.instance.MyPokemons.Count; i++)
        {
            for(int j = 0; j < pokemonManager.RandomPokemon.Count; j++)
            {
                if(GameManager.instance.MyPokemons[i].Name == pokemonManager.RandomPokemon[j].Name)
                {
                    pokemonManager.RandomPokemon.Remove(GameManager.instance.MyPokemons[i]);

                    break;
                }
            }
        }

        for(int i = 0; i < pokemonManager.RandomPokemon.Count; i++)
        {
            GameManager.instance.PokemonLists.Add(pokemonManager.RandomPokemon[i]);
        }

        pokemonManager.RandomPokemon.RemoveRange(0, pokemonManager.RandomPokemon.Count);

        for(int i = 0; i < pokemonManager.RandomPokemonSelected.Length; i++)
        {
            pokemonManager.RandomPokemonSelected[i] = false;
        }

        selectionComplete = true;

        SelectionOverButtons.SetActive(false);
    }

    // UI Manager
    // 최종 선택 취소 버튼
    void CancelButtonClicked()
    {
        UIAudioSource.Play();

        ButtonPanel.SetActive(false);
        SelectionButtons.SetActive(false);
        SelectionOverButtons.SetActive(false);

        Pokeballs[pokeballNumber].GetComponent<Animator>().SetBool("Rented", false);
        Pokeballs[pokeballNumber].GetComponent<Animator>().SetBool("PokeballAction", false);
        GameManager.instance.MyPokemons.RemoveAt(GameManager.instance.MyPokemons.Count - 1);
        pokemonManager.RandomPokemonSelected[pokeballNumber] = false;
        selectedPokemonNumber--;

        for(int i = 0; i < SelectedPokemonImages.Length; i++)
        {
            SetImageColor(SelectedPokemonImages[i], 0f);
            SelectedPokemonImages[i].sprite = null;
        }

        animationManager.SetPokemonInfoAnimator(true);

        isSelectionOver = false;

        UpdateTextboxText();
        ResetButtonEnable();
    }

    // UI Manager
    // 상세보기 나가기 버튼
    void SummaryReturnButtonClicked()
    {
        UIAudioSource.Play();

        isPokemonSummaryOpen = false;

        PokemonSummaryPanel.SetActive(false);

        AllContainObjects.SetActive(true);

        isMoveInfoOpen = false;

        pokemonSummaryIndex = 0;
        PokemonPanelImage.sprite = SummaryImages[0];

        for (int i = 0; i < Summaries.Length; i++)
        {
            if(i == 0)
            {
                Summaries[i].SetActive(true);
            }
            else
            {
                Summaries[i].SetActive(false);
            }
        }

        //ResetPokemonSummary();

        SetImageColor(PokeballButtons[pokeballNumber].image, 1f);

        ButtonPanel.SetActive(true);
    }

    // UI Manager
    // 포켓몬 기술 버튼
    void SummaryMoveButtonClicked(int number)
    {
        UIAudioSource.Play();

        AllContainObjects.SetActive(false);

        PokemonPanelImage.sprite = SummaryImages[3];

        isMoveInfoOpen = true;

        Summaries[0].SetActive(false);
        Summaries[1].SetActive(false);
        Summaries[2].SetActive(false);
        Summaries[3].SetActive(true);

        SetMoveInfo(number);
    }

    // UI Manager
    void SearchMove(int number)
    {
        for(int i = 0; i < GameManager.instance.PokemonMove.Count; i++)
        {
            if(pokemonManager.RandomPokemon[pokeballNumber].MoveName[number] == GameManager.instance.PokemonMove[i]["MoveName"].ToString())
            {
                MoveDescription.text = GameManager.instance.PokemonMove[i]["Description"].ToString();

                break;
            }
        }
    }

    // UI Manager
    // 포켓몬 기술 정보 버튼
    void MoveInfoButtonClicked(int number)
    {
        UIAudioSource.Play();

        SetMoveInfo(number);
    }

    // UI Manager
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

    // UI Manager
    // 포켓몬 갯수 텍스트 업데이트
    void UpdateTextboxText()
    {
        isTypingFinished = false;

        if (selectedPokemonNumber < 3)
        {
            ResetTextboxText();
            StartCoroutine(Typing((GameManager.instance.MyPokemons.Count + 1).ToString() + "번째 포켓몬을 선택하세요."));
        }
        else
        {
            ResetTextboxText();
            StartCoroutine(Typing("선택하신 " + GameManager.instance.MyPokemons.Count + "마리 포켓몬으로 가시겠습니까?"));
        }
    }

    void ResetTextboxText()
    {
        if (TextboxText.text != null)
            TextboxText.text = "";
    }

    IEnumerator Typing(string message)
    {
        for (int i = 0; i < message.Length; i++)
        {
            TextboxText.text += message[i];

            yield return new WaitForSeconds(0.01f);
        }

        isTypingFinished = true;
    }

    // UI Manager
    // 취소 버튼 클릭 시, 모든 몬스터볼 버튼 활성
    void ResetButtonEnable()
    {
        for (int i = 0; i < PokeballButtons.Length; i++)
        {
            PokeballButtons[i].GetComponent<Button>().enabled = true;
        }
    }

    // UI Manager
    // 모든 포켓몬 선택 시, 몬스터볼 버튼 비활성
    void LockButtonEnable()
    {
        for (int i = 0; i < PokeballButtons.Length; i++)
        {
            PokeballButtons[i].GetComponent<Button>().enabled = false;
        }
    }

    // UI Manager
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

                    animationManager.SetPokemonInfoAnimator(true);
                }
            }
        }
    }

    // UI Manager
    // 포켓몬 성별 이미지 설정
    void SetPokemonGenderImage(int number)
    {
        if (pokemonManager.RandomPokemon[number].Genderless)
        {
            SetImageColor(GenderImage, 0f);
        }
        else
        {
            SetImageColor(GenderImage, 1f);

            if (pokemonManager.RandomPokemon[number].Gender == "Male")
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

    // UI Manager
    // 포켓몬 타입 설정
    void SetPokemonType(int number)
    {
        string Type1 = pokemonManager.RandomPokemon[number].Type1;

        if (pokemonManager.PokemonTypes.TryGetValue(Type1, out Sprite sprite1))
        {
            PokemonType1.sprite = sprite1;
            MoveInfoPokemonType1.sprite = sprite1;
        }

        string Type2 = pokemonManager.RandomPokemon[number].Type2;

        if (pokemonManager.RandomPokemon[number].Type2 == "")
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

            if (pokemonManager.PokemonTypes.TryGetValue(Type2, out Sprite sprite2))
            {
                PokemonType2.sprite = sprite2;
                MoveInfoPokemonType2.sprite = sprite2;
            }
        }
    }

    // UI Manager
    // 포켓몬 기술 타입 설정
    void SetPokemonMoveType(int number)
    {
        for (int i = 0; i < PokemonMoveType.Length; i++)
        {
            string MoveType = pokemonManager.RandomPokemon[number].MoveType[i];

            if (pokemonManager.PokemonTypes.TryGetValue(MoveType, out Sprite sprite))
            {
                PokemonMoveTypeImage[i].sprite = sprite;
                PokemonMoveInfoTypeImage[i].sprite = sprite;
            }
        }
    }

    // UI Manager
    // 버튼 람다식 설정
    void SetButtons(Button[] buttons, UnityAction<int> buttonName)
    {
        for (int i = 0; i < buttons.Length; i++)
        {
            int number = i;

            buttons[i].onClick.AddListener(() => buttonName(number));
        }
    }

    // UI Manager
    void FadeIn()
    {
        if(currentCoroutine != null)
        {
            StopCoroutine(currentCoroutine);
        }

        StartCoroutine(Fade(1f, 0f));
    }

    // UI Manager
    void FadeOut()
    {
        if (currentCoroutine != null)
        {
            StopCoroutine(currentCoroutine);
        }

        StartCoroutine(Fade(0f, 1f));
    }

    // UI Manager
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