using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    [SerializeField] private int deskSceneIndex;
    [SerializeField] private GameObject creditsPanel;

    public void OnStartPressed()
    {
        SceneManager.LoadScene(deskSceneIndex);
    }

    public void OnCreditsPressed()
    {
        creditsPanel.SetActive(true);
    }

    public void OnClosePressed()
    {
        creditsPanel.SetActive(false);
    }

    public void OnQuitPressed()
    {
        Application.Quit();
    }
}
