using UnityEngine;
using TMPro;

public class MessageManager : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private TMP_Text messageText;
    [SerializeField] private RectTransform backgroundRect;

    [Header("Settings")]
    [SerializeField] private float maxWidth = 800f;
    [SerializeField] private float leftrightPadding = 15f;

    private void Start()
    {
        SetText("This is a really very long message that is likely to overflow to the next line.");
    }

    public void SetText(string text)
    {
        //messageText.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, maxWidth);
        messageText.text = text;
    }
}
