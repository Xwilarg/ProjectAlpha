using System.Collections.Generic;
using UnityEngine;

public class ControllerManager : MonoBehaviour
{
    private List<User> users;

    public List<User> GetUsers()
        => users;

    public void AddUser(User u)
        => users.Add(u);

    private void Start()
    {
        DontDestroyOnLoad(gameObject);
        users = new List<User>();
    }
}
