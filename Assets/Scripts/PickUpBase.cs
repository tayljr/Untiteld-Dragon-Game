using UnityEngine;

public class PickUpBase : MonoBehaviour
{
    public delegate void PickUp(PickUpBase obj);
    public event PickUp PickUpEvent;

    protected void PickUpItem(PickUpBase obj)
    {
        PickUpEvent?.Invoke(obj);
    }
}
