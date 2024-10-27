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
    private bool isHoldingObject;

    private IInteractable interactedObject;
    private bool isInteracting;
    
    private void Start() {
        PlayerInput.Instance.OnInteracted += OnInteractPerformed;
        Player.Instance.OnPlayerDied +=OnDied;
    }
    private void OnDestroy() {
        PlayerInput.Instance.OnInteracted-=OnInteractPerformed;
        Player.Instance.OnPlayerDied -=OnDied;
    }
    private void OnDied(float delay)
    {
        Reset();
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
                if (hit.collider.TryGetComponent(out IInteractable interactable))
                {
                    interactable.Interact(this);   
                    interactedObject=interactable;
                    isInteracting=true;
                }
            }
        }
    }
    public bool GetIsHoldingObject()
    {
        return isHoldingObject;
    }
    public void SetIsHoldingObject(bool isHoldingObject)
    {
        this.isHoldingObject=isHoldingObject;
    }

    /// <summary>
    /// Callback to draw gizmos only if the object is selected.
    /// </summary>
    void OnDrawGizmosSelected()
    {
        Gizmos.color=Color.red;
        Gizmos.DrawLine(interactPoint.position,interactPoint.position+interactPoint.forward*rayLength);
    }

    public Transform GetInteractPoint()
    {
        return interactPoint;
    }
    public Vector3 GetInteractedObjectPosition()
    {
        return interactedObject.GetPosition();
    }
    public void RotateInteractedObject(float angle)
    {
        interactedObject.RotateObject(angle);
    }
    public void Reset()
    {
                    isHoldingObject=false;
                   isInteracting=false;
                   if (interactedObject!=null)
                   {
                    interactedObject.Reset();
                    interactedObject=null;
                   }
    }

}
