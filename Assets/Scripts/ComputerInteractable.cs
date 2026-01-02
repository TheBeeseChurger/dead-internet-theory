using UnityEngine;

public class ComputerInteractable : Interactable
{
    [SerializeField] private GameObject curatorScreen;
    [SerializeField] private CameraController cc;
    [SerializeField] private InteractManager im;

    public override void OnInteract()
    {
        curatorScreen.SetActive(true);
        cc.CanMove = false;
        im.interactionEnabled = false;
    }

    public void CloseScreen()
    {
        curatorScreen.SetActive(false);
        cc.CanMove = true;
        im.interactionEnabled = true;
    }
}
