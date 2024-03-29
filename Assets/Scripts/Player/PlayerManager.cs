﻿using System;
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
        bool isDebug; // Enable if we don't init players with the character selection menu. If true, AIs won't be init automatically

        enemies = new List<Transform>();
        var controllerGo = GameObject.Find("IntroController");
        if (controllerGo == null)
        {
            var newControllerGo = new GameObject("IntroController", typeof(ControllerDetection), typeof(ControllerManager));
            controller = newControllerGo.GetComponent<ControllerManager>();
            users = new List<User>();
            User player = new User(-1, null);
            player.SetGameplayClass(User.GameplayClass.Rifleman);
            users.Add(player);
            isDebug = true;
        }
        else
        {
            controller = controllerGo.GetComponent<ControllerManager>();
            users = controller.GetUsers();
            isDebug = false;
        }
        GameObject.Find("RpcManager")?.GetComponent<RpcManager>().StartGame(users.Count);
        Transform[] humans = new Transform[users.Count];
        int i = 0;

        // Instantiate all humans
        foreach (var user in users)
        {
            GameObject go = SpawnPlayer(i);
            go.GetComponent<NavMeshAgent>().enabled = false;
            go.AddComponent<PlayerHuman>();
            go.tag = "PlayerHuman";
            go.name = "Player " + i + " - Human";
            humans[i] = go.transform;
            i++;
        }

        // Get all classes that aren't choosed yet
        List<User.GameplayClass> remaningClasses = new List<User.GameplayClass>();
        for (int y = 0; y <= (int)Enum.GetValues(typeof(User.GameplayClass)).Cast<User.GameplayClass>().Max(); y++)
        {
            if (isDebug || controller.IsClassAvailable((User.GameplayClass)y)) // If class is available. If debug mode enabled, we don't do the check
                remaningClasses.Add((User.GameplayClass)y);
        }

        // Instantiate all AIs
        if (!isDebug) // IAs are instantiated only if debug mode is disabled. Else they must be instantiated manually
        {
            for (; i < 4; i++)
            {
                // Take a random class for the AI
                var aiUser = new User(-2, null);
                var randomClass = UnityEngine.Random.Range(0, remaningClasses.Count);
                aiUser.SetGameplayClass(remaningClasses[randomClass]);
                users.Add(aiUser);
                remaningClasses.RemoveAt(randomClass);

                GameObject go = SpawnPlayer(i);
                go.AddComponent<PlayerAI>().Init(humans);
                go.GetComponent<Rigidbody>().isKinematic = true;
                go.tag = "PlayerAI";
                go.name = "Player " + i + " - AI";
            }
        }

        // Debug scenes
        GetComponent<DebugInitEnemies>()?.InitEnemies(humans);
    }

    private void Update()
    {
        int i = 0;
        StringBuilder sb = new StringBuilder();
        foreach (User u in users)
        {
            sb.AppendLine("<b>Player " + i + ": " + u.GetControllerName() + "</b>");
            sb.AppendLine("Character: " + u.GetGameplayClass().ToString());
            sb.AppendLine("HP: " + u.GetCharacter().GetHP());
            sb.AppendLine("Main Fire: " + (u.GetWeapon().CanShoot() ? "Ready" : u.GetWeapon().GetRemainingReloadTime() + "s"));
            sb.AppendLine("Enemies killed: " + u.GetPlayerStats().EnemyKilled);
            sb.AppendLine();
            i++;
        }
        summaryText.text = sb.ToString();
    }

    private GameObject SpawnPlayer(int index)
    {
        GameObject go = Instantiate(playerPrefab, spawns[index].transform.position, Quaternion.identity);
        go.GetComponent<Renderer>().material = materials[index];
        go.GetComponent<PlayerController>().SetUser(users[index]);
        go.layer = 11 + index; // First "Player" layer
        return go;
    }
}
