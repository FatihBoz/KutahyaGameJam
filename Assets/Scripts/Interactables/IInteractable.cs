using UnityEngine;

public interface IInteractable
{
    public void Interact(PlayerInteract playerInteract);
    public Vector3 GetPosition();
    public void RotateObject(float angle);
    public void Reset();
}
