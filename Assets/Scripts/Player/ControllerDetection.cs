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

    public List<User> GetUsers()
        => users;

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
                for (int i = 0; i < 4; i++)
                {
                    if (Input.GetKeyDown((KeyCode)(User.button0KeyCode + (User.nextJoystick * i))))
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
}
