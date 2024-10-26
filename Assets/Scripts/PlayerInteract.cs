using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent (typeof(PlayerInput))]
public class PlayerInteract : MonoBehaviour
{
    
    [SerializeField]
    private Transform interactPoint;
     [SerializeField]
    private float rayLength=20f;

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
            if (Physics.Raycast(interactPoint.position,interactPoint.forward,out hit,rayLength,mask))
            {
                Debug.Log("vurdum");
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

    /// <summary>
    /// Callback to draw gizmos only if the object is selected.
    /// </summary>
    void OnDrawGizmosSelected()
    {
        Gizmos.color=Color.red;
        Gizmos.DrawLine(interactPoint.position,interactPoint.position-interactPoint.forward*rayLength);
    }

}
