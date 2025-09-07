using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TextTest : MonoBehaviour
{
    public TextMeshProUGUI Text;
    private bool isTypingFinished;
    private string message = "가나다라마바사아자차카타파하";
    private string message1 = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";

    public Button Button;

    void Start()
    {
        StartCoroutine(Typing(message));

        Button.onClick.AddListener(ButtonClick);
    }

    void Update()
    {
        if(isTypingFinished)
        {
            Button.interactable = true;
        }
        else
        {
            Button.interactable = false;
        }
    }

    IEnumerator Typing(string message)
    {
        isTypingFinished = false;

        for(int i  = 0; i < message.Length; i++)
        {
            Text.text += message[i];

            yield return new WaitForSeconds(0.5f);
        }

        isTypingFinished = true;
    }

    void ButtonClick()
    {
        Text.text = "";

        StartCoroutine(Typing(message1));
    }
}
