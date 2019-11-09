using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class StartToInvoke : MonoBehaviour
{
    private Button b;

    private void Start()
    {
        b = GetComponent<Button>();
    }

    private void Update()
    {
        if (b.interactable && (Input.GetKeyDown(KeyCode.Joystick1Button7) ||
            Input.GetKeyDown(KeyCode.Joystick2Button7) ||
            Input.GetKeyDown(KeyCode.Joystick3Button7) ||
            Input.GetKeyDown(KeyCode.Joystick4Button7)))
            b.onClick.Invoke();
    }
}
