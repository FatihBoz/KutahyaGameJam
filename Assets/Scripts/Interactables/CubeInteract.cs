using UnityEngine;

public class CubeInteract : MonoBehaviour, IInteractable
{
    private bool inInteracting;
    private Transform target;

     [SerializeField] private float moveSpeed = 5f;    // Movement speed in units per second
    [SerializeField] private float smoothTime = 0.1f; // Smoothing time for movement
    private Vector3 currentVelocity;                 // Reference velocity for SmoothDamp
    
    Vector3 targetPos;
    private void Awake() {
        targetPos=transform.position;
        target=null;
    }
    public void Interact(PlayerInteract playerInteract)
    {
        if (inInteracting)
        {
            inInteracting=false;
            target=null;
            playerInteract.SetIsHoldingObject(false);
            return;
        }
        
        // limit the player movement
        playerInteract.SetIsHoldingObject(true);
        inInteracting=true;
        target=playerInteract.GetInteractPoint();
    }

    public void Update()
    {

        if (inInteracting && target!=null )
        {
            targetPos = target.position + 2 * target.forward;
            targetPos.y=transform.position.y;
            transform.position = Vector3.SmoothDamp(
            transform.position,
            targetPos,
            ref currentVelocity,
            smoothTime,
            moveSpeed
        );
        }
    }

    public Vector3 GetPosition()
    {
        return transform.position;
    }

    public void RotateObject(float angle)
    {
        float pastRotateX=transform.rotation.x;
        float pastRotateZ=transform.rotation.z;
        transform.Rotate(new Vector3(0,angle,0));
        transform.rotation = Quaternion.Euler(pastRotateX,transform.rotation.y,pastRotateZ);
    }
}
