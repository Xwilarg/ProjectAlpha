using System.Collections.Generic;
using System.Linq;
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

    public bool AreUsersReady()
        => users.Count != 0 && !users.Any(x => x.GetGameplayClass() == User.GameplayClass.NotSelected);
}
