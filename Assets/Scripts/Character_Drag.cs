using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class Character_Drag : Character_Base
{
    private Vector3 offset;

    protected override void OnDragStart(Vector3 mouseWorld)
    {
        offset = transform.parent.position - mouseWorld;
    }

    protected override void OnDragging(Vector3 mouseWorld)
    {
        transform.parent.position = mouseWorld + offset;
    }
}
