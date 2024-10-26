using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent (typeof(PlayerInput))]
public class PlayerMovement : MonoBehaviour
{
    private bool canJump;
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

    private void Jump()
    {
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
        Vector2 moveDirection = movementSpeedMultiplier * PlayerInput.Instance.GetMoveDirection();

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

    //public void KnockBack(Vector3 dir, float duration)
    //{

    //}

    public IEnumerator KnockBack(Vector3 targetPos,float duration)
    {
        float timeElapsed = 0;

        while (timeElapsed < duration)
        {
            transform.position = Vector3.Lerp(transform.position, targetPos, timeElapsed / duration);
            yield return null;
        }
    }   

    private void OnEnable()
    {
        PlayerInput.Instance.OnJumped += Jump;
    }

    private void OnDisable()
    {
        PlayerInput.Instance.OnJumped -= Jump;
    }


}
