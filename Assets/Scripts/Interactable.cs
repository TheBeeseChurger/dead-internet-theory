using UnityEngine;

public interface IInteractable
{
    void OnHoverEnter();
    void OnHoverExit();
    void OnInteract();
}

//[RequireComponent(typeof(Highlightable))]
public abstract class Interactable : MonoBehaviour, IInteractable
{
    // protected Highlightable highlight;

    protected virtual void Awake()
    {
        //highlight = GetComponent<Highlightable>();
    }

    public virtual void OnHoverEnter()
    {
        //highlight.Highlight(true);
    }

    public virtual void OnHoverExit()
    {
        //highlight.Highlight(false);
    }

    public abstract void OnInteract();
}
