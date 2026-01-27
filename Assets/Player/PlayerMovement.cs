using System.Collections;
using UnityEngine;

[RequireComponent(typeof(PlayerInput))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float WalkSpeed = 1, RunSpeed = 2;

    CharacterController CC;

    void Start()
    {
        PlayerInput playerInput = GetComponent<PlayerInput>();
        CC = GetComponent<CharacterController>();
        playerInput.OnMovement += HandleMovement;
    }

    public void HandleMovement(Vector3 NewMovement)
    {
        float Mag = NewMovement.magnitude;
        NewMovement = NewMovement.normalized;
        
        Vector3 GroundMovement = transform.forward * NewMovement.z + transform.right * NewMovement.x;
        GroundMovement *= Mag == 2f ? RunSpeed : WalkSpeed; 
        transform.Translate(GroundMovement * Time.fixedDeltaTime,Space.World);
    }
}
