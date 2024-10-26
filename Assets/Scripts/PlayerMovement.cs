using System;
using Unity.Mathematics;
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
    [SerializeField] private float gravityScale;


[SerializeField]
    private Transform groundCheckTransform;
[SerializeField]
    private float groundCheckRadius;



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
        return Physics.CheckSphere(groundCheckTransform.position,groundCheckRadius,groundLayer);
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

        Vector3 movement = new Vector3(moveDirection.x, rb.linearVelocity.y, moveDirection.y);
        if (movement.x!=0 || movement.z!=0)
        {
            //transform.forward = new Vector3(movement.x, 0, movement.z); // Hareket y�n�ne g�re rotasyon ayarla
            Quaternion targetRotation = Quaternion.LookRotation(new Vector3(movement.x, 0, movement.z));
            transform.rotation = Quaternion.Euler(-90, targetRotation.eulerAngles.y, targetRotation.eulerAngles.z);
        }
        rb.linearVelocity = movement;
    }

    private void FixedUpdate()
    {
        Vector3 gravity = Physics.gravity * gravityScale;
        rb.AddForce(gravity,ForceMode.Acceleration);
        Move();

        if (rb.linearVelocity.y < 0)
        {
            rb.linearVelocity += (fallMultiplier - 1) * Physics.gravity.y * Time.deltaTime * Vector3.up;
        }
    }


    private void Start()
    {
        transform.rotation = Quaternion.Euler(-90, 0, 0);
        PlayerInput.Instance.OnJumped += Jump;
    }

    private void OnDestroy()
    {
        PlayerInput.Instance.OnJumped -= Jump;
    }

    /// <summary>
    /// Callback to draw gizmos that are pickable and always drawn.
    /// </summary>
    void OnDrawGizmos()
    {
        Gizmos.DrawSphere(groundCheckTransform.position,groundCheckRadius);
    }

}
