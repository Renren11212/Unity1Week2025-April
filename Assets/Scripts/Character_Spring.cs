using System;
using UnityEngine;

public class Character_Spring : Character_Base
{
    private Rigidbody2D parentRB;

    private Vector3 defaultCharacterSize = new Vector3(0.03f, 0.03f, 0);

    [SerializeField, Header("バネ伸縮率(%)")]
    private ScallingRange scallingRange = new()
    {
        maxScale = 110f,
        minScale = 50f
    };

    [SerializeField, Header("基準ドラッグ距離")]
    private float maxDistance = 20f;

    // クリック開始位置
    private Vector3 startPos;

    protected override void Initialize()
    {
        base.Initialize();

        parentRB = GetComponentInParent<Rigidbody2D>();

        if (parentRB == null)
        {
            Debug.LogError("Character_Spring : 親のRigidbody2Dが見つかりません");
            return;
        }
    }

    [Serializable]
    public struct ScallingRange
    {
        public float maxScale;
        public float minScale;
    }

    protected override void OnDragStart(Vector3 mouseWorld)
    {
        startPos = mouseWorld;
        parentRB.bodyType = RigidbodyType2D.Kinematic;
    }

    protected override void OnDragging(Vector3 mouseWorld)
    {
        float distance = startPos.y - mouseWorld.y;

        float scallingRate;
    }

    protected override void OnDragEnd(Vector3 mouseWorld)
    {
        parentRB.bodyType = RigidbodyType2D.Dynamic;
    }
}
