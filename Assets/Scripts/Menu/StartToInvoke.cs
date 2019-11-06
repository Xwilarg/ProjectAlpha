using UnityEngine;
using UnityEngine.UI;

public class StartToInvoke : MonoBehaviour
{
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Joystick1Button7) ||
            Input.GetKeyDown(KeyCode.Joystick2Button7) ||
            Input.GetKeyDown(KeyCode.Joystick3Button7) ||
            Input.GetKeyDown(KeyCode.Joystick4Button7))
            GetComponent<Button>().onClick.Invoke();
    }
}
