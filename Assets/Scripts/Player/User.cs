using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class User
{
    public static readonly int button0KeyCode = (int)KeyCode.Joystick1Button0;
    public static readonly int nextJoystick = 20; // That means that if you add 20 to button0KeyCode you get the button 0 of the joystick 2

    public static Dictionary<string, Key> keybind = new Dictionary<string, Key>()
    {
        { "left",       new Key(new[]{ KeyCode.A, KeyCode.LeftArrow },  null, null) },
        { "right",      new Key(new[]{ KeyCode.D, KeyCode.RightArrow }, null, null) },
        { "up",         new Key(new[]{ KeyCode.W, KeyCode.UpArrow },    null, null) },
        { "down",       new Key(new[]{ KeyCode.S, KeyCode.DownArrow },  null, null) },
        { "fireMain",   new Key(null, new KeyCode[]{ KeyCode.Joystick1Button5 }, new int[]{ 0 }) }
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
            if (controllerId == -1) // Is keyboard
            {
                foreach (var key in _keyboard)
                    if (Input.GetKey(key))
                        return true;
                foreach (var mouse in _mouseButton)
                    if (Input.GetMouseButton(mouse))
                        return true;
            }
            else // Is controller
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

    public enum GameplayClass
    {
        NotSelected = -1,
        Rifleman,
        Grenadier,
        Brawler,
        Conjuror
    }

    public User(int controller, Text controllerText)
    {
        _controller = controller;
        lastRotX = 0f;
        lastRotY = 0f;
        myClass = GameplayClass.NotSelected;
        if (controller != -2)
        {
            string name = _controller == -1 ? "Keyboard" : Input.GetJoystickNames()[_controller];
            controllerName = name;
            controllerText.text = name;
        }
        else
            controllerName = "AI";
    }

    public bool GetKey(string key)
        => keybind[key].IsPressed(_controller);

    public Quaternion GetRotation(Vector3 playerPos)
    {
        float x, y;
        if (_controller > -1f)
        {
            x = Input.GetAxis("Joy" + _controller + "X2");
            y = Input.GetAxis("Joy" + _controller + "Y2");
            if (x == 0 && y == 0)
            {
                x = lastRotX;
                y = lastRotY;
            }
            else
            {
                lastRotX = x;
                lastRotY = y;
            }
        }
        else
        {
            Vector3 mousePos = Input.mousePosition;
            Vector3 pos = Camera.main.WorldToScreenPoint(playerPos);
            x = mousePos.x - pos.x;
            y = mousePos.y - pos.y;
        }
        var angle = Mathf.Atan2(y, x) * Mathf.Rad2Deg - 90f;
        return Quaternion.Euler(0f, -angle, 0f);
    }

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

    public string GetControllerName()
        => controllerName;

    public int GetControllerId()
        => _controller;

    public void SetGameplayClass(GameplayClass value)
        => myClass = value;

    public GameplayClass GetGameplayClass()
        => myClass;

    public void Init(AWeapon w, Character c)
    {
        weapon = w;
        charac = c;
    }

    public AWeapon GetWeapon()
        => weapon;

    public Character GetCharacter()
        => charac;

    /// -2: None (for bots)
    /// -1: Keyboard
    /// > -1: Controller id
    private int _controller;
    private float lastRotX, lastRotY;
    private GameplayClass myClass;
    private string controllerName;
    private AWeapon weapon;
    private Character charac;
}
