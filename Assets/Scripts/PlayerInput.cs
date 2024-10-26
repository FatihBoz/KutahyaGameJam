using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInput : MonoBehaviour
{
    public static PlayerInput Instance;
    public Action OnJumped;
    public Action OnInteracted;
    private PlayerController characterController;
    
    private void Awake()
    {
        characterController = new PlayerController();
        characterController.CharacterMovement.Enable();

        Instance = this;
    }

    private void Start()
    {
        characterController.CharacterMovement.Jump.performed += Jump;
        characterController.CharacterMovement.Interact.performed += Interact;
    }

    private void Interact(InputAction.CallbackContext context)
    {
        OnInteracted?.Invoke();
    }
    private void Jump(InputAction.CallbackContext context)
    {
        OnJumped?.Invoke();
        print("z�pla");
    }
    public Vector2 GetMoveDirection()
    {
        return characterController.CharacterMovement.Movement.ReadValue<Vector2>().normalized;
    }
}
