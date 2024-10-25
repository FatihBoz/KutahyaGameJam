using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInteract : MonoBehaviour
{
    [SerializeField]
    private LayerMask mask;
    

    private bool limitLeftRightMovement;
    void Start()
    {
        PlayerInput.Instance.GetPlayerController().CharacterMovement.Interact.performed += OnInteractPerformed;       
    }
    void OnInteractPerformed(InputAction.CallbackContext context)
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position,transform.forward,out hit,Mathf.Infinity,mask))
        {

            if (hit.collider.TryGetComponent(out IInteractable interactable))
            {
                 Debug.Log("interactildi");

                interactable.Interact(this);   
            }
        }
    }

    public bool GetIsMovementLimited()
    {
        return limitLeftRightMovement;
    }
    public void SetIsMovementLimited(bool isLimited)
    {
        limitLeftRightMovement=isLimited;
    }

}
