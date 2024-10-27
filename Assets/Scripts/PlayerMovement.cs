using System;
using Unity.Mathematics;
using UnityEngine;


[RequireComponent(typeof(Rigidbody))]
[RequireComponent (typeof(PlayerInput))]
public class PlayerMovement : MonoBehaviour
{
    
[SerializeField]
    private float rotateSpeedMultiplier=10f;
    [SerializeField] private float movementSpeedMultiplier;
    [SerializeField] private float jumpSpeedMultiplier;
    [SerializeField] private float fallMultiplier;

    [SerializeField] private float speedWhileCarryingMultiplier=0.5f;
    [SerializeField] private float rayLength = 0.5f;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private float gravityScale;


[SerializeField]
    private Transform groundCheckTransform;
[SerializeField]
    private float groundCheckRadius;


    [SerializeField]
    private Animator animator;

    private PlayerInteract playerInteract;
    private Rigidbody rb;


    private Camera camera;
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        playerInteract= GetComponent<PlayerInteract>();
    
    }
    private void Jump()
    {
        if(CheckGround() && !playerInteract.GetIsHoldingObject())
        {
            animator.SetTrigger("isJumping");
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
        if (playerInteract.GetIsHoldingObject())
        {
            float angle =inputMoveDirection.x*rotateSpeedMultiplier;
            transform.RotateAround(playerInteract.GetInteractedObjectPosition(),Vector3.up,angle);
            playerInteract.RotateInteractedObject(angle);
            rb.linearVelocity = inputMoveDirection.y*playerInteract.GetInteractPoint().forward * movementSpeedMultiplier*speedWhileCarryingMultiplier;
            if (inputMoveDirection.y!=0)
            {
                animator.SetBool("isMoving",true);
            }
            else
            {
                animator.SetBool("isMoving",false);
            }
        }
        else
        {
            
        animator.SetFloat("inputX",inputMoveDirection.x);
        animator.SetFloat("inputY",inputMoveDirection.y);

        Vector2 moveDirection = movementSpeedMultiplier * inputMoveDirection;
        
        Vector3 cameraForward= camera.transform.forward;
        Vector3 cameraRight= camera.transform.right;

        cameraForward.y=0;
        cameraRight.y=0;
        cameraForward.Normalize();
        cameraRight.Normalize();

        Vector3 fullMovement=cameraForward*moveDirection.y+cameraRight*moveDirection.x;    
        
        rb.linearVelocity = new Vector3(fullMovement.x,rb.linearVelocity.y,fullMovement.z);

        Vector3 movement = new Vector3(moveDirection.x, rb.linearVelocity.y, moveDirection.y);
        if (movement.x!=0 || movement.z!=0)
        {
            animator.SetBool("isMoving",true);
            //transform.forward = new Vector3(movement.x, 0, movement.z); // Hareket y�n�ne g�re rotasyon ayarla
            Quaternion targetRotation = Quaternion.LookRotation(fullMovement);
            transform.rotation = Quaternion.Euler(-90, targetRotation.eulerAngles.y, targetRotation.eulerAngles.z);
        }
        else{
            animator.SetBool("isMoving",false);
        }
       // rb.linearVelocity = movement;
        
        }
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
        camera=Camera.main;
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
