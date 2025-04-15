using UnityEngine;
using System;
using Cysharp.Threading.Tasks;

[RequireComponent(typeof(Collider2D))]
public class Character_Drag : Character_Base
{
    private Vector3 offset;

    private Rigidbody2D parentRB;

    [SerializeField, Header("空中待機時間")]
    private float floatingTime = 3f;

    protected override void Initialize()
    {
        base.Initialize();

        parentRB = GetComponentInParent<Rigidbody2D>();

        if (parentRB == null)
        {
            Debug.LogError("Character_Drag : 親のRigidbody2Dが見つかりません");
            return;
        }
    }

    protected override void OnDragStart(Vector3 mouseWorld)
    {
        offset = transform.parent.position - mouseWorld;
        parentRB.bodyType = RigidbodyType2D.Kinematic;
    }

    protected override void OnDragging(Vector3 mouseWorld)
    {
        transform.parent.position = mouseWorld + offset;
    }

    protected override void OnDragEnd(Vector3 mouseWorld)
    {
        DelaySetDynamicAsync().Forget();
    }

    private async UniTaskVoid DelaySetDynamicAsync()
    {
        await UniTask.Delay(TimeSpan.FromSeconds(floatingTime));

        parentRB.bodyType = RigidbodyType2D.Dynamic;
    }
}
