using UnityEngine;
using UnityEngine.UI;

public class User
{
    public User(int controller, Text controllerText)
    {
        _controller = controller;
        string name = _controller == -1 ? "Keyboard" : Input.GetJoystickNames()[_controller];
        controllerText.text = name;
    }

    public int GetControllerId()
        => _controller;

    private int _controller;
}
