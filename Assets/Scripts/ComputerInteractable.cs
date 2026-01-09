using UnityEngine;

public class ComputerInteractable : Interactable
{
    [SerializeField] private GameObject desktopScreen;
    [SerializeField] private CameraController cc;
    [SerializeField] private InteractManager im;

    private bool isComputerOpen = false;

    public override void OnInteract()
    {
        if (isComputerOpen) return;

        desktopScreen.SetActive(true);
        cc.CanMove = false;
        im.interactionEnabled = false;
        isComputerOpen = true;
    }

    public async void CloseScreen()
    {
        if (!isComputerOpen) return;

        desktopScreen.SetActive(false);
        await Awaitable.WaitForSecondsAsync(0.5f);
        if (!isComputerOpen) return;
        cc.CanMove = true;
        im.interactionEnabled = true;
        isComputerOpen = false;
    }
}
