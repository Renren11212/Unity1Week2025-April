using UnityEngine;
using System;
using Cysharp.Threading.Tasks;

public class UI_Logo : MonoBehaviour
{
    [SerializeField, Header("Iの文字")]
    private GameObject iChara;

    private Rigidbody2D rb;

    private void Start()
    {
        rb = iChara.GetComponent<Rigidbody2D>();
        if (rb == null) 
        {
            Debug.LogError("UI_Logo : I文字のRigidBody2Dが見つかりません");
        }

        // 開始まで待機
        WaitForStart().Forget();
    }

    /// <summary>
    /// RbがKinematicじゃなくなったら開始判定
    /// </summary>
    private async UniTaskVoid WaitForStart()
    {
        await UniTask.Delay(TimeSpan.FromSeconds(1f));
        
        while (rb.bodyType == RigidbodyType2D.Kinematic)
        {
            await UniTask.Yield(PlayerLoopTiming.Update);
        }

        this.gameObject.SetActive(false);
    }
}
