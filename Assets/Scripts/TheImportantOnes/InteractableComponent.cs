using UnityEngine;

public abstract class InteractableComponent : MonoBehaviour
{
    //this function will be callled when wanting to move around the component, while you dont know what class it is exactly
    public abstract void CloneTo(GameObject target);
}
