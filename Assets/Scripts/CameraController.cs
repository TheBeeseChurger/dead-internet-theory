using UnityEngine;
using UnityEngine.InputSystem;

public class CameraController : MonoBehaviour
{
    public bool CanMove
    {
        get { return _canMove; }
        set
        {
            _canMove = value;
            if (_canMove) ResetValues();
        }
    }

    private bool _canMove = true;

    [Header("Movement")]
    [SerializeField] private float edgeThreshold = 40f;
    [SerializeField] private float panSpeed = 50f;
    [SerializeField] private float tiltSpeed = 50f;
    [SerializeField] private float damping = 8f;

    [Header("Limit Vectors")]
    [SerializeField] private Vector2 yawLimit = new(-30f, 30f); // X is Min, Y is Max
    [SerializeField] private Vector2 pitchLimit = new(30f, 50f); // X is Min, Y is Max

    private float yaw;
    private float pitch;
    private Vector2 velocity;

    void Start()
    {
        ResetValues();
    }

    private void ResetValues()
    {
        Vector3 rot = transform.localEulerAngles;
        yaw = rot.y;
        pitch = rot.x;
    }

    void Update()
    {
        if (!_canMove) return;

        Vector2 input = GetEdgeInput();

        velocity += new Vector2(input.x * panSpeed * Time.deltaTime, input.y * tiltSpeed * Time.deltaTime);

        float smooth = 1f - Mathf.Exp(-damping * Time.deltaTime);
        velocity = Vector2.Lerp(velocity, Vector2.zero, smooth);

        yaw += velocity.x;
        pitch -= velocity.y;

        yaw = Mathf.Clamp(yaw, yawLimit.x, yawLimit.y);
        pitch = Mathf.Clamp(pitch, pitchLimit.x, pitchLimit.y);

        transform.localRotation = Quaternion.Euler(pitch, yaw, 0f);
    }

    private Vector2 GetEdgeInput()
    {
        Vector2 input = Vector2.zero;

        if (Mouse.current == null) return input;

        Vector3 mouse = Mouse.current.position.ReadValue();

        if (mouse.x < edgeThreshold)
            input.x = -1f;
        else if (mouse.x > Screen.width - edgeThreshold)
            input.x = 1f;

        if (mouse.y < edgeThreshold)
            input.y = -1f;
        else if (mouse.y > Screen.height - edgeThreshold)
            input.y = 1f;

        return input;
    }
}
