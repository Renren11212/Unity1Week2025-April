using System;
using UnityEngine;

public class UI_StartButton : UI_Button
{
    // スタートイベント
    private event Action OnStartClicked;
    public void AddStartClickedListener(Action listener) => OnStartClicked += listener;

    protected override void OnClick()
    {
        OnStartClicked?.Invoke();
    }
}
