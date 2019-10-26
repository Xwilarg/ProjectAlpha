using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class ControllerDetection : MonoBehaviour
{
    [SerializeField]
    private Text[] controllerMenu;

    [SerializeField]
    private Button startButton;

    private List<User> users;

    private void Start()
    {
        DontDestroyOnLoad(gameObject);
        users = new List<User>();
    }

    private void Update()
    {
        if (Input.anyKeyDown)
        {
            if (Input.GetKeyDown("enter"))
            {
                if (!users.Any(x => x.GetControllerId() == -1))
                    users.Add(new User(-1, controllerMenu[users.Count]));
            }
            else
            {
                for (int i = 0; i < 4; i++) // TODO; Must check what controller is used
                {
                    if (Input.GetKeyDown("joystick button 0"))
                    {
                        if (!users.Any(x => x.GetControllerId() == i))
                            users.Add(new User(i, controllerMenu[users.Count]));
                        break;
                    }
                }
            }
            if (users.Count > 0)
                startButton.interactable = true;
        }
    }

    public bool GetKeyDown(string key, int id)
        => users[id].GetKeyDown(key);

    private struct User
    {
        public User(int controller, Text controllerText)
        {
            _controller = controller;
            string name = _controller == -1 ? "Keyboard" : Input.GetJoystickNames()[_controller];
            controllerText.text = name;
        }

        public bool GetKeyDown(string key)
            => Input.GetKeyDown(key);

        public int GetControllerId()
            => _controller;

        private int _controller;
    }
}
