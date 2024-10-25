using UnityEngine;

public class CubeInteract : MonoBehaviour, IInteractable
{
    private bool isMoving;
    private Transform target;

     [SerializeField] private float moveSpeed = 5f;    // Movement speed in units per second
    [SerializeField] private float smoothTime = 0.1f; // Smoothing time for movement
    private Vector3 currentVelocity;                 // Reference velocity for SmoothDamp
    
    Vector3 targetPos;
    private void Awake() {
        targetPos  =transform.position;
        target=null;
    }
    public void Interact(PlayerInteract playerInteract)
    {
      
        // limit the player movement
        isMoving=true;
        target=playerInteract.transform;
    }

    public void Update()
    {

        if (isMoving && target!=null )
        {

            targetPos = target.position + 2 * transform.forward;
            transform.position = Vector3.SmoothDamp(
            transform.position,
            targetPos,
            ref currentVelocity,
            smoothTime,
            moveSpeed
        );
        }
    } 
}
