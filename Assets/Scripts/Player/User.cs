using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class User
{
    public static readonly int button0KeyCode = (int)KeyCode.Joystick1Button0;
    private static readonly int nextJoystick = 20; // That means that if you add 20 to button0KeyCode you get the button 0 of the joystick 2

    public static Dictionary<string, Key> keybind = new Dictionary<string, Key>()
    {
        { "left",   new Key{ keyboard = KeyCode.A, keycontroller = (KeyCode)(button0KeyCode + 14) } },
        { "right",  new Key{ keyboard = KeyCode.D, keycontroller = (KeyCode)(button0KeyCode + 15) } },
        { "up",     new Key{ keyboard = KeyCode.W, keycontroller = (KeyCode)(button0KeyCode + 12) } },
        { "down",   new Key{ keyboard = KeyCode.S, keycontroller = (KeyCode)(button0KeyCode + 13) } }
    };

    public struct Key
    {
        public KeyCode keyboard;
        public KeyCode keycontroller;
    }

    public User(int controller, Text controllerText)
    {
        _controller = controller;
        string name = _controller == -1 ? "Keyboard" : Input.GetJoystickNames()[_controller];
        controllerText.text = name;
    }

    public bool GetKeyDown(string key)
        => Input.GetKeyDown(GetKeyInternal(key));

    public bool GetKey(string key)
        => Input.GetKey(GetKeyInternal(key));

    private KeyCode GetKeyInternal(string key)
        => _controller == -1 ? keybind[key].keyboard : (KeyCode)((int)(keybind[key].keycontroller) + (nextJoystick * _controller));

    public Vector3 GetMovement()
    {
        float x = 0f;
        float y = 0f;

        if (_controller > -1f)
        {
            x = Input.GetAxis("Joy" + _controller + "X1");
            y = Input.GetAxis("Joy" + _controller + "Y1");
        }
        if (GetKey("left"))
            x = -1f;
        else if (GetKey("right"))
            x = 1f;
        if (GetKey("up"))
            y = 1f;
        else if (GetKey("down"))
            y = -1f;

        return new Vector3(x, 0f, y);
    }

    public int GetControllerId()
        => _controller;

    private int _controller;
}
