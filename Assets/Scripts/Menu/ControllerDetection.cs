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
        public Button okButton;
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
                    int index = manager.GetUsers().Count;
                    var value = controllerMenu[index];
                    manager.AddUser(new User(-1, value.title));
                    value.buttons.SetActive(true);
                    value.description.text = ((User.GameplayClass)0).ToString();
                    CheckClassAvailability(index);
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
                            int index = manager.GetUsers().Count;
                            var value = controllerMenu[index];
                            manager.AddUser(new User(i, value.title));
                            value.buttons.SetActive(true);
                            value.description.text = ((User.GameplayClass)0).ToString();
                            CheckClassAvailability(index);
                        }
                        break;
                    }
                }
            }
        }
    }

    private void CheckClassAvailability(int index)
    {
        var value = controllerMenu[index];
        if (manager.IsClassAvailable(selectedClass[index]))
        {
            value.description.color = Color.black;
            value.okButton.interactable = true;
        }
        else
        {
            value.description.color = Color.grey;
            value.okButton.interactable = false;
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
        CheckClassAvailability(index);
    }

    public void ValidateClass(int index)
    {
        manager.GetUsers()[index].SetGameplayClass(selectedClass[index]);
        controllerMenu[index].buttons.SetActive(false);
        if (manager.AreUsersReady())
            startButton.interactable = true;
        else
        {
            for (int i = 0; i < manager.GetUsers().Count; i++)
            {
                if (manager.GetUsers()[i].GetGameplayClass() == User.GameplayClass.NotSelected)
                    CheckClassAvailability(index);
            }
        }
    }
}
