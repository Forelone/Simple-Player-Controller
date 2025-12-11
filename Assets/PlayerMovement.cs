using UnityEngine;

[RequireComponent(typeof(PlayerInput))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float KMH = 1, AccelerationMul;
    [SerializeField] PlayerInput PInput;
    [SerializeField] Rigidbody RG;

    void FixedUpdate()
    {
        Vector3 PMove = PInput.DesiredMovement;

        Vector3 O = Vector3.Lerp(RG.velocity, PMove * KMH, Time.fixedDeltaTime * AccelerationMul);
        RG.velocity = O;
    }
}
