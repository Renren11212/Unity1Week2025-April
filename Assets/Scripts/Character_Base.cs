using System;
using UnityEditor.Animations;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public abstract class Character_Base : MonoBehaviour
{
    private bool isDragging = false;
    private Camera mainCamera;

    private event Action OnDragStarted;
    public void AddOnDragStartedListener(Action listener) => OnDragStarted += listener;

    private event Action OnDragEnded;
    public void AddOnDragEndedListener(Action listener) => OnDragEnded += listener;

    /// <summary>
    /// 初期化メソッド
    /// </summary>
    protected virtual void Initialize()
    {
        mainCamera = Camera.main;
    }

    private void Start()
    {
        Initialize();
    }

    private void Update()
    {
        Vector3 mouseWorld = GetMouseWorldPosition();

        // ドラッグ開始
        if (Input.GetMouseButtonDown(0) && IsMouseOver(mouseWorld) && !isDragging)
        {
            isDragging = true;

            // ドラッグ開始イベント
            OnDragStarted?.Invoke();
            OnDragStart(mouseWorld);
        }

        // ドラッグ終了
        if (!Input.GetMouseButton(0) && isDragging)
        {
            isDragging = false;

            // ドラッグ終了イベント
            OnDragEnded?.Invoke();
            OnDragEnd(mouseWorld);
        }

        // ドラッグ中
        if (isDragging)
        {
            OnDragging(mouseWorld);
        }
    }

    /// <summary>
    /// アタッチされているオブジェクトに触れているかを返す
    /// </summary>
    private bool IsMouseOver(Vector3 mouseWorld)
    {
        // オブジェクトにアタッチされている全てのCollider2Dを取得
        Collider2D[] colliders = GetComponents<Collider2D>();

        // 現在マウスが重なっているコライダーを取得
        Collider2D hitCollider = Physics2D.OverlapPoint(mouseWorld);

        // どれか一致すればtrue
        foreach (var col in colliders)
        {
            if (col == hitCollider) return true;
        }

        return false;
    }

    /// <summary>
    /// マウスカーソルの世界座標を返す
    /// </summary>
    private Vector3 GetMouseWorldPosition()
    {
        Vector3 screenPos = Input.mousePosition;
        screenPos.z = -mainCamera.transform.position.z;
        return mainCamera.ScreenToWorldPoint(screenPos);
    }

    // フックメソッド（子クラスでオーバーライド）

    /// <summary>
    /// ドラッグ開始時処理
    /// </summary>
    protected virtual void OnDragStart(Vector3 mouseWorld) { }

    /// <summary>
    /// ドラッグ中処理
    /// </summary>
    protected virtual void OnDragging(Vector3 mouseWorld) { }

    /// <summary>
    /// ドラッグ終了処理
    /// </summary>
    protected virtual void OnDragEnd(Vector3 mouseWorld) { }
}