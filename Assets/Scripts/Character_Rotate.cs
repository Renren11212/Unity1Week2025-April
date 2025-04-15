using UnityEngine;
using Cysharp.Threading.Tasks;
using System;

[RequireComponent(typeof(Collider2D))]
public class Character_Rotate : Character_Base
{
    // クリック開始位置
    private Vector3 startPos;

    [SerializeField, Header("1周させる時のドラッグ距離")]
    private float maxDistance;

    private Rigidbody2D parentRB;

    protected override void Initialize()
    {
        base.Initialize();

        parentRB = GetComponentInParent<Rigidbody2D>();

        if (parentRB == null)
        {
            Debug.LogError("Character_Rotate : 親のRigidbody2Dが見つかりません");
            return;
        }
    }

    protected override void OnDragStart(Vector3 mouseWorld)
    {
        parentRB.bodyType = RigidbodyType2D.Kinematic;
        startPos = mouseWorld;
    }

    protected override void OnDragging(Vector3 mouseWorld)
    {
        // 現在のドラッグ距離
        float distance = startPos.x - mouseWorld.x;
        
        // 距離を回転に変換
        float rotate = distance / maxDistance * 360f;

        // z軸に対して回転        
        transform.parent.rotation = Quaternion.Euler(0, 0, rotate);
    }

    protected override void OnDragEnd(Vector3 mouseWorld)
    {
        parentRB.bodyType = RigidbodyType2D.Dynamic;
    }
}
