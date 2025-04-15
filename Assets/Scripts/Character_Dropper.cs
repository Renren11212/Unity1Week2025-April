using UnityEngine;
using System;
using Cysharp.Threading.Tasks;

public class Character_Dropper : MonoBehaviour
{
    [SerializeField]
    private Character_Base[] initiators;

    [SerializeField, Header("空中待機時間")]
    private float floatingTime = 2f;

    private Rigidbody2D parentRB;

    private void Start()
    {
        parentRB = GetComponentInParent<Rigidbody2D>();
        if (parentRB == null)
        {
            Debug.LogError("Character_Dropper: 親のRigidbody2Dが見つかりません");
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

    }

    private void HandleDragEnded()
    {

    }

    private async UniTaskVoid DelaySetDynamicAsync()
    {
        await UniTask.Delay(TimeSpan.FromSeconds(floatingTime));

        parentRB.bodyType = RigidbodyType2D.Dynamic;
    }
}
