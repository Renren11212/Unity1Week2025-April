using System;
using UnityEngine;

public class Character_Spring : Character_Base
{
    private Vector3 defaultCharacterSize = new Vector3(0.03f, 0.03f, 0);

    [SerializeField, Header("バネ伸縮率(%)")]
    private ScallingRange scallingRange = new()
    {
        maxScale = 110f,
        minScale = 50f
    };

    //[SerializeField, Header("基準ドラッグ距離")]
    //private float maxDistance = 20f;

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
    }

    protected override void OnDragging(Vector3 mouseWorld)
    {
        float distance = startPos.y - mouseWorld.y;

        //float scallingRate;
    }

    protected override void OnDragEnd(Vector3 mouseWorld)
    {
        
    }
}
