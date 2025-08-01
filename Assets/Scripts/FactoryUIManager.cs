using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
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
    public Button CancelButton;

    [Header("Factory Animators")]
    public GameObject PokemonInfo;
    private Animator PokemonInfoAnimator;
    public Sprite PokemonInfoSprite;
    public Sprite SelectedPokemonInfoSprite;    
    private bool isSelectionOver;

    public Image[] SelectedPokemonImages = new Image[3];    // 선택한 포켓몬 이미지

    [Header("Pokemon Summary")]
    public GameObject PokemonSummaryPanel;
    public AudioSource PokemonCryAudioSource;
    private bool isPokemonSummaryOpen;
    public GameObject[] PokemonSummaries = new GameObject[3];
    public GameObject PokemonMoveInfo;
    private int pokemonSummaryIndex = 0;
    public Image PokemonSummaryImage;
    public TextMeshProUGUI PokemonSummaryName;
    public Image PokemonSummaryGenderImage;
    public Button SummaryReturnButton;

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
    public TextMeshProUGUI PokemonAbilityInfoText;

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

        for(int i = 0; i < MoveInfoButtons.Length; i++)
        {
            int number = i;

            MoveInfoButtons[i].onClick.AddListener(() => MoveInfoButtonClicked(number));
        }
    }

    void Update()
    {
        if(FadeAnimator.GetCurrentAnimatorStateInfo(0).IsName("Fade In") &&
            FadeAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f)
        {
            PokemonInfoAnimator.enabled = true;
        }

        if (PokemonInfoAnimator.GetCurrentAnimatorStateInfo(0).IsName("Factory Info OFF") &&
            PokemonInfoAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f)
        {
            if(isSelectionOver)
            {
                PokemonInfo.GetComponent<RectTransform>().sizeDelta = new Vector2(1600, 0);
                PokemonInfo.GetComponent<Image>().sprite = SelectedPokemonInfoSprite;
            }
            else
            {
                PokemonInfo.GetComponent<RectTransform>().sizeDelta = new Vector2(1000, 0);
                PokemonInfo.GetComponent<Image>().sprite = PokemonInfoSprite;
            }
            

            PokemonInfoAnimator.SetBool("isActive", false);
        }

        if (PokemonInfoAnimator.GetCurrentAnimatorStateInfo(0).IsName("Factory Info ON") &&
            PokemonInfoAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f)
        {
            if(isSelectionOver)
            {
                for (int i = 0; i < SelectedPokemonImages.Length; i++)
                {
                    SetImageColor(SelectedPokemonImages[i], 1f);
                    SelectedPokemonImages[i].sprite = GameManager.instance.MyPokemons[i].Regular;
                }
            }
        }

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
                //Debug.Log("Left Swipe");

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

                            if (PokemonMoveInfo.activeSelf)
                            {
                                PokemonMoveInfo.SetActive(false);
                            }

                            break;
                        }
                }
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

        PokemonNumber.text = GameManager.instance.RandomPokemon[pokeballNumber].PokedexNumber.ToString();
        PokemonInfoName.text = GameManager.instance.RandomPokemon[pokeballNumber].Name;
        OwnerName.text = "배틀팩토리";
        ID.text = "00001";
        PokemonNature.text = GameManager.instance.RandomPokemon[pokeballNumber].Nature;
        PokemonHoldItemName.text = "없음";

        SetPokemonType(pokeballNumber);

        PokemonHP.text = GameManager.instance.RandomPokemon[pokeballNumber].HP.ToString() + "/" + GameManager.instance.RandomPokemon[pokeballNumber].HP.ToString();
        PokemonAttack.text = GameManager.instance.RandomPokemon[pokeballNumber].Attack.ToString();
        PokemonDefense.text = GameManager.instance.RandomPokemon[pokeballNumber].Defense.ToString();
        PokemonSpecialAttack.text = GameManager.instance.RandomPokemon[pokeballNumber].SAttack.ToString();
        PokemonSpecialDefense.text = GameManager.instance.RandomPokemon[pokeballNumber].SDefense.ToString();
        PokemonSpeed.text = GameManager.instance.RandomPokemon[pokeballNumber].Speed.ToString();
        PokemonAbility.text = GameManager.instance.RandomPokemon[pokeballNumber].Ability;

        for (int i = 0; i < 4; i++)
        {
            PokemonMove[i].text = GameManager.instance.RandomPokemon[pokeballNumber].MoveName[i];
            PokemonMovePP[i].text = GameManager.instance.RandomPokemon[pokeballNumber].FullMovePP[i].ToString() + "/" + GameManager.instance.RandomPokemon[pokeballNumber].FullMovePP[i].ToString();
            PokemonMovePower[i] = GameManager.instance.RandomPokemon[pokeballNumber].MovePower[i];
            PokemonMoveAccuracy[i] = GameManager.instance.RandomPokemon[pokeballNumber].MoveAccuracy[i];
            PokemonMoveCategory[i] = GameManager.instance.RandomPokemon[pokeballNumber].MoveCategory[i];

            PokemonMoveType[i] = GameManager.instance.RandomPokemon[pokeballNumber].MoveType[i];
        }

        if (PokemonMoveInfo.activeSelf)
        {
            PokemonMoveInfo.SetActive(false);
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

        ResetPokemonSummaryIndex();

        PokemonSummaries[pokemonSummaryIndex].SetActive(true);

        SetImageColor(PokeballButtons[pokeballNumber].image, 1f);

        ButtonPanel.SetActive(true);
    }

    // 포켓몬 상세보기 기술 버튼
    void MoveInfoButtonClicked(int number)
    {
        PokemonMoveInfo.SetActive(true);

        for (int i = 0; i < MoveInfoButtons.Length; i++)
        {
            MovePowerText.text = PokemonMovePower[number].ToString();
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

    // 포켓몬 갯수 텍스트 업데이트
    void UpdateTextboxText()
    {
        if(GameManager.instance.FirstSelection)
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
        if(GameManager.instance.FirstSelection)
        {
            if (selectedPokemonNumber == 3)
            {
                ButtonPanel.SetActive(true);
                SelectionButtons.SetActive(false);
                SelectionOverButtons.SetActive(true);

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
            PokemonType1.gameObject.GetComponent<RectTransform>().anchoredPosition = new Vector2(240, 200);
            MoveInfoPokemonType1.gameObject.GetComponent<RectTransform>().anchoredPosition = new Vector2(290, 380);

            PokemonType2.sprite = null;
            MoveInfoPokemonType2.sprite = null;
            PokemonType2.gameObject.SetActive(false);
            MoveInfoPokemonType2.gameObject.SetActive(false);
        }
        else
        {
            PokemonType1.gameObject.GetComponent<RectTransform>().anchoredPosition = new Vector2(130, 200);
            MoveInfoPokemonType1.gameObject.GetComponent<RectTransform>().anchoredPosition = new Vector2(30, 380);

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
            }
        }
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
}