using UnityEngine;

public class ObjectInteractable : Interactable
{
    [SerializeField] private float distanceFromCam;

    private bool inspecting = false;
    private Vector3 prePosition;
    private Quaternion preRotation;
    private Rigidbody rb;
    private CameraController cc;
    private PhotographDragger pd;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        cc = Camera.main.GetComponent<CameraController>();
        pd = GetComponent<PhotographDragger>();
    }

    public override void OnInteract()
    {
        if (!inspecting)
        {
            prePosition = transform.position;
            preRotation = transform.rotation;
            inspecting = true;
            cc.CanMove = false;
            rb.useGravity = false;
            if (pd != null) pd.draggingDisabled = true;
            return;
        }

        rb.Move(prePosition, preRotation);
        inspecting = false;
        cc.CanMove = true;
        rb.useGravity = true;
        if (pd != null) pd.draggingDisabled = false;
    }

    private void FixedUpdate()
    {
        if (!inspecting) return;

        Vector3 targetPos = Camera.main.transform.position + Camera.main.transform.forward * distanceFromCam;

        rb.MovePosition(Vector3.Lerp(rb.position, targetPos, Time.fixedDeltaTime * 10f));
        rb.MoveRotation(Quaternion.LookRotation(Camera.main.transform.forward));
    }
}
