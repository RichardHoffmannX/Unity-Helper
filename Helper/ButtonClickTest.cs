#if ODIN_INSPECTOR
using Sirenix.OdinInspector; // Used for [Button]
#endif // ODIN_INSPECTOR
using UnityEngine;
using UnityEngine.UI;

public class ButtonClickTest : MonoBehaviour
{
    public Button button; // just in case 

    private void Start()
    {
        if (!button)
            button = gameObject.GetComponent<Button>();
    }

    [Button]
    private void OnClickText()
    {
        button.onClick.Invoke();
        Debug.Log("Button Clicked!");
    }
}
