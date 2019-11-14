using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class PlayerManager : MonoBehaviour
{
    [SerializeField]
    private Transform[] spawns;

    [SerializeField]
    private GameObject playerPrefab;

    [SerializeField]
    private Material[] materials;

    [SerializeField]
    private Text summaryText;

    private ControllerManager controller;

    private List<Transform> enemies;
    public void AddEnemy(Transform t) => enemies.Add(t);
    public void DeleteEnemy(Transform t) => enemies.Remove(t);
    public IReadOnlyCollection<Transform> GetEnemies() => enemies.AsReadOnly();

    private List<User> users;

    private void Start()
    {
        enemies = new List<Transform>();
        controller = GameObject.Find("IntroController").GetComponent<ControllerManager>();
        users = controller.GetUsers();
        GameObject.Find("RpcManager")?.GetComponent<RpcManager>().StartGame(users.Count);
        Transform[] humans = new Transform[users.Count];
        int i = 0;

        // Instantiate all humans
        foreach (var user in users)
        {
            GameObject go = SpawnPlayer(i);
            go.AddComponent<PlayerHuman>().SetUser(controller.GetUsers()[i]);
            go.GetComponent<NavMeshAgent>().enabled = false;
            go.tag = "PlayerHuman";
            go.name = "Player " + i + " - Human";
            humans[i] = go.transform;
            i++;
        }

        // Get all classes that aren't choosed yet
        List<User.GameplayClass> remaningClasses = new List<User.GameplayClass>();
        for (int y = 0; y <= (int)Enum.GetValues(typeof(User.GameplayClass)).Cast<User.GameplayClass>().Max(); y++)
        {
            if (controller.IsClassAvailable((User.GameplayClass)y))
                remaningClasses.Add((User.GameplayClass)y);
        }

        // Instantiate all AIs
        for (; i < 4; i++)
        {
            GameObject go = SpawnPlayer(i);
            go.AddComponent<PlayerAI>().Init(humans);
            go.GetComponent<Rigidbody>().isKinematic = true;
            go.tag = "PlayerAI";
            go.name = "Player " + i + " - AI";

            // Take a random class for the AI
            var aiUser = new User(-2, null);
            var randomClass = UnityEngine.Random.Range(0, remaningClasses.Count);
            aiUser.SetGameplayClass(remaningClasses[randomClass]);
            users.Add(aiUser);
            remaningClasses.RemoveAt(randomClass);
        }
    }

    private void Update()
    {
        int i = 0;
        StringBuilder sb = new StringBuilder();
        foreach (User u in users)
        {
            sb.AppendLine("<b>Player " + i + ": " + u.GetControllerName() + "</b>");
            sb.AppendLine("Character: " + u.GetGameplayClass().ToString());
            sb.AppendLine();
            i++;
        }
        summaryText.text = sb.ToString();
    }

    private GameObject SpawnPlayer(int index)
    {
        GameObject go = Instantiate(playerPrefab, spawns[index].transform.position, Quaternion.identity);
        go.GetComponent<Renderer>().material = materials[index];
        go.layer = 11 + index; // First "Player" layer
        return go;
    }
}
