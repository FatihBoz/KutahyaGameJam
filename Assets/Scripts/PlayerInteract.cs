using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent (typeof(PlayerInput))]
public class PlayerInteract : MonoBehaviour
{
    [SerializeField]
    private LayerMask mask;
    private bool limitLeftRightMovement;

    private IInteractable interactedObject;
    private bool isInteracting;
    
    private void Start() {
        PlayerInput.Instance.OnInteracted += OnInteractPerformed;
    }
    private void OnDisable() {
        PlayerInput.Instance.OnInteracted-=OnInteractPerformed;
        
    }
    public void OnInteractPerformed()
    {
        if (isInteracting && interactedObject!=null)
        {
            interactedObject.Interact(this);   
            isInteracting=false;
            interactedObject=null;
        }
        else
        {
            RaycastHit hit;
            if (Physics.Raycast(transform.position,transform.forward,out hit,Mathf.Infinity,mask))
            {

                if (hit.collider.TryGetComponent(out IInteractable interactable))
                {
                    interactable.Interact(this);   
                    interactedObject=interactable;
                    isInteracting=true;
                }
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
