using System;
using UnityEngine;


[RequireComponent(typeof(Rigidbody))]
[RequireComponent (typeof(PlayerInput))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float movementSpeedMultiplier;
    [SerializeField] private float jumpSpeedMultiplier;
    [SerializeField] private float fallMultiplier;

    [SerializeField] private float rayLength = 0.5f;
    [SerializeField] private LayerMask groundLayer;

    private PlayerInteract playerInteract;
    private Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        playerInteract= GetComponent<PlayerInteract>();
    }

    private void Jump()
    {
        print(CheckGround());
        if(CheckGround())
        {
            rb.AddForce(jumpSpeedMultiplier * Vector3.up, ForceMode.Impulse);
        }
    }


    private bool CheckGround()
    {
        Ray ray = new(transform.position, Vector3.down);
        return Physics.Raycast(ray, rayLength, groundLayer);

    }


    void Move()
    {
        Vector2 inputMoveDirection = PlayerInput.Instance.GetMoveDirection();
        if (playerInteract.GetIsMovementLimited())
        {
            inputMoveDirection.x=0;
        }

        Vector2 moveDirection = movementSpeedMultiplier * inputMoveDirection;

        rb.linearVelocity = new Vector3(moveDirection.x, rb.linearVelocity.y, moveDirection.y);
    }

    private void FixedUpdate()
    {
        Move();

        if (rb.linearVelocity.y < 0)
        {
            rb.linearVelocity += (fallMultiplier - 1) * Physics.gravity.y * Time.deltaTime * Vector3.up;
        }
    }


    private void Start()
    {
        PlayerInput.Instance.OnJumped += Jump;
    }

    private void OnDestroy()
    {
        PlayerInput.Instance.OnJumped -= Jump;
    }


}
