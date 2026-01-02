using UnityEngine;
using UnityEngine.InputSystem;

public class InteractManager : MonoBehaviour
{
    [SerializeField] private LayerMask interactableLayers;
    [SerializeField] private InputActionReference interactAction;
    [SerializeField] private GameObject keybindHint;

    public bool interactionEnabled = true;

    private IInteractable currentInteractable;
    private GameObject currentGameObject;
    private int currentLayer;

    private LayerMask outlinedLayer;

    private void Start()
    {
        outlinedLayer = LayerMask.NameToLayer("Outline");
    }

    private void OnEnable()
    {
        interactAction.action.Enable();
    }

    private void OnDisable()
    {
        interactAction.action.Disable();
    }
    void Update()
    {
        if (!interactionEnabled)
        {
            ClearCurrent();
            return;
        }

        if (Mouse.current == null)
        {
            ClearCurrent();
            return;
        }

        Ray ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());
        bool isHit = Physics.Raycast(ray, out RaycastHit hit, 50f, interactableLayers);

        if (isHit)
        {
            IInteractable interactable = hit.collider.GetComponentInParent<IInteractable>();
            
            if (interactable != currentInteractable)
            {
                ClearCurrent();
                currentInteractable = interactable;
                currentInteractable?.OnHoverEnter();

                currentLayer = hit.collider.gameObject.layer;
                currentGameObject = hit.collider.gameObject;
                currentGameObject.layer = outlinedLayer;

                keybindHint.SetActive(true);
            }

            if (interactAction.action.WasPressedThisFrame())
            {
                currentInteractable?.OnInteract();
            }
        }
        else
        {
            ClearCurrent();
        }
    }

    private void ClearCurrent()
    {
        if (currentInteractable != null)
        {
            currentInteractable.OnHoverExit();
            currentInteractable = null;
            currentGameObject.layer = currentLayer;
            currentGameObject = null;

            keybindHint.SetActive(false);
        }
    }
}
