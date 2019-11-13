using System;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(ControllerManager))]
public class ControllerDetection : MonoBehaviour
{
    private ControllerManager manager;

    [SerializeField]
    private PlayerSelection[] controllerMenu;

    [SerializeField]
    private Button startButton;

    [Serializable]
    private struct PlayerSelection
    {
        public Text title;
        public Text description;
    }

    private void Start()
    {
        manager = GetComponent<ControllerManager>();
    }

    private void Update()
    {
        if (Input.anyKeyDown)
        {
            if (Input.GetKeyDown("enter"))
            {
                if (!manager.GetUsers().Any(x => x.GetControllerId() == -1))
                    manager.AddUser(new User(-1, controllerMenu[manager.GetUsers().Count].title));
            }
            else
            {
                for (int i = 0; i < 4; i++)
                {
                    if (Input.GetKeyDown((KeyCode)(User.button0KeyCode + (User.nextJoystick * i))))
                    {
                        if (!manager.GetUsers().Any(x => x.GetControllerId() == i))
                            manager.AddUser(new User(i, controllerMenu[manager.GetUsers().Count].title));
                        break;
                    }
                }
            }
            if (manager.GetUsers().Count > 0)
                startButton.interactable = true;
        }
    }
}
