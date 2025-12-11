using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    [SerializeField] Vector3 Movement;
    public Vector3 DesiredMovement { get { return Movement; } }

    [SerializeField] Vector2 Rotation;
    public Vector2 DesiredRotation { get { return Rotation; } }
    [SerializeField] float VerticalMul = 1, HorizontalMul = 1;

    [SerializeField] float MinXRot = -80, MaxXRot = 80;

    [SerializeField] bool M1 = false, M2 = false, M3 = false;
    public bool PrimaryHandUse { get { return M1; } }
    public bool SecondaryHandUse { get { return M2; } }
    public bool InteractiveUse { get { return M3; } }

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    float VerRot = 0;

    void Update()
    {
        float h = Input.GetAxis("Horizontal"), v = Input.GetAxis("Vertical"), u = Input.GetAxisRaw("Jump");
        Movement = transform.forward * v + transform.right * h + Vector3.up * u;

        float X = Input.GetAxis("Mouse X") * HorizontalMul, Y = Input.GetAxis("Mouse Y") * VerticalMul;

        VerRot -= Y;
        VerRot = Mathf.Clamp(VerRot, MinXRot, MaxXRot);        
        
        Rotation = Vector2.up * X + Vector2.right * VerRot;

        M1 = Input.GetAxisRaw("Fire1") == 1;
        M2 = Input.GetAxisRaw("Fire2") == 1;
        M3 = Input.GetAxisRaw("Fire3") == 1;
    }
}
