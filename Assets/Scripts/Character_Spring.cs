using System;
using UnityEngine;

public class Character_Spring : Character_Base
{
    private float maxHeight;
    private float minHeight;

    [SerializeField, Header("バネ伸縮率(倍)")]
    private ScallingRange scallingRange = new ScallingRange()
    {
        maxScale = 1.5f,
        minScale = 0.5f
    };

    // クリック開始位置
    private Vector3 startPos;

    [Serializable]
    public struct ScallingRange
    {
        public float maxScale;
        public float minScale;
    }

    protected override void Initialize()
    {
        base.Initialize();

        maxHeight = transform.parent.localScale.y * scallingRange.maxScale;

        minHeight = transform.parent.localScale.y * scallingRange.minScale;
    }

    protected override void OnDragStart(Vector3 mouseWorld)
    {
        startPos = mouseWorld;
    }

    protected override void OnDragging(Vector3 mouseWorld)
    {
        // 移動量
        float deltaY = startPos.y - mouseWorld.y;

        deltaY *= 0.0001f;
        StretchSprite(deltaY);
    }

    protected override void OnDragEnd(Vector3 mouseWorld)
    {
        
    }

    private void StretchSprite(float deltaY)
    {
        // 現在のサイズ
        Vector2 size = new Vector2(
            transform.parent.localScale.x,
            transform.parent.localScale.y
            );

        // 新しいサイズ（制限つき）
        float newHeight = Mathf.Clamp(size.y - deltaY, minHeight, maxHeight);

        // 高さの変化分
        float deltaHeight = newHeight - size.y;

        // サイズ更新
        transform.parent.localScale = new Vector2(size.x, newHeight);

        // 上にずらす
        //transform.parent.position += new Vector3(0f, deltaHeight * 0.5f);
    }
}
