using UnityEngine;
using UnityEngine.InputSystem;

public class PhotographDragger : MonoBehaviour
{
    public bool draggingDisabled = false;

    private Camera cam;
    private Rigidbody rb;
    private bool dragging = false;
    private float dragDistance;

    private void Awake()
    {
        cam = Camera.main;
        rb = GetComponent<Rigidbody>();
    }

    private void OnRelease()
    {
        dragging = false;
        rb.useGravity = true;
    }

    private void OnPress()
    {
        Vector2 screenPos = Mouse.current.position.ReadValue();
        Ray ray = cam.ScreenPointToRay(screenPos);

        if (Physics.Raycast(ray, out RaycastHit hitInfo, 20f, LayerMask.GetMask("Interactable", "Outline")))
        {
            if (hitInfo.rigidbody == rb)
            {
                dragging = true;
                dragDistance = hitInfo.distance;
                rb.useGravity = false;
            }
        }
    }

    private void Update()
    {
        if (draggingDisabled)
        {
            if (dragging) OnRelease();
            return;
        }

        if (Mouse.current.leftButton.wasPressedThisFrame) OnPress();
        if (Mouse.current.leftButton.wasReleasedThisFrame) OnRelease();
    }

    private void FixedUpdate()
    {
        if (!dragging) return;

        Vector2 screenPos = Mouse.current.position.ReadValue();
        Ray ray = cam.ScreenPointToRay(screenPos);

        Vector3 targetPos = ray.GetPoint(dragDistance);
        rb.MovePosition(targetPos);
    }
}
