using System;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour
{

    private Rigidbody rb;
    [SerializeField] private float movementSpeedMultiplier;
    [SerializeField] private float jumpSpeedMultiplier;
    [SerializeField] private float fallMultiplier;

    [SerializeField] private float rayLength = 0.5f;
    [SerializeField] private LayerMask groundLayer;



    private void Awake()
    {


        rb = GetComponent<Rigidbody>();

    }

    private void Start()
    {
        PlayerInput.Instance.characterController.CharacterMovement.Jump.performed += Jump;
    }

    private void Jump(InputAction.CallbackContext context)
    {
        if (IsGrounded())
        {
            rb.AddForce(jumpSpeedMultiplier * Vector3.up, ForceMode.Impulse);
        }
    }


    private bool IsGrounded()
    {
        Ray ray = new(transform.position, Vector3.down);
        return Physics.Raycast(ray, rayLength, groundLayer);
    }


    void Move()
    {
        Vector2 moveDirection = movementSpeedMultiplier * PlayerInput.Instance.characterController.CharacterMovement.Movement.ReadValue<Vector2>().normalized;

        rb.linearVelocity = new Vector3(moveDirection.x, rb.linearVelocity.y, moveDirection.y);
    }

    private void FixedUpdate()
    {
        Move();

        if (rb.linearVelocity.y < 0)
        {
            rb.linearVelocity += Vector3.up * Physics.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
        }
    }


}
