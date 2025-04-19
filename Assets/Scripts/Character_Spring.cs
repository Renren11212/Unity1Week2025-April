using System;
using UnityEngine;

public class Character_Spring : Character_Base
{
    private Vector3 defaultCharacterSize;

    [SerializeField, Header("バネ伸縮率(倍)")]
    private ScallingRange scallingRange = new()
    {
        maxScale = 1.5f,
        minScale = 0.5f
    };

    [SerializeField, Header("基準ドラッグ距離")]
    private float maxDistance = 20f;

    // クリック開始位置
    private Vector3 startPos;

    [Serializable]
    public struct ScallingRange
    {
        public float maxScale;
        public float minScale;
    }

    protected override void OnDragStart(Vector3 mouseWorld)
    {
        startPos = mouseWorld;
        defaultCharacterSize = transform.parent.localScale;
    }

    protected override void OnDragging(Vector3 mouseWorld)
    {
        float distance = startPos.y - mouseWorld.y;

        float scallingRate = (maxDistance - distance) / maxDistance;

        scallingRate = Mathf.Clamp(scallingRate, scallingRange.minScale, scallingRange.maxScale);

        transform.parent.localScale = new Vector3(
            defaultCharacterSize.x, 
            defaultCharacterSize.y * scallingRate,
            defaultCharacterSize.z
            );
    }

    protected override void OnDragEnd(Vector3 mouseWorld)
    {
        
    }
}
