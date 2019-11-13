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
        public GameObject buttons;
    }

    private User.GameplayClass[] selectedClass;

    private void Start()
    {
        manager = GetComponent<ControllerManager>();
        selectedClass = new User.GameplayClass[] {
            User.GameplayClass.Rifleman,
            User.GameplayClass.Rifleman,
            User.GameplayClass.Rifleman,
            User.GameplayClass.Rifleman
        };
    }

    private void Update()
    {
        if (Input.anyKeyDown)
        {
            if (Input.GetKeyDown("enter"))
            {
                if (!manager.GetUsers().Any(x => x.GetControllerId() == -1))
                {
                    var value = controllerMenu[manager.GetUsers().Count];
                    manager.AddUser(new User(-1, value.title));
                    value.buttons.SetActive(true);
                    value.description.text = ((User.GameplayClass)0).ToString();
                }
            }
            else
            {
                for (int i = 0; i < 4; i++)
                {
                    if (Input.GetKeyDown((KeyCode)(User.button0KeyCode + (User.nextJoystick * i))))
                    {
                        if (!manager.GetUsers().Any(x => x.GetControllerId() == i))
                        {
                            var value = controllerMenu[manager.GetUsers().Count];
                            manager.AddUser(new User(i, value.title));
                            value.buttons.SetActive(true);
                            value.description.text = ((User.GameplayClass)0).ToString();
                        }
                        break;
                    }
                }
            }
        }
    }

    public void DisplayNextClass(int index)
    {
        var value = selectedClass[index];
        value++;
        if (value > Enum.GetValues(typeof(User.GameplayClass)).Cast<User.GameplayClass>().Max())
            value = 0;
        selectedClass[index] = value;
        controllerMenu[index].description.text = value.ToString();
    }

    public void ValidateClass(int index)
    {
        manager.GetUsers()[index].SetGameplayClass(selectedClass[index]);
        controllerMenu[index].buttons.SetActive(false);
        if (manager.AreUsersReady())
            startButton.interactable = true;
    }
}
