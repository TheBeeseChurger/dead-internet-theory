using UnityEngine;

public class DesktopManager : MonoBehaviour
{
    [SerializeField] private GameObject curateAppScreen;
    [SerializeField] private GameObject chatAppScreen;

    public void OnChatButton(bool value)
    {
        if (curateAppScreen.activeSelf) curateAppScreen.SetActive(false);
        chatAppScreen.SetActive(value);
    }

    public void OnCurateButton(bool value)
    {
        if (chatAppScreen.activeSelf) chatAppScreen.SetActive(false);
        curateAppScreen.SetActive(value);
    }
}
