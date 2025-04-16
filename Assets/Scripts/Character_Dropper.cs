using UnityEngine;
using System;
using Cysharp.Threading.Tasks;
using System.Threading;

public class Character_Dropper : MonoBehaviour
{
    [SerializeField]
    private Character_Base[] initiators;

    [SerializeField, Header("空中待機時間")]
    private float floatingTime = 2f;

    private Rigidbody2D rb;

    private CancellationTokenSource delayCts;

    private void Start()
    {
        rb = GetComponentInParent<Rigidbody2D>();
        if (rb == null)
        {
            Debug.LogError("Character_Dropper: Rigidbody2Dが見つかりません");
            return;
        }

        foreach (Character_Base initiator in initiators)
        {
            initiator.AddOnDragStartedListener(HandleDragStarted);
            initiator.AddOnDragEndedListener(HandleDragEnded);
        }
    }

    private void HandleDragStarted()
    {
        delayCts?.Cancel();
        rb.bodyType = RigidbodyType2D.Kinematic;
    }

    private void HandleDragEnded()
    {
        delayCts?.Cancel();
        DelaySetDynamicAsync().Forget();
    }

    private async UniTaskVoid DelaySetDynamicAsync()
    {
        delayCts?.Cancel();
        delayCts = new CancellationTokenSource();
        
        try
        {
            await UniTask.Delay(TimeSpan.FromSeconds(floatingTime), cancellationToken: delayCts.Token);
            rb.bodyType = RigidbodyType2D.Dynamic;
        }
        catch (OperationCanceledException)
        {
            // nothing
        }
    }
}
