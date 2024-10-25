using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInput : MonoBehaviour
{

    public PlayerController characterController;

    public static PlayerInput Instance { get; private set; }
    private void Awake()
    {
    

        characterController = new PlayerController();
        characterController.CharacterMovement.Enable();

    }


}
