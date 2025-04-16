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

    protected override void OnDragStart(Vector3 mouseWorld)
    {
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
}
