using UnityEngine;
using UnityEngine.UI;

public class UI_Button : MonoBehaviour
{
    private Button button;
    private void Start()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(OnClick);
    } 

    protected virtual void OnClick() {}
}
