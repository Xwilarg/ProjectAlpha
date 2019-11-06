using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class User
{
    public static readonly int button0KeyCode = (int)KeyCode.Joystick1Button0;
    public static readonly int nextJoystick = 20; // That means that if you add 20 to button0KeyCode you get the button 0 of the joystick 2

    public static Dictionary<string, Key> keybind = new Dictionary<string, Key>()
    {
        { "left",   new Key(new[]{ KeyCode.A, KeyCode.LeftArrow }, null, null) },
        { "right",  new Key(new[]{ KeyCode.D, KeyCode.RightArrow }, null, null) },
        { "up",     new Key(new[]{ KeyCode.W, KeyCode.UpArrow }, null, null) },
        { "down",   new Key(new[]{ KeyCode.S, KeyCode.DownArrow }, null, null) }
    };

    public class Key
    {
        public Key(KeyCode[] keyboard, KeyCode[] keyController, int[] mouseButton)
        {
            _keyboard = keyboard ?? new KeyCode[] { };
            _keycontroller = keyController ?? new KeyCode[] { };
            _mouseButton = mouseButton ?? new int[] { };
        }

        public bool IsPressed(int controllerId)
        {
            if (controllerId == -1)
            {
                foreach (var key in _keyboard)
                    if (Input.GetKey(key))
                        return true;
                foreach (var mouse in _mouseButton)
                    if (Input.GetMouseButton(mouse))
                        return true;
            }
            else
            {
                foreach (var key in _keycontroller)
                    if (Input.GetKey((KeyCode)((int)key + (nextJoystick * controllerId))))
                        return true;
            }
            return false;
        }

        private KeyCode[] _keyboard;
        private KeyCode[] _keycontroller;
        private int[] _mouseButton;
    }

    public User(int controller, Text controllerText)
    {
        _controller = controller;
        string name = _controller == -1 ? "Keyboard" : Input.GetJoystickNames()[_controller];
        controllerText.text = name;
    }

    public bool GetKey(string key)
        => keybind[key].IsPressed(_controller);

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
