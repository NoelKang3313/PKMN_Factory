using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class DoTweenTest : MonoBehaviour
{
    public GameObject Camera;

    public Image Image;
    public Button MoveButton;
    public Button ShakeButton;

    void Start()
    {
        MoveButton.onClick.AddListener(MoveButtonClick);
        ShakeButton.onClick.AddListener(ShakeButtonClick);
    }

    void Update()
    {
        
    }

    void MoveButtonClick()
    {
        Image.rectTransform.DOAnchorPos(new Vector2(500f, 0), 1f);
    }

    void ShakeButtonClick()
    {
        //Image.rectTransform.DOShakeAnchorPos(1f, 30f);
        Camera.transform.DOShakePosition(1f, 30f);
    }
}
